using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using UnityEngine.Events;

namespace DCATS.Assets.Code.Components
{
    public interface IComponentInfo
    {
        [SerializeField]
        ComponentType Kind { get; }
    }
}
