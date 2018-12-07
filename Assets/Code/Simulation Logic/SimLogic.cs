using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Attachable
{
    public class SimLogic : MonoBehaviour
    {
        public bool isGuided;
        public static ComponentsManager Instance;
        public  AudioSource audioInstalled;
        public GameObject CPU_Instructions;
        public GameObject CPU_Fan_Instructions;
        public GameObject GPU_Instructions;
        public GameObject RAM_Instructions;
        public GameObject Motherboard_Instructions;
        public GameObject HDD_Instructions;
        public GameObject PSU_Instructions;
        public GameObject PSU_Cable_Instructions;
        public GameObject End_Simulation;

        private void Start()
        {
            Instance = new ComponentsManager(isGuided);
            audioInstalled = GetComponentInParent<AudioSource>();
            Debug.Log("This simulation is guided: " + isGuided);
            Debug.Log("Init " + CPU_Instructions);
            Debug.Log("Init " + CPU_Fan_Instructions);
            Debug.Log("Init " + GPU_Instructions);
            Debug.Log("Init " + RAM_Instructions);
            Debug.Log("Init " + Motherboard_Instructions);
            Debug.Log("Init " + HDD_Instructions);
            Debug.Log("Init " + PSU_Instructions);
            Debug.Log("Init " + PSU_Cable_Instructions);
            Debug.Log("Init " + End_Simulation);
        }

        public void Update()
        {
            // check for completion of simulation
            if(Instance.Components.completed)
            {
                endSim();
            }
        }

        public void endSim()
        {
            // notify user that the simulation is complete
            // delay
            // load the main menu scene
        }

        public void Transition(GameObject previous, GameObject next)
        {
            previous.SetActive(false);
            next.SetActive(true);
        }


    }
    public class ComponentsManager : SimLogic
    {
        public ComponentsList Components;

        public ComponentsManager(bool guided)
        {
            Components = new ComponentsList();
            isGuided = guided;
        }

        public void UpdateComponents(BaseGrabbable obj)
        {
            Debug.LogWarning("SimLogic.UpdateComponents called with " + obj.name + "as argument.");
            switch (obj.name)
            {
                /* GUIDED PRACTICE STEPS
                 * 
                 *  1. Install CPU
                 *      1a. Pick up CPU
                 *      1b. Locate CPU socket on motherboard
                 *      1c. Place CPU in CPU socket
                 *  2. Install CPU Fan
                 *      2a. Pick up CPU Fan
                 *      2b. Locate CPU on motherboard
                 *      3c. Place CPU Fan on top of CPU
                 *  3. Install GPU
                 *      3a. Pick up GPU
                 *      3b. Locate PCI slots on motherboard
                 *      3c. Place GPU in empty PCI slot
                 *  4. Install RAM modules
                 *      4a. Pick up RAM modules
                 *      4b. Locate RAM slots on motherboard
                 *      4c. Place RAM module in empty RAM slot
                 *      4d. Repeat for each of the four RAM modules
                 *  5. Install Motherboard into PC Case
                 *      5a. Pick up Motherboard
                 *      5b. Locate mounting plate inside PC Case
                 *      5c. Place Motherboard on mounting plate
                 *  6. Install Hard Drive into PC Case
                 *      6a. Pick up Hard Drive
                 *      6b. Locate Hard Drive mounting bracket
                 *      6c. Place Hard Drive in mounting bracket
                 *  7. Install Power Supply Unit into PC Case
                 *      7a. Pick up Power Supply Unit
                 *      7b. Locate mounting location in PC Case
                 *      7c. Place Power Supply Unit in mounting location
                 *  8. Plug in power cords from PSU
                 *      8a. Pick up SATA Power Plug
                 *      8b. Locate Hard Drive SATA Power socket
                 *      8c. Plug SATA Power Plug into Hard Drive SATA Power socket
                 *      8d. Pick up Motherboard Power Plug.
                 *      8e. Locate Power Socket on Motherboard.
                 *      8f. Plug Motherboard Power Plug into Power Socket on Motherboard
                 *      8g. Pick up CPU Power Plug
                 *      8h. Locate CPU Power Socket on Motherboard
                 *      8i. Plug CPU Power Plug into CPU Power Socket on Motherboard
                 */

                case "CPU":
                    Debug.Log("CPU has been installed!");
                    Components.CPU = true;
                    if (isGuided)
                    {
                        Transition(CPU_Instructions, CPU_Fan_Instructions);
                    }
                    audioInstalled.Play();
                    break;
                case "CPU_Fan":
                    Debug.Log("CPU Fan has been installed!");
                    Components.CPU_Fan = true;
                    if (isGuided)
                    {
                        Transition(CPU_Fan_Instructions, GPU_Instructions);
                    }
                    audioInstalled.Play();
                    break;
                case "GPU":
                    Debug.Log("GPU has been installed!");
                    Components.GPU = true;
                    if (isGuided)
                    {
                        Transition(GPU_Instructions, RAM_Instructions);
                    }
                    audioInstalled.Play();
                    break;
                case "RAM1":
                    Debug.Log("RAM1 has been installed!");
                    Components.RAM1 = true;
                    Components.CheckRam();
                    if (isGuided && Components.allRamInstalled)
                    {
                        Transition(RAM_Instructions, Motherboard_Instructions);
                    }
                    audioInstalled.Play();
                    break;
                case "RAM2":
                    Debug.Log("RAM2 has been installed!");
                    Components.RAM2 = true;
                    Components.CheckRam();
                    if (isGuided && Components.allRamInstalled)
                    {
                        Transition(RAM_Instructions, Motherboard_Instructions);
                    }
                    audioInstalled.Play();
                    break;
                case "RAM3":
                    Debug.Log("RAM3 has been installed!");
                    Components.RAM3 = true;
                    Components.CheckRam();
                    if (isGuided && Components.allRamInstalled)
                    {
                        Transition(RAM_Instructions, Motherboard_Instructions);
                    }
                    audioInstalled.Play();
                    break;
                case "RAM4":
                    Debug.Log("RAM4 has been installed!");
                    Components.RAM4 = true;
                    Components.CheckRam();
                    if (isGuided && Components.allRamInstalled)
                    {
                        Transition(RAM_Instructions, Motherboard_Instructions);
                    }
                    audioInstalled.Play();
                    break;
                case "Motherboard":
                    Debug.Log("Motherboard has been installed!");
                    Components.Motherboard = true;
                    if (isGuided)
                    {
                        Transition(Motherboard_Instructions, HDD_Instructions);
                    }
                    audioInstalled.Play();
                    break;
                case "HDD":
                    Debug.Log("Hard Drive has been installed!");
                    Components.HDD = true;
                    if (isGuided)
                    {
                        Transition(HDD_Instructions, PSU_Instructions);
                    }
                    audioInstalled.Play();
                    break;
                case "PSU":
                    Debug.Log("Power Supply has been installed!");
                    Components.PSU = true;
                    if (isGuided)
                    {
                        Transition(PSU_Instructions, PSU_Cable_Instructions);
                    }
                    audioInstalled.Play();
                    break;
                case null:
                    Debug.LogWarning("UpdateComponents called with null BaseGrabbable obj!");
                    break;
            }
            Components.CheckCompletion();
            Components.CheckRam();
        }
    }
}
