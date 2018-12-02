using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Connectable
{
    public class ConnectableAttachment : GrabbableSnapToOrient
    {
        public virtual ConnectableSlot GetSlot()
        {
            return GrabberPrimary as ConnectableSlot;
        }

        public ConnectableEvent OnAttachAttempt;
        public ConnectableEvent OnAttachSuccess;
        public ConnectableEvent OnBadAttach;







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
    }
}
