using System;
using UnityEngine;

namespace AI.States
{
    [Serializable]
    public class StateTransition
    {
        [HideInInspector]
        public StateBehaviour CurrentState = null;

        [SerializeField]
        private StateCondition Condition = null;

        [SerializeField]
        private StateBehaviour nextState = null;
        public StateBehaviour NextState { get { return nextState; } }

        private StateCondition ConditionInst = null;

        public int Priority;

        public void Init(FSMController controller)
        {
            if (Condition)
            {
                ConditionInst = ScriptableObject.Instantiate<StateCondition>(Condition);
                ConditionInst.Init(controller.Owner);
            }
        }

        public void Activate()
        {
            ConditionInst?.Activate();
        }

        public bool CheckCondition()
        {
            if (ConditionInst && ConditionInst.Validate())
                return true;
            return false;
        }
    }
}