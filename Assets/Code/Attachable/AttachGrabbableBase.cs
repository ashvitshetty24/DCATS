using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Attachable
{
    public class AttachGrabbableBase : GrabbableSnapToOrient
    {
        #region Property Fields
        private AttachableBase _Base = null;
        #endregion

        protected AttachableBase Base
        {
            get
            {
                if (_Base == null)
                {
                    _Base = this.gameObject.GetComponent<AttachableBase>();
                }

                return _Base;
            }
        }



        public virtual void DisableGrabbing()
        {
            this.enabled = false;
        }

        public virtual void EnableGrabbing()
        {
            this.enabled = true;
        }

        public virtual void DetachAllGrabbers()
        {
            foreach (var grabber in this.ActiveGrabbers)
            {
                this.DetachFromGrabber(grabber);
            }
        }

        public virtual void TransferTo(BaseGrabber grabber)
        {
            foreach (var oldGrabber in this.ActiveGrabbers.ToList())
            {
                if (!oldGrabber.CanTransferOwnershipTo(this, grabber))
                {
                    Debug.LogError("Couldn't transfer ownership of " + this.name + " from " + oldGrabber.name);
                }
                //this.DetachFromGrabber(grabber); (?)
            }
        }

        public override bool TryGrabWith(BaseGrabber grabber)
        {
            if (Base.UnPluggable || !Base.IsPluggedIn())
            {
                if (Base.UnPluggable)
                {
                    Debug.LogWarning("MARKED AS UNPLUGGABLE WTF BRO");
                }
                return base.TryGrabWith(grabber);
            }
            else
            {
                Debug.Log("Won't start duplicate grab action!! Already Plugged In: " + Base.PluggedInObject().name + ", Attempted Grabber: " + grabber.name);
                Debug.Log("[" + name + "] " + "Stack: " + StackTraceUtility.ExtractStackTrace());
                return false;
            }
        }

        protected override void StartGrab(BaseGrabber grabber)
        {
            base.StartGrab(grabber);
            if (grabber is AttachGrabberBase)
            {
                transform.position = transform.parent.position;
                //UnityEngine.Physics.IgnoreCollision(GetComponent<Collider>(), grabber.GetComponent<Collider>());
                //this.GetComponent<Collider>().isTrigger = true;
            }
            Base.SetGrabber(grabber);
        }
    }
}
