using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using UnityEngine.Events;
using DCATS.Assets.Attachable;

namespace DCATS.Assets.Components
{
    public class ComponentSlot : AttachGrabberBase, IAttachableKindInfo<ComponentType>, IAttachSlotEvents<ComponentSlot>
    {
        [SerializeField]
        public ComponentType Kind;

        public AttachableEvent<ComponentSlot> OnAttachSuccess;

        AttachableEvent<ComponentSlot> IAttachSlotEvents<ComponentSlot>.OnAttachSuccess
        {
            get
            {
                return this.OnAttachSuccess;
            }
        }

        ComponentType IAttachableKindInfo<ComponentType>.Kind
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

        
    }
}
