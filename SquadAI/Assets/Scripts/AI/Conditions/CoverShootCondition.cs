using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "CoverShootCondition", menuName = "FSM/Condition/CoverShoot")]
    public class CoverShootCondition : StateCondition
    {
        public override bool Validate()
        {
            return Owner.PlayerCommands.IsCoverShootOrderActive;
        }
    }
}