using UnityEngine;
using UnityEngine.Events;

public class Agent : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int maxHP = 100;
    [SerializeField]
    protected GameObject BulletPrefab;
    [SerializeField]
    protected GunProperties WeaponProperties;

    protected Transform gunTransform;
    protected int currentHP;
    protected float bulletTimer = 0f;

    public bool IsDead { get; private set; } = false;
    public int MaxHP { get { return maxHP; } }
    public int CurrentHP { get { return currentHP; } }

    public UnityEvent OnPlayerDeath = new();

    void Update()
    {
        AgentUpdate();
    }

    protected virtual void AgentUpdate()
    {
        bulletTimer -= Time.deltaTime;
    }

    public virtual void ShootToPosition(Vector3 pos)
    {
        if (bulletTimer > 0)
            return;
        // instantiate bullet
        if (BulletPrefab)
        {
            GameObject bullet = Instantiate<GameObject>(BulletPrefab, gunTransform.position + transform.forward * 0.5f, Quaternion.identity);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.SetProperties(WeaponProperties);
            bulletComponent.SetOwner(gameObject);
            bulletComponent.Shoot(transform.forward * WeaponProperties.BulletSpeed);

            bulletTimer = WeaponProperties.FireRate;
        }
    }

    public virtual void AddDamage(int amount, GameObject from = null)
    {
        currentHP -= amount;
        if (currentHP <= 0 && !IsDead)
        {
            IsDead = true;
            currentHP = 0;
            OnPlayerDeath.Invoke();
            OnDeath();
        }
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }
}
