using System;

[Serializable]
public struct ProjectileStats
{
    public float MovementSpeed;
    public int Damage;
    public int Stun;
    public float LifeTime;

    public ProjectileStats(float movementSpeed, int damage, int stun, float lifeTime)
    {
        MovementSpeed = movementSpeed;
        Damage = damage;
        Stun = stun;
        LifeTime = lifeTime;
    }
}
