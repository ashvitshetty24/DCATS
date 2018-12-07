using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using UnityEngine;

namespace DCATS.Assets.Attachable
{
    public class SimLogic : MonoBehaviour
    {
        public bool isGuided;
        public static SimulationManager Instance;
        public AudioSource audioInstalled;
        public AudioSource audioCompleted;
        public AudioSource audioMistake;
        public GameObject CPU_Instructions;
        public GameObject CPU_Fan_Instructions;
        public GameObject GPU_Instructions;
        public GameObject RAM_Instructions;
        public GameObject Motherboard_Instructions;
        public GameObject HDD_Instructions;
        public GameObject PSU_Instructions;
        public GameObject PSU_Cable_Instructions;
        public GameObject End_Simulation;
        public int Mistakes;

        public void Start()
        {
            Instance = new SimulationManager(isGuided);
            Instance.Mistakes = 0;
        }

        public void Update()
        {
            // check for completion of simulation
            if(Instance.Components.completed)
            {
                Instance.endSim();
            }
        }
    }
    public class SimulationManager : SimLogic
    {
        public ComponentsList Components;

        public SimulationManager(bool guided)
        {
            Components = new ComponentsList();
            isGuided = guided;
        }

        public void MistakeMade(BaseGrabbable obj)
        {
            Debug.Log("A mistake was made! Object attempting to be plugged in is " + obj.name);
            audioMistake.Play();
            Mistakes++;
        }

        public void endSim()
        {
            if (isGuided)
            {

            }
            else
            {
                End_Simulation.SetActive(true);
                audioCompleted.Play();
                Debug.Log("A total of " + Mistakes + " were made.");

            }

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
                        CPU_Instructions.SetActive(false);
                        CPU_Fan_Instructions.SetActive(true);
                    }
                    audioInstalled.Play();
                    break;
                case "CPU_Fan":
                    Debug.Log("CPU Fan has been installed!");
                    Components.CPU_Fan = true;
                    if (isGuided)
                    {
                        CPU_Fan_Instructions.SetActive(false);
                        GPU_Instructions.SetActive(true);
                    }
                    audioInstalled.Play();
                    break;
                case "GPU":
                    Debug.Log("GPU has been installed!");
                    Components.GPU = true;
                    if (isGuided)
                    {
                        GPU_Instructions.SetActive(false);
                        RAM_Instructions.SetActive(true);
                    }
                    audioInstalled.Play();
                    break;
                case "RAM1":
                    Debug.Log("RAM1 has been installed!");
                    Components.RAM1 = true;
                    Components.CheckRam();
                    if (isGuided && Components.allRamInstalled)
                    {
                        RAM_Instructions.SetActive(false);
                        Motherboard_Instructions.SetActive(true);
                    }
                    audioInstalled.Play();
                    break;
                case "RAM2":
                    Debug.Log("RAM2 has been installed!");
                    Components.RAM2 = true;
                    Components.CheckRam();
                    if (isGuided && Components.allRamInstalled)
                    {
                        RAM_Instructions.SetActive(false);
                        Motherboard_Instructions.SetActive(true);
                    }
                    audioInstalled.Play();
                    break;
                case "RAM3":
                    Debug.Log("RAM3 has been installed!");
                    Components.RAM3 = true;
                    Components.CheckRam();
                    if (isGuided && Components.allRamInstalled)
                    {
                        RAM_Instructions.SetActive(false);
                        Motherboard_Instructions.SetActive(true);
                    }
                    audioInstalled.Play();
                    break;
                case "RAM4":
                    Debug.Log("RAM4 has been installed!");
                    Components.RAM4 = true;
                    Components.CheckRam();
                    if (isGuided && Components.allRamInstalled)
                    {
                        RAM_Instructions.SetActive(false);
                        Motherboard_Instructions.SetActive(true);
                    }
                    audioInstalled.Play();
                    break;
                case "Motherboard":
                    Debug.Log("Motherboard has been installed!");
                    Components.Motherboard = true;
                    if (isGuided)
                    {
                        Motherboard_Instructions.SetActive(false);
                        HDD_Instructions.SetActive(true);
                    }
                    audioInstalled.Play();
                    break;
                case "HDD":
                    Debug.Log("Hard Drive has been installed!");
                    Components.HDD = true;
                    if (isGuided)
                    {
                        HDD_Instructions.SetActive(false);
                        PSU_Instructions.SetActive(true);
                    }
                    audioInstalled.Play();
                    break;
                case "PSU":
                    Debug.Log("Power Supply has been installed!");
                    Components.PSU = true;
                    if (isGuided)
                    {
                        PSU_Instructions.SetActive(false);

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
