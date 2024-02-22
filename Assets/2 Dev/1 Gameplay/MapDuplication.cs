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

    private void OnEnable()
    {
        GameManager.OnGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        GameManager.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        _movementTween.Kill();
    }

    Tween _movementTween;
    private void StartMovement()
    {
        _movementTween = map.transform.DOMoveY(-mapHeight, mapHeight / speed).SetEase(Ease.Linear).OnComplete(RestartMovement);
    }

    private void RestartMovement()
    {
        _movementTween.Kill();
        map.transform.position = Vector3.forward;
        StartMovement();
    }
}