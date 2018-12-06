using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Attachable
{
    public abstract class AttachGrabberBase : Grabber
    {
        protected override void OnEnable()
        {

        }

        protected override void OnDisable()
        {
            grabbedObjects.Clear();
        }

        public virtual void DoGrab(BaseGrabbable obj)
        {
            if (obj.TryGrabWith(this))
            {
                this.grabbedObjects.Add(obj);
                Debug.LogWarning("[" + name + "] " + "TryGrabWith Succeeded. Grabbable: " + obj.name);
                SimLogic.UpdateInstalled(obj);
            }
            else
            {
                Debug.LogWarning("[" + name + "] " + "TryGrabWith failed! Grabbable: " + obj.name);
            }
        }

        public virtual void FinishGrab()
        {
            this.GrabEnd();
        }

        public abstract bool IsOccupied();
    }

    public class AttachGrabberBase<TAttachable> : AttachGrabberBase where TAttachable : AttachableBase
    {
        public TAttachable Plug { get; protected set; }

        public override bool IsOccupied()
        {
            return this.Plug != null;
        }

        public override void DoGrab(BaseGrabbable obj)
        {
            base.DoGrab(obj);
            var plugGrabbable = obj as AttachGrabbableBase;
            if (plugGrabbable != null)
            {
                var plug = plugGrabbable.gameObject.GetComponent<TAttachable>();
                if (plug != null)
                {
                    Debug.LogWarning("Plug grab activated.");
                    this.Plug = plug;
                }
            }
        }

        public override void FinishGrab()
        {
            base.FinishGrab();
            Plug = null;
        }
    }
}
