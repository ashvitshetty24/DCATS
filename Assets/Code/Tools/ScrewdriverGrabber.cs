using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Tools
{
    public class ScrewdriverGrabber : Grabber
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







        protected override void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger entered!");
            base.OnTriggerEnter(other);
        }
    }
}
