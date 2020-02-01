using System;

[Serializable]
public struct ProjectileStats
{
    public readonly float MovementSpeed;
    public readonly int Damage;
    public readonly int Stun;
    public readonly bool IsPiercing;
    public float LifeTime;

    public ProjectileStats(float movementSpeed, int damage, int stun, 
        float lifeTime, bool isPiercing = false)
    {
        MovementSpeed = movementSpeed;
        Damage = damage;
        Stun = stun;
        LifeTime = lifeTime;
        IsPiercing = isPiercing;
    }
}
