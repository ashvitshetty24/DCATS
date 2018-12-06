﻿using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Attachable
{
    public partial class SimLogic : MonoBehaviour {

        // Use this for initialization
        public void Start() {

        }

        // Update is called once per frame
        public void Update() {
            if(ComponentsList.completed)
            {
                // trigger alert informing user that they successfully completed the simulation
                // provide button to exit to main menu
            }
        }

        // update installed components
        public static void UpdateInstalled(BaseGrabbable obj)
        {
            switch(obj.name)
            {
                case "CPU":
                    ComponentsList.CPU = true;
                    // trigger success sound
                    // trigger transition in instructions from CPU to CPU Fan installation in Guided
                    break;
                case "CPU_Fan":
                    ComponentsList.CPU_Fan = true;
                    break;
                    // trigger success sound
                    // trigger transition in instructions from CPU Fan to GPU installation
                case "GPU":
                    ComponentsList.GPU = true;
                    break;
                    // trigger success sound
                    // trigger transition from GPU to RAM installation
                case "RAM1":
                    ComponentsList.RAM1 = true;
                    break;
                    // trigger success sound
                case "RAM2":
                    ComponentsList.RAM2 = true;
                    break;
                    // trigger success sound
                case "RAM3":
                    ComponentsList.RAM3 = true;
                    break;
                    // trigger success sound
                case "RAM4":
                    ComponentsList.RAM4 = true;
                    break;
                    // trigger success sound
                case "Motherboard":
                    ComponentsList.Motherboard = true;
                    break;
                    // trigger success sound
                    // trigger transition from Motherboard to HDD installation
                case "HDD":
                    ComponentsList.HDD = true;
                    break;
                    // trigger success sound
                    // trigger transition from HDD to PSU installation
                case "PSU":
                    ComponentsList.PSU = true;
                    break;
                    // trigger success sound
                }
            ComponentsList.CheckRam();
            ComponentsList.CheckCompletion();
            }
        }
    }
}
