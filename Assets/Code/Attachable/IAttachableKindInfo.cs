using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;

namespace DCATS.Assets.Attachable
{
    public interface IAttachableKindInfo<TKind>
    {
        TKind Kind { get; set; }
    }
}
