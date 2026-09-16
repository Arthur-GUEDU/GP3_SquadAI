using UnityEngine;

namespace AI.States
{
    public class HealState : StateBehaviour
    {
        public float HealFromDistance = 10f;
        public int HealAmount = 20;
        public float HealInterval = 1f;

        float healTimer = 0f;
        Transform healTarget;
        AIAgent agent;
        public override void Enter()
        {
            base.Enter();
            agent = Controller.Owner;
            agent.Follow();
            healTarget = agent.Target;
        }

        public override void Exit()
        {
            agent.StopMove();
            base.Exit();
        }

        public override StateBehaviour UpdateState()
        {
            if(CanHeal())
            {
                agent.StopMove();
                agent.HealTarget(healTarget, HealAmount);
                healTimer = Time.time;
            }
            else
            { 
                agent.MoveTo(healTarget.position); 
            }
            return base.UpdateState();
        }

        bool CanHeal()
        {
            return (Vector3.Distance(healTarget.position, agent.transform.position) <= HealFromDistance &&
                Time.time - healTimer > HealInterval);
        }
    }
}
