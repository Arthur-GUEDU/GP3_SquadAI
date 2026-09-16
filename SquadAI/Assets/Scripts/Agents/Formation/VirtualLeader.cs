using AI;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class VirtualLeader : AIAgent
{
    [SerializeField]
    private AIAgent[] AIAgents;

    private Transform target;
    private Collider playerCollider;

    [SerializeField]
    private float followDistance = 2f;
    [SerializeField]
    private float spacing = 2f;



    private void Awake()
    {
        NavMeshAgentInst = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        PlayerAgent player = FindAnyObjectByType<PlayerAgent>();

        for (int i = 0; i < AIAgents.Length; i++)
        {
            AIAgents[i].Index = i;
        }

        if (player != null)
        {
            target = player.transform;
            playerCollider = player.GetComponent<Collider>();
        }
        NavMeshAgentInst.autoBraking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            IsPlayerInRange = true;
            for (int i = 0; i < AIAgents.Length; i++)
            {
                AIAgents[i].IsPlayerInRange = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            IsPlayerInRange = false;
            for (int i = 0; i < AIAgents.Length; i++)
            {
                AIAgents[i].IsPlayerInRange = false;
            }
        }
    }
    public Vector3 GetSlotWorldPosition(int index)
    {
        int row = index / 3;
        int column = index % 3;

        float x = (column - 1) * spacing;
        float z = -row * spacing;

        Vector3 formationOffSet = new Vector3(x, 0, z);

        Vector3 worldOffset = transform.right * formationOffSet.x + transform.forward * formationOffSet.z;

        return transform.position + worldOffset;
    }

    public override void Follow()
    {
        if (target == null)
            return;

        NavMeshAgentInst.isStopped = false;

        Vector3 desiredPos = target.position - target.forward * followDistance;
       
        NavMeshAgentInst.SetDestination(desiredPos);
        NavMeshAgentInst.transform.LookAt(target);
    }

    public override void AddDamage(int amount, GameObject from = null)
    {
        
    }
}