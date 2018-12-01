using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets
{
    public class AttachGrabbableBase : GrabbableSnapToOrient
    {
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
    }
}
