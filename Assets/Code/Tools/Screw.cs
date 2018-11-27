using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Tools
{
    public class Screw : GrabbableSnapToOrient
    {
        [SerializeField]
        public ScrewKind Kind = ScrewKind.Phillips;
    }
}
