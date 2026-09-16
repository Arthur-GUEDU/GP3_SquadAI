using UnityEngine;

namespace AI.States
{
    public class FollowAndShootState : StateBehaviour
    {
        Vector3 shootTarget;
        AIAgent agent;

        public override void Enter()
        {
            base.Enter();
            agent = Controller.Owner;
            agent.HasShot = false;
            agent.Follow();
            shootTarget = agent.PlayerCommands.shotTarget;
        }

        public override void Exit()
        {
            base.Exit();
            agent.StopMove();
        }

        public override StateBehaviour UpdateState()
        {
            agent.Follow();
            agent.ShootToPosition(shootTarget);
            return base.UpdateState();
        }
    }
}