using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Extensions
{
    public static class LogExtension
    {
        public static void ObjectLog(this UnityEngine.Object obj, object message)
        {
            Debug.Log("[" + obj.name + "] " + message, obj);
        }
    }
}
