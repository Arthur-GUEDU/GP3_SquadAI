using UnityEngine;

namespace AI.States
{
    public class ProtectState : StateBehaviour
    {
        public Transform ProtectTarget;
        public Transform ProtectFrom;
        public float ProtectDistance;

        AIAgent agent;
        PlayerAgent player;
        public override void Enter()
        {
            base.Enter();
            agent = Controller.Owner;
            player = agent.Target.GetComponent<PlayerAgent>();
            ProtectTarget = player.transform;
            ProtectFrom = player.lastHitBy.transform;
            player.protectedBy = agent;
            agent.MoveTo(GetTargetPos());

        }

        public override void Exit()
        {
            player.protectedBy = null;
            agent.StopMove();
            base.Exit();
        }

        public override StateBehaviour UpdateState()
        {
            if (player.lastHitBy)
            {
                ProtectFrom = player.lastHitBy.transform;
            }
            if(ProtectFrom)
            { 
                agent.MoveTo(GetTargetPos());
            }
            return base.UpdateState();
        }

        Vector3 GetTargetPos()
        {
            Vector3 dir = ProtectFrom.position - ProtectTarget.position;
            dir.Normalize();
            return ProtectTarget.position + (dir * ProtectDistance);
        }
    }
}