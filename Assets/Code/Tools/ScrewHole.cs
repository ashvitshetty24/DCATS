using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCATS.Assets.Tools
{
    public class ScrewHole : Connectable.ConnectableSlot<ScrewKind>
    {
        public ScrewHole() : base()
        {
            AutomaticAttach = false;
            Unpluggable = false;
        }

        public void OnStart()
        {
            AutomaticAttach = false;
            Unpluggable = false;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            AutomaticAttach = false;
            Unpluggable = false;
        }

        public virtual void ScrewInto(Screw screw)
        {

        }
    }
}
