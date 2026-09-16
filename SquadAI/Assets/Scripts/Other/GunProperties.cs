using UnityEngine;

[CreateAssetMenu(fileName = "GunProperties", menuName = "Scriptable Objects/GunProperties")]
public class GunProperties : ScriptableObject
{
    [Tooltip("Fires one bullet every x seconds")]
    public float FireRate = 0.5f;
    public int Damage = 10;
    public float BulletSpeed = 1000f;
    public Color trailColor = Color.white;
}
