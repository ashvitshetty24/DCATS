using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Attachable
{
    public partial class SimLogic : MonoBehaviour {

        public bool isGuided;
        public static ComponentsList Components = new ComponentsList();

        // Update is called once per frame
        public void Update()
        {
            if(Components.completed)
            {
                // trigger alert informing user that they successfully completed the simulation
                // provide button to exit to main menu
                // clear Components
                Components.Reset();
            }
        }

        // update installed components
        public static void UpdateComponents(BaseGrabbable obj)
        {
            switch(obj.name)
            {
                case "CPU":
                    Components.CPU = true;
                    // trigger success sound
                    // trigger transition in instructions from CPU to CPU Fan installation in Guided
                    break;
                case "CPU_Fan":
                    Components.CPU_Fan = true;
                    break;
                    // trigger success sound
                    // trigger transition in instructions from CPU Fan to GPU installation
                case "GPU":
                    Components.GPU = true;
                    break;
                    // trigger success sound
                    // trigger transition from GPU to RAM installation
                case "RAM1":
                    Components.RAM1 = true;
                    break;
                    // trigger success sound
                case "RAM2":
                    Components.RAM2 = true;
                    break;
                    // trigger success sound
                case "RAM3":
                    Components.RAM3 = true;
                    break;
                    // trigger success sound
                case "RAM4":
                    Components.RAM4 = true;
                    break;
                    // trigger success sound
                case "Motherboard":
                    Components.Motherboard = true;
                    break;
                    // trigger success sound
                    // trigger transition from Motherboard to HDD installation
                case "HDD":
                    Components.HDD = true;
                    break;
                    // trigger success sound
                    // trigger transition from HDD to PSU installation
                case "PSU":
                    Components.PSU = true;
                    break;
                    // trigger success sound
                }

            Components.CheckRam();
            Components.CheckCompletion();
        }
    }
}
