﻿using System;
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

		public Vector3 ConstantForce { get; set; }






		public BodyBehaviour()
		{
			ConstantForce = new Vector3(0, 0, 0);
		}


		protected virtual void _Update()
		{

		}

		public void Update()
		{
			// Add code here
			Body.AddForce(ConstantForce);



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
