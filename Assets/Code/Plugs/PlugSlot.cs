using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;

namespace DCATS.Assets.Plugs
{
    public class PlugSlot : AttachGrabberBase
    {
        [SerializeField]
        public PlugType Kind;



        public WirePluggedEvent OnPlugSuccess;
    }
}
