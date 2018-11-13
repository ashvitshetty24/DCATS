using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DCATS.Assets.Attributes;

namespace DCATS.Assets.Physics
{
    public class UGRope : BodyBehaviour, IRope
    {
        #region HiddenPropertyVariables
        [HideInInspector] [SerializeField] Material _RopeMaterial;
        [HideInInspector] [SerializeField] int _SegmentCount;
        [HideInInspector] [SerializeField] float _Length;
        [HideInInspector] [SerializeField] float _Radius;
#if IMPLEMENTED_DENSITY
		[HideInInspector] [SerializeField] float _Density;
#else
        [HideInInspector] [SerializeField] float _Mass;
#endif
        [HideInInspector] [SerializeField] float _SpringConstant;
        [HideInInspector] [SerializeField] GameObject _object1;
        [HideInInspector] [SerializeField] GameObject _object2;
        [HideInInspector] [SerializeField] int _ColliderSkip;
        [HideInInspector] [SerializeField] float _Dampening;
        [HideInInspector] bool RopeMaterialChanged;
        [HideInInspector] bool SegmentCountChanged;
        [HideInInspector] bool LengthChanged;
        [HideInInspector] bool RadiusChanged;
#if IMPLEMENTED_DENSITY
		[HideInInspector] bool DensityChanged;
#else
        [HideInInspector] bool MassChanged;
#endif
        [HideInInspector] bool SpringConstantChanged;
        [HideInInspector] bool Object1Changed;
        [HideInInspector] bool Object2Changed;
        [HideInInspector] bool ColliderSkipChanged;
        [HideInInspector] bool DampeningChanged;

        private const float DefaultDensity = 4 * 127.324f;
        private const float DefaultRadius = 0.05f;
        private const float DefaultSpringConstant = 20.0f;

        #endregion
        private UltimateRope URope;

        [ExposeProperty]
        public GameObject Object1
        {
            get
            {
                return _object1;
            }

            set
            {
                _object1 = value;
                Object1Changed = true;
            }
        }

        [ExposeProperty]
        public GameObject Object2
        {
            get
            {
                return _object2;
            }

            set
            {
                _object2 = value;
                Object1Changed = true;
            }
        }

        [ExposeProperty]
        public Material RopeMaterial
        {
            get
            {
                return _RopeMaterial;
            }
            set
            {
                _RopeMaterial = value;
                RopeMaterialChanged = true;
            }
        }

        [ExposeProperty]
        public int SegmentCount
        {
            get
            {
                return _SegmentCount;
            }

            set
            {
                if (value > 0 && value != _SegmentCount)
                {
                    _SegmentCount = value;
                    SegmentCountChanged = true;
                }
            }
        }

        [ExposeProperty]
        public float Length
        {
            get
            {
                return _Length;
            }

            set
            {
                if (value != _Length)
                {
                    _Length = value;
                    LengthChanged = true;
                }
            }
        }

        [ExposeProperty]
        public float Radius
        {
            get
            {
                return _Radius;
            }

            set
            {
                if (value != _Radius)
                {
                    _Radius = value;
                    RadiusChanged = true;
                }
            }
        }

#if IMPLEMENTED_DENSITY

		[ExposeProperty]
		public float Density
		{
			get
			{
				if (URope != null)
				{
					var r = Radius;
					return (float)(((URope.LinkMass * URope.TotalLinks) / URope.TotalRopeLength) / (r * r * Math.PI));
				}
				else if (_Density > 0)
				{
					return _Density;
				}
				else
				{
					return DefaultDensity;
				}
			}

			set
			{
				var newDensity = value;
				if (value <= 0)
				{
					newDensity = DefaultDensity;
				}

				_Density = newDensity;
				if (URope != null)
				{
					var r = Radius;
					float newMass = CalculateLinkMass(newDensity, r);
					if (URope.LinkMass != newMass)
					{
						URope.LinkMass = newMass;
					}
					
				}
			}
		}
#else
        [ExposeProperty]
        public float Mass
        {
            get
            {
                return _Mass;
            }
            set
            {
                if (value != _Mass)
                {
                    _Mass = value;
                    MassChanged = true;
                }
            }
        }
#endif

        [ExposeProperty]
        public float SpringConstant
        {
            get
            {
                return _SpringConstant;
            }

            set
            {
                if (value != _SpringConstant)
                {
                    _SpringConstant = value;
                    SpringConstantChanged = true;
                }
            }
        }

        [ExposeProperty]
        public int ColliderSkip
        {
            get
            {
                return _ColliderSkip;
            }

            set
            {
                if (_ColliderSkip != value)
                {
                    _ColliderSkip = value;
                    ColliderSkipChanged = true;
                }
            }
        }

        [ExposeProperty]
        public float Dampening
        {
            get
            {
                return _Dampening;
            }

            set
            {
                if (value != _Dampening)
                {
                    _Dampening = value;
                    DampeningChanged = true;
                }
            }
        }

