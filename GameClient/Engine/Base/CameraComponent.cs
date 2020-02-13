using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    public abstract class CameraComponent : Component
    {
        public Matrix View { get; set; }
        public Matrix projection { get; set; }

        public BoundingFrustum frustum { get { return new BoundingFrustum(View * projection); } }

        public float NearPlane { get; set; } = 0.1f;
        public float FarPlane { get; set; } = 1000f;
        public Vector3 CurrentTarget { get; set; }
        public Vector3 CameraDirection { get; set; }
        public Vector3 UpVector { get; set; }
        public CameraComponent(string id) : base() { }
        public CameraComponent() : base() { }

        //public Vector3 UP = Vector3.Up;

        public override void Initialize()
        {
            base.Initialize();  
        }
    }
}
