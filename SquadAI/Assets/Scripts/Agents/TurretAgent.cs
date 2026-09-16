using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAgent : Agent
{
    GameObject Target = null;

    public override void ShootToPosition(Vector3 pos)
    {
        // look at target position
        transform.LookAt(pos + Vector3.up * transform.position.y);
        base.ShootToPosition(pos);
    }
    void Start()
    {
        gunTransform = transform.Find("Body/Gun");
        if (gunTransform == null)
            Debug.Log("could not find gun transform");

        currentHP = MaxHP;
    }

    void Update()
    {
        AgentUpdate();
        if (Target != null)
            ShootToPosition(Target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Target == null && other.gameObject.layer == LayerMask.NameToLayer("Allies"))
        {
            Target = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Target != null && other.gameObject == Target)
        {
            Target = null;
        }
    }
}
