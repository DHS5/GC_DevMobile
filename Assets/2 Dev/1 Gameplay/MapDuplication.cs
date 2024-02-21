using DG.Tweening;
using UnityEngine;

public class MapDuplication : MonoBehaviour
{
    [SerializeField] private GameObject map;
    [SerializeField] private float speed = 2f;
    public float mapHeight = 20f;

    private void Start()
    {
        StartMovement();
    }

    private void StartMovement()
    {
        map.transform.DOMoveY(-mapHeight, mapHeight / speed).SetEase(Ease.Linear).OnComplete(RestartMovement);
    }

    private void RestartMovement()
    {
        map.transform.position = Vector3.forward;
        StartMovement();
    }
}