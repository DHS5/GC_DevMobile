using DG.Tweening;
using UnityEngine;

public class VFX : PoolableObject
{
    [SerializeField] private Transform vfxTransform;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private AudioData explosionAudioData;


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
        explosionParticleSystem.Play();
        AudioManager.Instance.PlayExplosionSFX(explosionAudioData);
        DOVirtual.DelayedCall(explosionParticleSystem.main.duration, Dispose);
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
