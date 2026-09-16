using AI.States;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "ProtectStateCondition", menuName = "FSM/Condition/ProtectStateExit")]
    public class ProtectStateExitCondition : StateCondition
    {
        public override bool Validate()
        {
            PlayerAgent player = Owner.Target.GetComponent<PlayerAgent>();

            return player.lastHitBy == null || player.lastHitBy.GetComponent<Agent>().IsDead;
        }
    }
}