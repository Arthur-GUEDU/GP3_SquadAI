using UnityEngine;

namespace AI.States
{
    public abstract class StateCondition : ScriptableObject
    {
        [HideInInspector]
        protected AIAgent _Owner;

        public AIAgent Owner { get { return _Owner; } }

        public virtual void Init(AIAgent owner)
        {
            _Owner = owner;
        }

        public virtual void Activate()
        {
        }

        public abstract bool Validate();
    }
}