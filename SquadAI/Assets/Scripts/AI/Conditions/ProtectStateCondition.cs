using AI.States;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "ProtectStateCondition", menuName = "FSM/Condition/ProtectState")]
    public class ProtectStateCondition : StateCondition
    {
        public override bool Validate()
        {
            PlayerAgent player = Owner.Target.GetComponent<PlayerAgent>();

            //not protected by anyone, but was hit by someone who is not dead
            return player.protectedBy == null && player.lastHitBy != null && !player.lastHitBy.GetComponent<Agent>().IsDead;
        }
    }
}
