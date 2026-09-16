using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "PlayerShootCondition", menuName = "FSM/Condition/PlayerShoot")]
    public class PlayerShootCondition : StateCondition
    {
        public override bool Validate()
        {
            return Owner.PlayerCommands.IsShootOrderActive;
        }
    }
}