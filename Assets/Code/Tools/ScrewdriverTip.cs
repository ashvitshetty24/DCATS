using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCATS.Assets.Connectable;

namespace DCATS.Assets.Tools
{
    public class ScrewdriverTip : ConnectableSlot<ScrewKind>
    {
        public ScrewdriverTip() : base()
        {
            Unpluggable = true;
            AutomaticAttach = false;
        }

        public void ActionTriggered()
        {
            if (Attached)
            {
                var screw = this.Attached as Screw;
                if (screw != null)
                {
                    if (!screw.InitiateAttach())
                    {
                        screw.ForceDetach();
                    }
                }
            }
        }
    }
}
