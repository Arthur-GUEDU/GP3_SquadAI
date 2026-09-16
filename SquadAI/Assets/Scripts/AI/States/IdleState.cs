using UnityEngine;

namespace AI.States
{
    public class IdleState : StateBehaviour
    {
        AIAgent agent;
        public override void Enter()
        {
            base.Enter();
            agent = Controller.Owner;
            agent.StopMove();
        }

        public override void Exit()
        {
            base.Exit(); 
            agent.Follow();
        }

        public override StateBehaviour UpdateState()
        {
            agent.StopMove();
            return base.UpdateState();
        }
    }
}
