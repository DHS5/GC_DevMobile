using _2_Dev._1_Gameplay.Colectible;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    #region Singleton

    private static CollectibleManager I { get; set; }

    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }

        I = this;

        Init();
    }

    #endregion

    [Serializable]
    private struct CollectibleDataPonderate
    {
        public CollectibleData collectibleData;
        [Min(1)] public int weight;
    }

    #region Global Members

    [SerializeField] [Range(0f, 1f)] private float collectibleChancePercentage = 0.05f;
    [SerializeField] private List<CollectibleDataPonderate> collectibleDatas;

    #endregion

    #region Initialization

    [SerializeField] private List<CollectibleData> _datas = new();

    private void Init()
    {
        foreach (var data in collectibleDatas)
            for (var i = 0; i < data.weight; i++)
                _datas.Add(data.collectibleData);
    }

    #endregion

    #region Accessor

    private CollectibleData GetRandom()
    {
        return _datas[UnityEngine.Random.Range(0, _datas.Count)];
    }

    public static CollectibleData Get()
    {
        if (UnityEngine.Random.value < I.collectibleChancePercentage)
            return I.GetRandom();
        return null;
    }

    #endregion
}