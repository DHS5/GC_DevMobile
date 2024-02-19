using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    #region Sprite Renderer

    /// <summary>
    /// Set the relative size (scale) of a SpriteRenderer
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="relativeSize">Value > 0 relative to reference resolution</param>
    /// <param name="ratio"></param>
    public static void SetRelativeSize(this SpriteRenderer spriteRenderer, float relativeSize, float ratio)
    {
        spriteRenderer.transform.localScale = Format.ComputeSize(relativeSize, ratio);
    }
    /// <summary>
    /// Set the relative position of a SpriteRenderer
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="relativePosition">Values between -1 and 1 (min/max of the screen)</param>
    public static void SetRelativePosition(this SpriteRenderer spriteRenderer, Vector2 relativePosition)
    {

    }

    public static void Move(this SpriteRenderer spriteRenderer, Vector2 relativeDelta)
    {

    }
    public static void MoveX(this SpriteRenderer spriteRenderer, float relativeXDelta)
    {

    }
    public static void MoveY(this SpriteRenderer spriteRenderer, float relativeYDelta)
    {

    }

    #endregion
}
