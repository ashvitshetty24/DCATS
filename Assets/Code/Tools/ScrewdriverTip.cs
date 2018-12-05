using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCATS.Assets.Connectable;
using UnityEngine;
using DCATS.Assets.Extensions;

namespace DCATS.Assets.Tools
{
    public class ScrewdriverTip : ConnectableSlot<ScrewKind>
    {
        public ScrewdriverTip() : base()
        {
            Unpluggable = true;
            AutomaticAttach = true;
        }

        public void ActionTriggered()
        {
            Debug.Log("Action Triggered.");
            if (Attached)
            {
                var screw = this.Attached as Screw;
                if (screw != null)
                {

                    if (!screw.InitiateAttach<ScrewHole>())
                    {
                        screw.ForceDetach();
                    }
                }
            }
        }
    }
}
