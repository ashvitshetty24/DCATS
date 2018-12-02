using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCATS.Assets.Physics;
using UnityEngine;

namespace DCATS.Assets.Physics
{
	public interface IRope : IUnityBody
	{
		float Length { get; }
		float Radius { get; set; }
#if IMPLEMENTED_DENSITY
		float Density { get; set; }
#endif
		float SpringConstant { get; set; }

		Vector3 Top { get; }
		Vector3 Bottom { get; }
	}
}
