using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets
{
    public class AttachGrabberBase : Grabber
    {
        protected override void OnEnable()
        {

        }

        protected override void OnDisable()
        {
            grabbedObjects.Clear();
        }

        public void DoGrab(BaseGrabbable obj)
        {
            if (obj.TryGrabWith(this))
            {
                this.grabbedObjects.Add(obj);
            }
        }

        public void FinishGrab()
        {
            this.GrabEnd();
        }
    }
}
