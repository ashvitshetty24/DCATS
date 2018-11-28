using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;

namespace DCATS.Assets.Plugs
{
    public class SlotGrabber : Grabber
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
