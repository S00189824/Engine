using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    public class GameObject
    {
        public string ID { get; set; }
        public event ObjectIDHandler OnDestroy;
        public Scene Scene { get; set; }
        public bool Enabled { get; set; }
        public Matrix World { get; set; }
        public Vector3 Location { get { return World.Translation; } }
        public Vector3 Sacle { get { return World.Scale; } }

        public Quaternion Rotation { get { return World.Rotation; } }

        public GameObject Parent { get; set; }
        public List<GameObject> Children { get; set; }

        private List<Component> components = new List<Component>();
        public List<Component> Components { get { return components; } }

        private List<string> AwaitingRemoval = new List<string>();
        private bool isInitialized = false;
        public bool IsInitialized { get { return isInitialized; } }

        public GameObject()
        {
            ID = this.GetType().Name + Guid.NewGuid();
            Enabled = true;
            World = Matrix.Identity;
            Children = new List<GameObject>();

        }

        public GameObject(Vector3 location)
        {
            ID = this.GetType().Name + Guid.NewGuid();
            Enabled = true;
            World = Matrix.Identity * Matrix.CreateTranslation(location);
            Children = new List<GameObject>();
        }

        public virtual void Initialize()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Initialize();
            }
            isInitialized = true;
        }

        public virtual void Initialized()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Initialize();
            }
            
        }

        public void Update()
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].Enabled)
                    components[i].Update();
            }


            for (int i = 0; i < AwaitingRemoval.Count; i++)
            {
                RemoveComponent(AwaitingRemoval[i]);
            }
        }

        List<RenderComponent> renderComponents;
        public void Draw(CameraComponent camera)
        {
            renderComponents = components.OfType<RenderComponent>().ToList();

            for (int i = 0; i < renderComponents.Count; i++)
            {
                if (renderComponents[i].Enabled)
                    renderComponents[i].Draw(camera);
            }
            renderComponents.Clear();
        }


        public void AddComponent(Component component)
        {
            if(component != null)
            {
                component.Owner = this;

                if(isInitialized)
                {
                    component.Initialized();
                    component.Initialized();
                }

                component.OnDestroy += Component_OnDestroy;
                components.Add(component);
            }
            renderComponents.Clear();
        }

        private void Component_OnDestroy(string ID)
        {
            AwaitingRemoval.Add(ID);
            
        }

        
        public void Destroy()
        {
            components.Clear();

            if(OnDestroy!= null)
            {
                OnDestroy(ID);
            }
            
        }

        public void RemoveComponent(string id)
        {
            //find the index of the component with the id
            int index = components.FindIndex(c => c.ID == id);
            //removeComponent (int index)

            RemoveComponent(index);
        }

        void RemoveComponent(int index)
        {
            ////remove component at specified index
            components.RemoveAt(index);
        }

        public void RemoveComponent(Component component)
        {
            //remove component that matches the component passed in
            components.Remove(component);
        }

        public float GetDistance(GameObject otherObject)
        {
            return Vector3.Distance(World.Translation, otherObject.World.Translation);
        }

        public bool HasComponent<T>() where T : Component
        {
            return components.Any(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T)));
        }

        public Component GetComponent(string id)
        {
            return components.Find(c => c.ID == id);//returns null if not found
        }

        public Component GetComponent<T>() where T : Component
        {
            return (T)components.Find(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T)));
        }

        public List<T> GetComponents<T>() where T : Component
        {
            return components.FindAll(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T))) as List<T>;
        }
    }
}
