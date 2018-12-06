using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace DCATS.Assets.Attachable
{
    public partial class SimLogic
    {
        // components list as Booleans
        public class ComponentsList
        {
            public bool CPU;
            public bool CPU_Fan;
            public bool GPU;
            public bool HDD;
            public bool Motherboard;
            public bool PSU;
            public bool RAM1;
            public bool RAM2;
            public bool RAM3;
            public bool RAM4;
            public bool allRamInstalled;
            public bool completed;

            public ComponentsList ()
            {
                CPU = false;
                CPU_Fan = false;
                GPU = false;
                HDD = false;
                Motherboard = false;
                PSU = false;
                RAM1 = false;
                RAM2 = false;
                RAM3 = false;
                RAM4 = false;
                allRamInstalled = false;
                completed = false;
            }

            public void CheckRam()
            {
                if(RAM1 == true && RAM2 == true && RAM3 == true && RAM4 == true)
                {
                    allRamInstalled = true;
                }
            }

            public void CheckCompletion()
            {
                if(CPU == true &&
                    CPU_Fan == true &&
                    GPU == true &&
                    HDD == true &&
                    Motherboard == true &&
                    PSU == true &&
                    RAM1 == true &&
                    RAM2 == true &&
                    RAM3 == true &&
                    RAM4 == true)
                {
                    completed = true;
                }
            }

            public void Reset()
            {
                CPU = false;
                CPU_Fan = false;
                GPU = false;
                HDD = false;
                Motherboard = false;
                PSU = false;
                RAM1 = false;
                RAM2 = false;
                RAM3 = false;
                RAM4 = false;
                allRamInstalled = false;
                completed = false;
            }
        }
    }
}
