using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using UnityEngine.Events;

namespace DCATS.Assets.Components
{
    public enum ComponentType
    {
        CPU = 1,
        RAM = 2,
        GPU = 3,
        CPU_FAN = 4,
        PSU = 5,
        HDD = 6,
        MOBO = 7,
        // TODO: Other components
    }
}
