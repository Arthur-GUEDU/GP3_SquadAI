using UnityEngine;
namespace AI.States
{
    public class CoverFireState : StateBehaviour
    {
        AIAgent Agent;
        Vector3 targetPos;
        public override void Enter()
        {
            base.Enter();
            Agent = Controller.Owner;
            Agent.StopMove();
            targetPos = Agent.PlayerCommands.coverShotTarget;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override StateBehaviour UpdateState()
        {
            Agent.ShootToPosition(targetPos);
            return base.UpdateState();
        }
    }
}