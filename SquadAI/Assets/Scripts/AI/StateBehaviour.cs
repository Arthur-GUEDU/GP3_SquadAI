using UnityEngine;
using UnityEngine.Events;

namespace AI.States
{
    public class StateBehaviour : MonoBehaviour
    {
        [SerializeField]
        private string StateName = "";
        public string Name { get { return StateName; } }

        [SerializeField]
        protected StateTransition[] Transitions;

        public StateTransition[] GetTransitions { get { return Transitions; } }

        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        public UnityEvent OnUpdate;

        protected FSMController Controller;

        public void SetController(FSMController controller)
        {
            Controller = controller;
            foreach (StateTransition transition in Transitions)
            {
                transition.Init(controller);
            }
        }

        private StateBehaviour ComputeTransition()
        {
            StateTransition currTransition = null;
            int currPriority = 0;
            for (int i = 0; i < Transitions.Length; i++)
            {
                if (Transitions[i].Priority > currPriority && Transitions[i].CheckCondition())
                {
                    currTransition = Transitions[i];
                    currPriority = currTransition.Priority;
                }
            }
            if (currTransition != null)
                return currTransition.NextState;
            else
                return this;
        }

        public virtual void Enter()
        {
            OnEnter?.Invoke();

            for (int i = 0; i < Transitions.Length; i++)
            {
                Transitions[i].Activate();
            }
        }

        public virtual void Exit()
        {
            OnExit?.Invoke();
        }

        public virtual StateBehaviour UpdateState()
        {
            OnUpdate?.Invoke();

            return ComputeTransition();
        }
    }
}