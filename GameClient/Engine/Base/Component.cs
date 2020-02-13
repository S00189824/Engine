using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    public abstract class Component
    {
        public string ID { get; set; }
        public bool Enabled { get; set; }
        public GameObject Owner { get; set; }

        public Component()
        {
            ID = GetType().Name + Guid.NewGuid();
            Enabled = true;
        }

        public virtual void Initialize() { }
        public virtual void Initialized() { }
        public virtual void Update() { }

        public event ObjectIDHandler OnDestroy;

        public virtual void Destroy()
        {
            if(OnDestroy != null)
            {
                OnDestroy(ID);
            }
        }
    }
}
