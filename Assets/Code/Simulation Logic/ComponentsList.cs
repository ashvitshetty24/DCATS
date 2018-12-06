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
            public static bool CPU;
            public static bool CPU_Fan;
            public static bool GPU;
            public static bool HDD;
            public static bool Motherboard;
            public static bool PSU;
            public static bool RAM1;
            public static bool RAM2;
            public static bool RAM3;
            public static bool RAM4;
            public static bool allRamInstalled;
            public static bool completed;

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

            public static void CheckRam()
            {
                if(RAM1 == true && RAM2 == true && RAM3 == true && RAM4 == true)
                {
                    allRamInstalled = true;
                }
            }

            public static void CheckCompletion()
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
                    RAM4 == true &&)
                {
                    completed = true;
                }
            }
        }
    }
}
