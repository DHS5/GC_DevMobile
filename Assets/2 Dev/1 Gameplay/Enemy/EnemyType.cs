using _2_Dev._1_Gameplay.Weapon;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Enemy/EnemyType", order = 0)]
public class EnemyType : ScriptableObject
{
    [SerializeField] private EnemyPath path;
    [SerializeField] private WeaponStrategy weaponStrategy;
    [SerializeField] private BulletStrategy bulletStrategy;

    public EnemyPath Path => path;
    public WeaponStrategy WeaponStrategy => weaponStrategy;
    public BulletStrategy BulletStrategy => bulletStrategy;
}
