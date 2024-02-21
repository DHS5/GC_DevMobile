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
    /// <param name="transform"></param>
    /// <param name="relativeSize">Value > 0 relative to reference resolution</param>
    /// <param name="ratio"></param>
    public static void SetRelativeSize(this Transform transform, float relativeSize, float ratio)
    {
        transform.localScale = Format.ComputeSize(relativeSize, ratio);
    }
    /// <summary>
    /// Set the relative position of the SpriteRenderer
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="relativePosition">Values between -1 and 1 (min/max of the screen)</param>
    public static void SetRelativePosition(this Transform transform, Vector2 relativePosition)
    {
        transform.position = Format.ComputePosition(relativePosition);
    }
    /// <summary>
    /// Set the position of the SpriteRenderer given a Screen Position
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="screenPos"></param>
    public static void SetPositionFromScreenPosition(this Transform transform, Vector2 screenPos)
    {
        transform.position = Format.ComputePositionFromScreenPosition(screenPos);
    }

    /// <summary>
    /// Move the SpriteRenderer given a relative delta vector
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="relativeDelta">Values between -1 and 1 (min/max of the screen)</param>
    public static void Move(this Transform transform, Vector2 relativeDelta)
    {
        transform.position += Format.ComputePosition(relativeDelta);
    }
    public static void Move(this Rigidbody2D rigidbody2D, Vector2 relativeDelta)
    {
        Format.MoveRigidBodyOptimized(rigidbody2D, relativeDelta);
    }
    
    public static void MovePlayerClamp(this Transform transform, Vector2 relativeDelta)
    {
        var pos = Format.ComputePosition(relativeDelta);
        var newPos = transform.position + pos;
        var x = Format.DemiBounds.x * .83f;
        var y = Format.DemiBounds.y * .75f;
        newPos.x = Mathf.Clamp(newPos.x, -x, x);
        newPos.y = Mathf.Clamp(newPos.y, -y, y);
        transform.position = newPos;
    }
    public static void MoveX(this Transform transform, float relativeXDelta)
    {
        transform.position += Format.ComputePosition(relativeXDelta, 0);
    }
    public static void MoveY(this Transform transform, float relativeYDelta)
    {
        transform.position += Format.ComputePosition(0, relativeYDelta);
    }

    #endregion
}
