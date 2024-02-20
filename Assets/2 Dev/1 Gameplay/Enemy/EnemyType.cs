using _2_Dev._1_Gameplay.Weapon;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Enemy/EnemyType", order = 0)]
public class EnemyType : ScriptableObject
{
    [SerializeField] private Enemy.EnemyMovement movement;
    [SerializeField] private Vector2 fixedRelativePosition = new Vector2(0, 0.5f);
    [SerializeField] private EnemyPath path;
    [SerializeField] private WeaponStrategy weaponStrategy;
    [SerializeField] private BulletStrategy bulletStrategy;

    public Enemy.EnemyMovement Movement => movement;
    public EnemyPath Path => path;
    public WeaponStrategy WeaponStrategy => weaponStrategy;
    public BulletStrategy BulletStrategy => bulletStrategy;
    public Vector2 FixedRelativePosition => fixedRelativePosition;
}
