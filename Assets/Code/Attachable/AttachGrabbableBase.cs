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
    }
}
