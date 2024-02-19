using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Enemy/EnemyType", order = 0)]
public class EnemyType : ScriptableObject
{
    public GameObject enemyPrefab;
    public GameObject weaponPrefab;
    public float speed;

}
