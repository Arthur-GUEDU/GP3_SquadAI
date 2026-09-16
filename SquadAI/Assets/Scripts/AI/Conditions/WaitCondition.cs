using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "WaitCondition", menuName = "FSM/Condition/Wait")]
    public class WaitCondition : StateCondition
    {
        public float WaitDuration = 2f;
        private float startWaitTime = 0f;

        public override void Activate()
        {
            startWaitTime = Time.time;
        }

        public override bool Validate()
        {
            return Time.time - startWaitTime >= WaitDuration;
        }
    }
}