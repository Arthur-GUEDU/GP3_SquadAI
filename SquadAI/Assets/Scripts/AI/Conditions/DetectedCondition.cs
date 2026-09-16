using AI.States;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[CreateAssetMenu(fileName = "DetectedCondition", menuName = "FSM/Condition/PlayerDetected")]
public class DetectCondition : StateCondition
{
    public override bool Validate()
    {
        return Owner.IsPlayerInRange;
    }
}
