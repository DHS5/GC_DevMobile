using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    #region Editor

#if UNITY_EDITOR

    private void OnValidate()
    {
        Vector3 poolPos = poolPosition.position;

        if (pooledSpriteRenderers.IsValid())
        {
            foreach (var sr in pooledSpriteRenderers)
            {
                sr.transform.position = poolPos;
            }
        }
    }

#endif

    #endregion

    #region Utility

    private static bool Exist()
    {
        if (I != null) return true;

        Debug.LogError("Pool doesn't exist in scene");
        return false;
    }

    #endregion

    #region Singleton

    private static Pool I { get; set; }

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

    #region Global Members

    [Header("Pool")]
    [SerializeField] private Transform poolPosition;

    [Header("SpriteRenderer Pool")]
    [SerializeField] private List<SpriteRenderer> pooledSpriteRenderers;

    #endregion

    #region Initialization

    private static Stack<SpriteRenderer> _spriteRendererStack;

    private void Init()
    {
        // Stack Sprite Renderer
        if (pooledSpriteRenderers.IsValid())
        {
            foreach (var psr in pooledSpriteRenderers)
            {
                _spriteRendererStack.Push(psr);
            }
        }
    }

    #endregion

    #region Pooling

    public static SpriteRenderer GetSpriteRenderer()
    {
        if (_spriteRendererStack.Count > 0)
        {
            return _spriteRendererStack.Pop();
        }
        else
        {
            Debug.LogError("Sprite Renderer Stack empty");
            return null;
        }
    }
    public static void DisposeSpriteRenderer(SpriteRenderer spriteRenderer)
    {
        if (Exist() && spriteRenderer != null)
        {
            spriteRenderer.transform.position = I.poolPosition.position;
            _spriteRendererStack.Push(spriteRenderer);
        }
    }

    #endregion
}
