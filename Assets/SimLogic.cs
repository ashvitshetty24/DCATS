using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Attachable
{
    public class SimLogic : MonoBehaviour {
        // components list as Booleans
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
        public static bool completed;
        public static List<bool> components;

        // Use this for initialization
        public void Start() {
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
            completed = false;
            components.Add(CPU);
            components.Add(CPU_Fan);
            components.Add(GPU);
            components.Add(HDD);
            components.Add(Motherboard);
            components.Add(PSU);
            components.Add(RAM1);
            components.Add(RAM2);
            components.Add(RAM3);
            components.Add(RAM4);
        }

        // Update is called once per frame
        void Update() {
            if(completed)
            {
                
            }
        }

        // update installed components
        public static void UpdateInstalled(BaseGrabbable obj)
        {
            switch(obj.name)
            {
                case "CPU":
                    CPU = true;
                    components.RemoveAt(0);
                    components.Insert(0, CPU);
                    break;
                case "CPU_Fan":
                    CPU_Fan = true;
                    components.RemoveAt(1);
                    components.Insert(1, CPU_Fan);
                    break;
                case "GPU":
                    GPU = true;
                    components.RemoveAt(2);
                    components.Insert(2, GPU);
                    break;
                case "HDD":
                    HDD = true;
                    components.RemoveAt(3);
                    components.Insert(3, HDD);
                    break;
                case "Motherboard":
                    Motherboard = true;
                    components.RemoveAt(4);
                    components.Insert(4, Motherboard);
                    break;
                case "PSU":
                    PSU = true;
                    components.RemoveAt(5);
                    components.Insert(5, PSU);
                    break;
                case "RAM1":
                    RAM1 = true;
                    components.RemoveAt(6);
                    components.Insert(6, RAM1);
                    break;
                case "RAM2":
                    RAM2 = true;
                    components.RemoveAt(7);
                    components.Insert(7, RAM2);
                    break;
                case "RAM3":
                    RAM3 = true;
                    components.RemoveAt(8);
                    components.Insert(8, RAM3);
                    break;
                case "RAM4":
                    RAM4 = true;
                    components.RemoveAt(9);
                    components.Insert(9, RAM4);
                    break;
            }
            for(int i = 0; i < components.Count; i++)
            {
                if(components.ElementAt(i) == false)
                {
                    return;
                }
                completed = true;
            }
        }
    }
}
