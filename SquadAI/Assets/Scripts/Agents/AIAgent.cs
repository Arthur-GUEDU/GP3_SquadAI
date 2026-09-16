using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIAgent : Agent
    {
        protected NavMeshAgent NavMeshAgentInst;
        Material MaterialInst;

        private int index;
        public int Index { get { return index; } set { index = value; } }

        [HideInInspector] public bool HasShot = false;

        [HideInInspector] public bool IsPlayerInRange = false;
        private void SetMaterial(Color col)
        {
            MaterialInst.color = col;
        }
        public void SetWhiteMaterial() { SetMaterial(Color.white); }
        public void SetRedMaterial() { SetMaterial(Color.red); }
        public void SetBlueMaterial() { SetMaterial(Color.blue); }
        public void SetYellowMaterial() { SetMaterial(Color.yellow); }

        public Transform Target;
        public PlayerCommandDispatcher PlayerCommands;
        public VirtualLeader Leader;

        #region MonoBehaviour

        private void Awake()
        {
            currentHP = maxHP;

            NavMeshAgentInst = GetComponent<NavMeshAgent>();

            Renderer rend = transform.Find("Body").GetComponent<Renderer>();
            MaterialInst = rend.material;

            gunTransform = transform.Find("Body/Gun");
            if (gunTransform == null)
                Debug.Log("could not fin gun transform");

            //NavMeshAgentInst.updatePosition = false;
        }

        private void Start()
        {
            PlayerCommands = FindAnyObjectByType<PlayerCommandDispatcher>();
            Target = PlayerCommands.transform;
        }

        private void OnTriggerEnter(Collider other)
        {
        }

        private void OnTriggerExit(Collider other)
        {
        }

        private void OnDrawGizmos()
        {
        }

        #endregion

        #region Perception methods

        #endregion

        #region MoveMethods
        public void StopMove()
        {
            if(gameObject.activeInHierarchy)
                NavMeshAgentInst.isStopped = true;
        }
        public void MoveTo(Vector3 dest)
        {
            NavMeshAgentInst.isStopped = false;
            NavMeshAgentInst.SetDestination(dest);
        }
        public virtual void Follow()
        {
            NavMeshAgentInst.isStopped = false;
            Vector3 desiredSlotPos = Leader.GetSlotWorldPosition(index);

            NavMeshAgentInst.SetDestination(desiredSlotPos);
        }
        public void SetDestination(Vector3 dest)
        {
            NavMeshAgentInst.SetDestination(dest);
        }
        public float GetRemainnigDistance()
        {
            return NavMeshAgentInst.remainingDistance;
        }
        public bool HasReachedPos()
        {
            return NavMeshAgentInst.remainingDistance - NavMeshAgentInst.stoppingDistance <= 0f;
        }

        #endregion

        #region ActionMethods

        
        public override void ShootToPosition(Vector3 pos)
        {
            // look at target position
            transform.LookAt(pos + Vector3.up * transform.position.y);
            base.ShootToPosition(pos);
            HasShot = true;
        }

        public void HealTarget(Transform target, int amount)
        {
            IDamageable targetInterface = target.GetComponent<IDamageable>();
            if(targetInterface != null)
            {
                targetInterface.AddDamage(-amount);
            }
        }
        #endregion
    }
}