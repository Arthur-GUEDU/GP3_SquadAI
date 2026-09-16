using UnityEngine;

namespace AI.States
{
    public class PatrolState : StateBehaviour
    {
        AIAgent agent;
        public Transform[] Points;

        public int destPoint = 0;
        public override void Enter()
        {
            base.Enter();
            agent = Controller.Owner;
            if (Points.Length != 0)
                Patrol();
        }

        public override void Exit()
        {
            base.Exit();
            agent.StopMove();
        }

        public override StateBehaviour UpdateState()
        {
            if (Points.Length == 0)
                return base.UpdateState();

            Patrol();

            return base.UpdateState();
        }

        private void Patrol()
        {
            // Set the agent to go to the currently selected destination.
            agent.SetDestination(Points[destPoint].position);

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            if (agent.GetRemainnigDistance() < 0.1f)
                destPoint = (destPoint + 1) % Points.Length;
        }
    }
}
