using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "NotCondition", menuName = "FSM/Condition/Not")]
    public class NotCondition : StateCondition
    {
        public StateCondition Condition;

        public override void Init(AIAgent owner)
        {
            base.Init(owner);
            
            if (Condition != null)
                Condition.Init(owner);
        }

        public override bool Validate()
        {
            if (Condition != null)
                return !Condition.Validate();
            // if the Condition is not set, returns false
            return false;
        }
    }
}