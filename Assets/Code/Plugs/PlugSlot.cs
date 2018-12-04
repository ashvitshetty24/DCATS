using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using DCATS.Assets.Connectable;

namespace DCATS.Assets.Plugs
{
    [AddComponentMenu("DCATS/Plugs/Plug Slot")]
    public class PlugSlot : ConnectableSlot<PlugType>
    {

    }
}
