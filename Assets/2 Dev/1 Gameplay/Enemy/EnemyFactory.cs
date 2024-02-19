using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyFactory
{
    public GameObject CreateEnemy(EnemyType enemyType, SplineContainer spline)
    {
        EnemyBuilder builder = new EnemyBuilder()
            .SetBasePrefab(enemyType.enemyPrefab)
            .SetSpline(spline)
            .SetSpeed(enemyType.speed);

        // do Weapons

        return builder.Build();
    }
}
