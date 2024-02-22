using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limits : MonoBehaviour
{
    [SerializeField] private Transform topLimit;
    [SerializeField] private Transform bottomLimit;
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;

    private void Start()
    {
        topLimit.SetRelativePosition(Vector2.up * 1.1f);
        bottomLimit.SetRelativePosition(Vector2.down * 1.1f);
        leftLimit.SetRelativePosition(Vector2.left * 1.1f);
        rightLimit.SetRelativePosition(Vector2.right * 1.1f);
    }
}
