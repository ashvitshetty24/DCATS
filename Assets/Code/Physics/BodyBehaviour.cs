using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DCATS.Assets.Physics;

namespace DCATS.Assets.Physics
{
	public class BodyBehaviour : MonoBehaviour, IUnityBody
	{
		public Rigidbody Body
		{
			get
			{
				return this.GetComponent<Rigidbody>();
			}
		}

		public Transform Transform
		{
			get
			{
				return ((MonoBehaviour)this).transform;
			}
		}

		public Vector3 Velocity
		{
			get
			{
				return Body.velocity;
			}

			set
			{
				Body.velocity = value;
			}
		}

		public Vector3 AngularVelocity
		{
			get
			{
				return Body.angularVelocity;
			}

			set
			{
				Body.angularVelocity = value;
			}
		}

		private ConstantForce constantForceComponent = null;
		public Vector3 ConstantForce
		{
			get
			{
				if (constantForceComponent == null)
				{
					constantForceComponent = GetComponent<ConstantForce>();
					if (constantForceComponent == null)
					{
						constantForceComponent = gameObject.AddComponent<ConstantForce>();
					}
				}

				return constantForceComponent.force;
			}

			set
			{
				if (constantForceComponent == null)
				{
					constantForceComponent = GetComponent<ConstantForce>();
					if (constantForceComponent == null)
					{
						constantForceComponent = gameObject.AddComponent<ConstantForce>();
					}
				}

				constantForceComponent.force = value;
			}
		}






		public BodyBehaviour()
		{
			//ConstantForce = new Vector3(0, 0, 0);
		}


		protected virtual void _Update()
		{

		}

		public void Update()
		{
			// Add code here
			//Body.AddForce(ConstantForce);



			_Update();
		}

		protected virtual void Init()
		{

		}

		public void Start()
		{
			Init();
		}
	}
}
