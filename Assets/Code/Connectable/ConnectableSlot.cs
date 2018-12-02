using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Connectable
{
    public class ConnectableSlot : BaseGrabber
    {
        private Dictionary<Collider, bool> CollidersNeedingReEntry = new Dictionary<Collider, bool>();

        [SerializeField]
        public bool Unpluggable = false;

        public ConnectableAttachment Attached { get; protected set; }

        public virtual bool IsAttached
        {
            get
            {
                return this.Attached != null;
            }
        }

        public ConnectableEvent OnAttachAttempt;
        public ConnectableEvent OnAttachSuccess;
        public ConnectableEvent OnBadAttach;





        public virtual bool CompatibleAttachment(ConnectableAttachment attachment)
        {
            return true;
        }

        protected void OnTriggerEnter(Collider other)
        {
            bool needsExit = false;
            if (CollidersNeedingReEntry.TryGetValue(other, out needsExit) && needsExit)
            {
                return;
            }
            GameObject otherObj = other.gameObject;
            ConnectableAttachment attachment = otherObj.GetComponent<ConnectableAttachment>();

            if (attachment != null)
            {
                if (!IsAttached || Unpluggable)
                {
                    TryAttach(attachment);
                }
                
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (CollidersNeedingReEntry.ContainsKey(other))
            {
                CollidersNeedingReEntry[other] = false;
            }
        }


        public virtual bool TryAttach(ConnectableAttachment attach)
        {
            Debug.Log("[" + name + "] " + "TryAttach() called.");
            if (attach != null)
            {
                AttachAttempt(attach);
                if (CompatibleAttachment(attach))
                {
                    if (ExecuteGrab(attach))
                    {
                        Attached = attach;
                        AttachSuccess(attach);
                        return true;
                    }
                }
                else
                {
                    BadAttach(attach);
                }
            }

            Debug.Log("[" + name + "] " + "Attachment failed.");

            return false;
        }

        public virtual bool Detach()
        {
            if (Attached != null)
            {
                Debug.Log("[" + name + "] " + "Detaching " + Attached.name + "...");
                var collider = Attached.gameObject.GetComponent<Collider>();
                if (collider != null)
                {
                    Debug.Log("[" + name + "] " + "Added " + Attached.name + " to ReEntry list.");
                    CollidersNeedingReEntry[collider] = true;
                }

                EnableCollisions(Attached);
            }
            GrabEnd();
            Attached = null;
            return true;
        }

        protected virtual bool ExecuteGrab(ConnectableAttachment attachment)
        {
            BaseGrabbable closestAvailable = attachment;
            if (closestAvailable.TryGrabWith(this))
            {
                if (IsAttached)
                {
                    Detach();
                }
                grabbedObjects.Add(closestAvailable);
                DisableCollisions(attachment);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void DisableCollisions(ConnectableAttachment attachment)
        {
            UnityEngine.Physics.IgnoreCollision(attachment.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>(), true);
        }

        protected void EnableCollisions(ConnectableAttachment attachment)
        {
            UnityEngine.Physics.IgnoreCollision(attachment.gameObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>(), false);
        }

        public override bool CanTransferOwnershipTo(BaseGrabbable ownerGrab, BaseGrabber otherGrabber)
        {
            if (!Unpluggable && IsAttached)
            {
                return false;
            }
            else
            {
                if (Attached != null)
                {
                    Detach();
                }
                return base.CanTransferOwnershipTo(ownerGrab, otherGrabber);
            }
        }


        public void AttachAttempt(ConnectableAttachment attachment, bool otherAlreadyNotified = false)
        {
            if (OnAttachAttempt != null)
            {
                OnAttachAttempt.Invoke(this, attachment);
            }

            if (!otherAlreadyNotified && attachment != null)
            {
                attachment.AttachAttempt(this, true);
            }
        }

        public void AttachSuccess(ConnectableAttachment attachment, bool otherAlreadyNotified = false)
        {
            if (OnAttachSuccess != null)
            {
                OnAttachSuccess.Invoke(this, attachment);
            }

            if (!otherAlreadyNotified && attachment != null)
            {
                attachment.AttachSuccess(this, true);
            }
        }

        public void BadAttach(ConnectableAttachment attachment, bool otherAlreadyNotified = false)
        {
            if (OnBadAttach != null)
            {
                OnBadAttach.Invoke(this, attachment);
            }

            if (!otherAlreadyNotified && attachment != null)
            {
                attachment.BadAttach(this, true);
            }
        }
    }

    public class ConnectableSlot<TDiscriminator> : ConnectableSlot
    {
        [SerializeField]
        protected TDiscriminator Type;

        public TDiscriminator Kind
        {
            get
            {
                return Type;
            }
        }

        public ConnectableAttachment<TDiscriminator> TypedAttached
        {
            get
            {
                return Attached as ConnectableAttachment<TDiscriminator>;
            }

            set
            {
                Attached = value;
            }
        }


        public virtual bool CheckDiscriminator(TDiscriminator other)
        {
            return Kind.Equals(other);
        }

        public override bool CompatibleAttachment(ConnectableAttachment attachment)
        {
            var typedAttachment = attachment as ConnectableAttachment<TDiscriminator>;
            if (typedAttachment != null)
            {
                return CheckDiscriminator(typedAttachment.Kind);
            }

            return false;
        }
    }
}
