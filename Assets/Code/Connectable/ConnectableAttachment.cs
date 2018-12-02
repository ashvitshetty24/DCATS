using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Connectable
{
    public abstract class ConnectableAttachment : GrabbableSnapToOrient
    {
        private HashSet<Collider> CollidersInRange = new HashSet<Collider>();

        public virtual ConnectableSlot GetSlot()
        {
            return GrabberPrimary as ConnectableSlot;
        }

        public ConnectableEvent OnAttachAttempt;
        public ConnectableEvent OnAttachSuccess;
        public ConnectableEvent OnBadAttach;



        protected abstract bool CompatibleDiscriminator(ConnectableSlot slot);





        public void AttachAttempt(ConnectableSlot slot, bool otherAlreadyNotified = false)
        {
            if (OnAttachAttempt != null)
            {
                OnAttachAttempt.Invoke(slot, this);
            }

            if (!otherAlreadyNotified && slot != null)
            {
                slot.AttachAttempt(this, true);
            }
        }

        public void AttachSuccess(ConnectableSlot slot, bool otherAlreadyNotified = false)
        {
            if (OnAttachSuccess != null)
            {
                OnAttachSuccess.Invoke(slot, this);
            }

            if (!otherAlreadyNotified && slot != null)
            {
                slot.AttachSuccess(this, true);
            }
        }

        public void BadAttach(ConnectableSlot slot, bool otherAlreadyNotified = false)
        {
            if (OnBadAttach != null)
            {
                OnBadAttach.Invoke(slot, this);
            }

            if (!otherAlreadyNotified && slot != null)
            {
                slot.BadAttach(this, true);
            }
        }



        public override bool TryGrabWith(BaseGrabber grabber)
        {
            var slot = GetSlot();
            if (slot != null)
            {
                if (!slot.Unpluggable)
                {
                    return false;
                }
            }
            return base.TryGrabWith(grabber);
        }

        protected override void StartGrab(BaseGrabber grabber)
        {
            base.StartGrab(grabber);
            if (grabber is ConnectableSlot)
            {
                Debug.Log("[" + name + "] " + "Grabbed by slot: " + grabber.name);
                transform.position = transform.parent.position;
            }
            else
            {
                Debug.Log("[" + name + "] " + "Being grabbed non-slot: " + grabber.name);
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            CollidersInRange.Add(other);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            CollidersInRange.Remove(other);
        }


        public virtual bool InitiateAttach()
        {
            var slots = CollidersInRange
                .Select(collider => collider.gameObject.GetComponent<ConnectableSlot>())
                .Where(slot => slot != null)
                .OrderBy(slot => (slot.transform.position - this.transform.position).magnitude)
                .ToList();


            foreach (var slot in slots)
            {
                if (CompatibleDiscriminator(slot))
                {
                    if (slot.TryAttach(this))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class ConnectableAttachment<TDiscriminator> : ConnectableAttachment
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

        public new ConnectableSlot<TDiscriminator> GetSlot()
        {
            return base.GetSlot() as ConnectableSlot<TDiscriminator>;
        }

        protected override bool CompatibleDiscriminator(ConnectableSlot slot)
        {
            var typedSlot = slot as ConnectableSlot<TDiscriminator>;
            if (typedSlot != null)
            {
                return Kind.Equals(typedSlot.Kind);
            }
            else
            {
                return false;
            }
        }
    }
}
