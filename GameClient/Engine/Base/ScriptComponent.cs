using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    class ScriptComponent : Component
    {
        public ObjectIDHandler OnComplete;
        public ScriptComponent() { }
        public ScriptComponent(string id) : base() { }

        public virtual bool HasCompleted()
        {
            return false;
        }

        
    }
}
