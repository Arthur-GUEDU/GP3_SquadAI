using UnityEngine;

namespace AI.States
{
    public class ShootAtPlayerState : StateBehaviour
    {
        AIAgent agent;
        public Transform Target;
        public override void Enter()
        {
            base.Enter();
            agent = Controller.Owner;
            agent.ShootToPosition(Target.position);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override StateBehaviour UpdateState()
        {
            if (Target != null)
                agent.ShootToPosition(Target.position);
            return base.UpdateState();
        }

        private void Start()
        {
            Target = FindAnyObjectByType<PlayerAgent>()?.transform;
        }
    }
}
