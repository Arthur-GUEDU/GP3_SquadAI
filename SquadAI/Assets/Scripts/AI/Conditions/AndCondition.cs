using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "AndCondition", menuName = "FSM/Condition/And")]
    public class AndCondition : StateCondition
    {
        public List<StateCondition> Conditions = new List<StateCondition>();

        public override void Init(AIAgent owner)
        {
            base.Init(owner);

            for (int i = 0; i < Conditions.Count; i++)
            {
                if (Conditions[i] != null)
                    Conditions[i].Init(owner);
            }
        }

        public override bool Validate()
        {
            for (int i = 0; i < Conditions.Count; i++)
            {
                // If the condition at index i is null, skips it, else, checks if it is validated
                if (Conditions[i] != null && !Conditions[i].Validate())
                    return false;
            }
            return true;
        }
    }
}