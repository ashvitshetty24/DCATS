using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;

namespace DCATS.Assets.Plugs
{
    public static class ColorCoding
    {
        public static readonly Color MotherboardPower = new Color(29.0f/255, 32.0f/255, 85.0f/255);
        public static readonly Color USB = Color.magenta;
        public static readonly Color SATAData = Color.cyan;
        public static readonly Color SATAPower = Color.yellow;
        public static readonly Color CPUFan = Color.green;

        public static readonly Color Default = Color.black;

        public static Color ColorForPlug(PlugType kind)
        {
            switch (kind)
            {
                case PlugType.MotherboardPower:
                    return MotherboardPower;

                case PlugType.USB:
                    return USB;

                case PlugType.SATAData:
                    return SATAData;

                case PlugType.SATAPower:
                    return SATAPower;

                case PlugType.CPUFan:
                    return CPUFan;

                default:
                    return Default;
            }

        }
    }
}
