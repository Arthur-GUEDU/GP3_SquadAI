using UnityEngine;

namespace AI.States
{
    public class FSMController : MonoBehaviour
    {
        [SerializeField]
        float UpdateFrequency = 0.1f;
        [SerializeField]
        bool EnableLog = false;

        float nextUpdateTime = 0.0f;
        public StateBehaviour CurrentState { get; private set; }

        StateBehaviour previousState;
        StateBehaviour[] States;

        public AIAgent Owner { get; set; }

        private void Start()
        {
            States = GetComponentsInChildren<StateBehaviour>();
            if (States.Length == 0)
                return;

            Owner = GetComponentInParent<AIAgent>();
            Owner.OnPlayerDeath.AddListener(OnOwnerDeath);

            foreach (StateBehaviour state in States)
            {
                state.SetController(this);
            }

            CurrentState = States[0];
            if (CurrentState != null)
                CurrentState.Enter();

            nextUpdateTime = Time.time + UpdateFrequency;
        }

        private void Update()
        {
            // custom update rate
            if (Time.time < nextUpdateTime)
                return;
            nextUpdateTime = Time.time + UpdateFrequency;

            if (CurrentState == null)
                return;

            StateBehaviour newState = CurrentState.UpdateState();
            if (newState != CurrentState)
            {
                CurrentState.Exit();
                newState.Enter();

                if (EnableLog)
                    Debug.Log(string.Format("Transiting from {0} to {1}", CurrentState.Name, newState.Name));

                previousState = CurrentState;
                CurrentState = newState;
            }
        }

        private void OnOwnerDeath()
        {
            CurrentState.Exit();
        }
    }
}