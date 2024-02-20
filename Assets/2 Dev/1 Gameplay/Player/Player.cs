using _2_Dev._1_Gameplay;
using UnityEngine;

public class Player : Plane, IDamageable
{
    protected override void Die()
    {
        // TODO: Implement VFX?  Freeze Controls?
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
