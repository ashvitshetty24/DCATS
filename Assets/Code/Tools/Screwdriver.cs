using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Tools
{
    public class Screwdriver : BaseUsable
    {
        [SerializeField]
        public ScrewKind Kind = ScrewKind.Phillips;


        public Screwdriver()
        {

        }

        public Screwdriver(ScrewKind kind) : this()
        {
            Kind = kind;
        }


        protected override void OnEnable()
        {
            //base.OnEnable();

            Debug.Log("Screwdriver is ENABLED.");
        }

        protected override void OnDisable()
        {
            Debug.Log("Screwdriver is DISABLED.");

            //base.OnDisable();
        }


        protected override void UseStart()
        {
            Debug.Log("Screwdriver is being USED.");
        }

        protected override void UseEnd()
        {
            Debug.Log("Screwdriver is DONE being USED.");
        }
    }
}
