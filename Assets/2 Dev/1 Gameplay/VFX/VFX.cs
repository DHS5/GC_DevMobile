using DG.Tweening;
using UnityEngine;

public class VFX : PoolableObject
{
    private static int explosionTriggerHash = Animator.StringToHash("Explosion");

    [SerializeField] private Transform vfxTransform;
    [SerializeField] private Animator animator;


    #region Accessor

    public static VFX Spawn(Vector3 position)
    {
        VFX vfx = Pool.Get<VFX>(Pool.PoolableType.VFX);
        vfx.Init(position);
        return vfx;
    }

    #endregion

    public void Init(Vector3 position)
    {
        MoveTo(position);
        animator.SetTrigger(explosionTriggerHash);
        AudioManager.Instance.PlayExplosionSFX();
        DOVirtual.DelayedCall(1f, Dispose);
    }

    public void Dispose()
    {
        Pool.Dispose(this, Pool.PoolableType.VFX);
    }


    public override void MoveTo(Vector3 position)
    {
        vfxTransform.position = position;
    }
}
