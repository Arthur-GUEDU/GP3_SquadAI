using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "PlayerHealthInferiorCondition", menuName = "FSM/Condition/PlayerHealthInferior")]
    public class PlayerHealthInferiorCondition : StateCondition
    {
        /// <summary>
        /// Percentage of Player's MaxHP
        /// </summary>
        [Range(0,100)] public float HealthThreshold;

        public override bool Validate()
        {
            return Owner.PlayerCommands.Player.GetHPPercentage() <= HealthThreshold;
        }
    }
}