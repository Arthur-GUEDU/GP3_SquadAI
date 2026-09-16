using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "XorCondition", menuName = "FSM/Condition/Xor")]
    public class XorCondition : StateCondition
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
            bool isValid = false;

            for (int i = 0; i < Conditions.Count; i++)
            {
                // If the condition at index i is null, skips it, else, ckecks if it is validated
                if (Conditions[i] != null && Conditions[i].Validate())
                {
                    if (isValid)
                        return false;
                    else
                        isValid = true;
                }
            }
            return isValid;
        }
    }
}