using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerAgent : Agent
{
    private Rigidbody rb;

    public ParticleSystem HealParticle;

    public UnityEvent<Vector3> OnAim = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> OnShoot = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> OnCoverShoot = new UnityEvent<Vector3>();
    public UnityEvent OnCancel = new UnityEvent();

    public GameObject lastHitBy { get; private set; }

    [HideInInspector] public AIAgent protectedBy;

    public float GetHPPercentage()
    {
        return currentHP / maxHP * 100;
    }

    public void Heal(int _hp)
    {
        currentHP += _hp;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public override void AddDamage(int amount, GameObject from = null)
    {
        base.AddDamage(amount, from);
        if(amount < 0 && HealParticle)
        {
            Instantiate(HealParticle, transform);
        }
        lastHitBy = from;
    }

    public Vector3 CurrentVelocity { get; private set; }
    public void AimAtPosition(Vector3 pos)
    {
        if (Vector3.Distance(transform.position, pos) > 2.5f)
            transform.LookAt(pos + Vector3.up * transform.position.y);
        OnAim.Invoke(pos);
    }

    public override void ShootToPosition(Vector3 pos)
    {
        base.ShootToPosition(pos);
        OnShoot.Invoke(pos);
    }

    public void NPCShootToPosition(Vector3 pos)
    {
        OnCoverShoot.Invoke(pos);
    }

    public void MoveToward(Vector3 velocity)
    {
        CurrentVelocity = velocity;
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    #region MonoBehaviour Methods
    void Start()
    {
        currentHP = maxHP;
        gunTransform = transform.Find("Gun");
        rb = GetComponent<Rigidbody>();
    }

    void OnDestroy()
    {
        OnShoot.RemoveAllListeners();
        OnCoverShoot.RemoveAllListeners();
        OnCancel.RemoveAllListeners();
    }

    #endregion

    public override void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
