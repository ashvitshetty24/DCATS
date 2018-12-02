using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;

namespace DCATS.Assets.Attachable
{
    public interface IAttachSlotEvents<TSlot> where TSlot : AttachGrabberBase
    {
        AttachableEvent<TSlot> OnAttachSuccess { get; }
    }
}
