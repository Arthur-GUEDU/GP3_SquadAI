using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Duration = 2f;
    public ParticleSystem shotParticle;

    private Rigidbody rb;
    private GunProperties gunProperties;
    private GameObject owner;
    private int ownerLayer;
    void Start()
    {
        Destroy(gameObject, Duration);
        rb = GetComponent<Rigidbody>();   
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != ownerLayer)
        {    
            IDamageable damagedAgent = collision.gameObject.GetComponentInParent<IDamageable>();
            if (damagedAgent == null)
                damagedAgent = collision.gameObject.GetComponent<IDamageable>();
            damagedAgent?.AddDamage(gunProperties.Damage, owner);
        }
        Destroy(gameObject);
    }

    public void Shoot(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(direction);
        if (shotParticle)
        {
            ParticleSystem particle = Instantiate<ParticleSystem>(shotParticle, transform.position, transform.rotation);
            particle.transform.forward = direction.normalized;
        }
    }

    public void SetProperties(GunProperties gunProperties)
    {
        this.gunProperties = gunProperties;

        TrailRenderer lineRenderer = GetComponent<TrailRenderer>();
        lineRenderer.startColor = gunProperties.trailColor;
        lineRenderer.endColor = gunProperties.trailColor;
    }

    public void SetOwner(GameObject _owner)
    {
        owner = _owner;
        gameObject.layer = owner.layer;
        ownerLayer = owner.layer;
        SetRigidbodyIgnoreLayers();
    }

    private void SetRigidbodyIgnoreLayers()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //make mask from layer of owner
        LayerMask ignoreLayers = 1 << owner.layer;

        rb.excludeLayers = ignoreLayers;
    }
}