        public Vector3 Top
        {
            get
            {
                var n = Node();
                if (n != null && n.segmentLinks.Length > 0)
                {
                    return n.segmentLinks[0].transform.position;
                }
                else
                {
                    return new Vector3();
                }
            }
        }

        public Vector3 Bottom
        {
            get
            {
                var n = Node();
                if (n != null && n.segmentLinks.Length > 0)
                {
                    return n.segmentLinks.Last().transform.position;
                }
                else
                {
                    return new Vector3();
                }
            }
        }

        public UGRope()
        {
            _SpringConstant = DefaultSpringConstant;
            _Radius = DefaultRadius;
#if IMPLEMENTED_DENSITY
			_Density = DefaultDensity;
#else
            if (_Mass == 0)
            {
                _Mass = SegmentCount;
                if (_Mass == 0)
                {
                    _Mass = 1;
                }
            }
#endif
        }


        protected override void Init()
        {
            base.Init();

            URope = this.gameObject.AddComponent<UltimateRope>();
            Debug.Assert(URope != null);

            if (_RopeMaterial != null)
            {
                URope.RopeMaterial = _RopeMaterial;
            }

            Debug.Assert(Object1 != null);
            Debug.Assert(Object2 != null);




            URope.RopeType = UltimateRope.ERopeType.Procedural;
            //rope.IsExtensible = true;
            URope.RopeStart = Object1;
            if (URope.RopeNodes == null)
            {
                URope.RopeNodes = new List<UltimateRope.RopeNode>();
            }


            var new_node = new UltimateRope.RopeNode();
            URope.RopeNodes.Add(new_node);


            if (_SegmentCount >= 0)
            {
                new_node.nNumLinks = _SegmentCount;
            }
            else
            {
                new_node.nNumLinks = 1;
            }
            new_node.goNode = Object2;

            if (_Length > 0.0f)
            {
                new_node.fLength = _Length;
            }
            else
            {
                new_node.fLength = (Object1.transform.position - Object2.transform.position).magnitude + 2;
                _Length = new_node.fLength;
            }


            new_node.eColliderType = UltimateRope.EColliderType.Capsule;
            new_node.nColliderSkip = 3;
            URope.TotalLinks = new_node.nNumLinks;









            URope.CreateRopeJoints(true);
            URope.SetupRopeLinks();
            URope.SetupRopeMaterials();
            URope.SetupRopeJoints();

            RopeMaterialChanged = true;
            SegmentCountChanged = true;
            LengthChanged = true;
            RadiusChanged = true;
#if IMPLEMENTED_DENSITY
			DensityChanged = true;
#else
            MassChanged = true;
#endif
            SpringConstantChanged = true;
            Object1Changed = true;
            Object2Changed = true;
            ColliderSkipChanged = true;
            DampeningChanged = true;



            URope.AutoRegenerate = false;

            URope.Regenerate(true);



        }



        private void SetLinkCount(int n)
        {
            if (URope != null && URope.RopeNodes.Count > 0)
            {
                URope.RopeNodes[0].nNumLinks = n;
            }
        }

        private int? GetLinkCount()
        {
            if (URope != null && URope.RopeNodes.Count > 0)
            {
                return URope.RopeNodes[0].nNumLinks;
            }
            else
            {
                return null;
            }
        }

        private UltimateRope.RopeNode GetNode(int n)
        {
            if (URope != null)
            {
                if (URope.RopeNodes != null)
                {
                    if (URope.RopeNodes.Count > n)
                    {
                        return URope.RopeNodes[n];
                    }
                }
            }
            return null;
        }

        protected UltimateRope.RopeNode Node()
        {
            return GetNode(0);
        }

        private void LogMsg(string str, params object[] args)
        {
            Debug.Log(string.Format(str, args));
        }


        private float CalculateLinkMass(float density, float radius)
        {
            return (float)(radius * radius * Math.PI * (density * URope.TotalRopeLength) / URope.TotalLinks);
        }


        private void InitSpringConstant(float value)
        {
            _SpringConstant = value;
            if (URope != null)
            {
                URope.LinkJointSpringValue = value;
            }
        }


