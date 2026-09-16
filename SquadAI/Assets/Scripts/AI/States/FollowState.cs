using UnityEngine;

namespace AI.States
{
    public class FollowState : StateBehaviour
    {
        public Transform FollowTarget;
        AIAgent agent;
        public override void Enter()
        {
            base.Enter();
            agent = Controller.Owner;
            agent.Follow();
        }

        public override void Exit()
        {
            base.Exit();
            agent.StopMove();
        }

        public override StateBehaviour UpdateState()
        {
            agent.Follow();
            return base.UpdateState();
        }
    }
}