using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "AIShootCondition", menuName = "FSM/Condition/AIShoot")]
    public class AIShootCondition : StateCondition
    {
        public override bool Validate()
        {
            return Owner.HasShot;
        }
    }
}