        protected override void _Update()
        {
            base._Update();
            bool needsRegenerate = false;

            if (LengthChanged)
            {
                LogMsg("Length changed.");
                needsRegenerate |= OnLengthUpdate();
            }

            if (RopeMaterialChanged)
            {
                needsRegenerate |= OnMaterialUpdate();
            }

            if (SegmentCountChanged)
            {
                LogMsg("Segment count changed.");
                needsRegenerate |= OnSegmentCountUpdate();
            }

            if (RadiusChanged)
            {
                LogMsg("Radius changed.");
                needsRegenerate |= OnRadiusUpdate();
            }

#if IMPLEMENTED_DENSITY
			if (DensityChanged)
			{
				needsRegenerate |= OnDensityUpdate();
			}
#else
            if (MassChanged)
            {
                needsRegenerate |= OnMassUpdate();
            }
#endif

            if (SpringConstantChanged)
            {
                LogMsg("Spring constant changed.");
                needsRegenerate |= OnSpringConstantUpdate();
            }

            if (Object1Changed)
            {
                needsRegenerate |= OnObject1Update();
            }

            if (Object2Changed)
            {
                needsRegenerate |= OnObject2Update();
            }

            if (ColliderSkipChanged)
            {
                needsRegenerate |= OnColliderSkipUpdate();
            }





            System.Diagnostics.Debug.Assert(!needsRegenerate);

            if (needsRegenerate)
            {
                LogMsg("Regenerating.");
                RegenerateRope();
            }

            if (URope != null)
            {
                URope.RopeMaterial = RopeMaterial;
                URope.RopeStart = Object1;
                var n = Node();
                if (n != null)
                {
                    n.goNode = Object2;
                    n.fLength = Length;
                    n.nNumLinks = SegmentCount;
                }

                if (URope.RopeDiameter != this.Radius * 2)
                {
                    //URope.ChangeRopeDiameter(Radius * 2, 1, 1);
                }
            }
        }


#pragma warning disable CS0219 // Variable is assigned but its value is never used


        protected bool OnLengthUpdate()
        {
            if (URope == null)
            {
                return false;
            }


            bool regen = false;


            Node().fLength = this._Length;
            LengthChanged = false;



            return true;
        }

        protected bool OnMaterialUpdate()
        {
            if (URope == null)
            {
                return false;
            }

            RopeMaterialChanged = false;
            if (_RopeMaterial != URope.RopeMaterial)
            {
                URope.RopeMaterial = _RopeMaterial;
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool OnSegmentCountUpdate()
        {
            if (URope == null)
            {
                return false;
            }
            bool regen = false;

            var n = Node();
            if (n == null)
            {
                return regen;
            }

            SegmentCountChanged = false;
            if (n.nNumLinks != this._SegmentCount)
            {
                n.nNumLinks = _SegmentCount;
                regen = true;
            }

            return regen;
        }

        protected bool OnRadiusUpdate()
        {
            if (URope == null)
            {
                return false;
            }
            bool regen = false;

            var skin = URope.GetComponent<SkinnedMeshRenderer>();
            Debug.Assert(skin != null);
            Debug.Assert(skin.sharedMesh != null);

            URope.RopeDiameter = 2 * _Radius;
            URope.ChangeRopeDiameter(2 * _Radius, 1, 1);
            URope.RopeDiameter = 2 * _Radius;
            RadiusChanged = false;


            return true;
        }

#if IMPLEMENTED_DENSITY
		protected bool OnDensityUpdate()
		{
			if (URope == null)
			{
				return false;
			}
			bool regen = false;


			return regen;
		}

#else

        protected bool OnMassUpdate()
        {
            if (URope == null)
            {
                return false;
            }

            float linkMass = _Mass / this.SegmentCount;
            if (URope.LinkMass != linkMass)
            {
                URope.LinkMass = linkMass;
            }

            MassChanged = false;
            return false;
        }
#endif

        protected bool OnSpringConstantUpdate()
        {
            if (URope == null)
            {
                return false;
            }
            bool regen = false;

            URope.LinkJointSpringValue = _SpringConstant;
            SpringConstantChanged = false;

            return regen;
        }

        protected bool OnObject1Update()
        {
            if (URope == null)
            {
                return false;
            }
            bool regen = false;

            URope.RopeStart = this._object1;
            this.Object1Changed = false;


            return regen;
        }

        protected bool OnObject2Update()
        {
            if (URope == null)
            {
                return false;
            }
            bool regen = false;

            var node = Node();
            if (node != null)
            {
                if (node.goNode != _object2)
                {
                    node.goNode = _object2;
                    Object2Changed = false;
                }
            }


            return regen;
        }

        protected bool OnColliderSkipUpdate()
        {
            if (URope == null)
            {
                return false;
            }

			_ColliderSkip = 3;

            var node = Node();
            if (node != null)
            {
                ColliderSkipChanged = false;
                if (node.nColliderSkip != _ColliderSkip)
                {
                    node.nColliderSkip = _ColliderSkip;

                    return true;
                }
            }

            return false;
        }

        protected bool OnDampeningUpdate()
        {
            if (URope == null)
            {
                return false;
            }

            if (URope.LinkJointDamperValue != _Dampening)
            {
                URope.LinkJointDamperValue = _Dampening;
                DampeningChanged = false;
            }

            return false;
        }

#pragma warning restore CS0219 // Variable is assigned but its value is never used

        protected void RegenerateRope()
        {
            if (URope == null)
            {
                return;
            }

            URope.Regenerate(false);
        }
    }
}
