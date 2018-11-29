using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using DCATS.Assets.Attachable;

namespace DCATS.Assets.Plugs
{
    public class PlugSlot : AttachGrabberBase, IAttachableKindInfo<PlugType>
    {
        [SerializeField]
        public PlugType Kind;
        
        public WirePluggedEvent OnPlugSuccess;

        public WirePlugBase Plug { get; protected set; }

        PlugType IAttachableKindInfo<PlugType>.Kind
        {
            get
            {
                return this.Kind;
            }

            set
            {
                this.Kind = value;
            }
        }

        public override void DoGrab(BaseGrabbable obj)
        {
            base.DoGrab(obj);
            var plugGrabbable = obj as PlugGrabbable;
            if (plugGrabbable != null)
            {
                var plug = plugGrabbable.gameObject.GetComponent<WirePlugBase>();
                if (plug != null)
                {
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
