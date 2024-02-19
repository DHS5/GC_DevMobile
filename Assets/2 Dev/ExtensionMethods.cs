using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    #region Collections

    public static bool IsValid(this IList list)
    {
        return list != null && list.Count > 0;
    }

    #endregion

    #region Vectors

    public static Vector3 ToVector3(this Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0);
    }

    #endregion

    #region Sprite Renderer

    /// <summary>
    /// Set the relative size (scale) of the SpriteRenderer
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="relativeSize">Value > 0 relative to reference resolution</param>
    /// <param name="ratio"></param>
    public static void SetRelativeSize(this SpriteRenderer spriteRenderer, float relativeSize, float ratio)
    {
        spriteRenderer.transform.localScale = Format.ComputeSize(relativeSize, ratio);
    }
    /// <summary>
    /// Set the relative position of the SpriteRenderer
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="relativePosition">Values between -1 and 1 (min/max of the screen)</param>
    public static void SetRelativePosition(this SpriteRenderer spriteRenderer, Vector2 relativePosition)
    {
        spriteRenderer.transform.position = Format.ComputePosition(relativePosition);
    }

    public static void MoveTo(this SpriteRenderer spriteRenderer, Vector3 worldPos)
    {
        spriteRenderer.transform.position = Format.ComputePosition(worldPos.x, worldPos.y);
    }

    /// <summary>
    /// Move the SpriteRenderer given a relative delta vector
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="relativeDelta">Values between -1 and 1 (min/max of the screen)</param>
    public static void Move(this SpriteRenderer spriteRenderer, Vector2 relativeDelta)
    {
        spriteRenderer.transform.position += Format.ComputePosition(relativeDelta);
    }
    public static void MoveX(this SpriteRenderer spriteRenderer, float relativeXDelta)
    {
        spriteRenderer.transform.position += Format.ComputePosition(relativeXDelta, 0);
    }
    public static void MoveY(this SpriteRenderer spriteRenderer, float relativeYDelta)
    {
        spriteRenderer.transform.position += Format.ComputePosition(0, relativeYDelta);
    }

    #endregion
}
