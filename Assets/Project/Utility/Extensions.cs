using UnityEngine;
using UnityEngine.Assertions;

//single file for all utility extensions

/// <summary>
/// Static class containing extension methods for Unity's Transform class.
/// </summary>
public static class TransformExtension
{
    /// <summary>
    /// Sets the parent of the transform.
    /// </summary>
    /// <param name="transform">The transform being reparented.</param>
    /// <param name="parent">The parent Transform to use.</param>
    /// <param name="newScale">The localScale of the transform.</param>
    /// <param name="worldPositionStays">If true, the parent-relative position, scale and rotation is modified such that the object keeps the same world space position, rotation and scale as before.</param>
    public static void SetParent(this Transform transform, Transform parent, Vector3 newScale, bool worldPositionStays = false)
    {
        transform.SetParent(parent, worldPositionStays);
        transform.localScale = newScale;
    }

    /// <summary>
    /// Sets the parent of the transform, maintaining the same localScale.
    /// </summary>
    /// <param name="transform">The transform being reparented.</param>
    /// <param name="parent">The parent Transform to use.</param>
    public static void SetParentConstLocalScale(this Transform transform, Transform parent)
    {
        Vector3 localScale = transform.localScale;
        transform.SetParent(parent);
        transform.localScale = localScale;
    }

    /// <summary>
    /// Resets the values of the transform to the defaults. (no local offset, no local rotation, no local scale)
    /// </summary>
    /// <param name="transform">The transform to reset</param>
    public static void Reset(this Transform transform)
    {
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Resets the values of the transform to the defaults. (no local offset, no local rotation, no local scale)
    /// </summary>
    /// <param name="transform">The transform to reset</param>
    public static void Reset(this RectTransform transform) {
        transform.localScale = Vector3.one;
        transform.offsetMax = Vector2.zero;
        transform.offsetMin = Vector2.zero;
        transform.anchoredPosition = Vector2.zero;
        transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Rotates the transform so the forward vector points at worldPosition in the context of Unity 2D.
    /// </summary>
    /// <param name="self">Transform to modify.</param>
    /// <param name="worldPosition">Point to look at.</param>
    public static void LookAt(this Transform self, Vector2 worldPosition)
    {
        self.rotation = (worldPosition - (Vector2)(self.position)).ToRotation();
    }

    /// <summary>
    /// Adds a component of class T to the game object.
    /// </summary>
    /// <typeparam name="T">Component class to add.</typeparam>
    /// <param name="transform">Transform whose gameObject will recieve a new component.</param>
    /// <returns>The added component.</returns>
    public static T AddComponent<T>(this Transform transform) where T : Component
    {
        return transform.gameObject.AddComponent<T>();
    }
}

/// <summary>
/// Class containing extension methods for Unity's GameObject class.
/// </summary>
public static class GameObjectExtension
{
    /// <summary>
    /// Returns the component of type T if there exists a game object tagged with tag that has one attached, null otherwise.
    /// </summary>
    /// <typeparam name="T">The type of component to retrieve.</typeparam>
    /// <param name="tag">The name of the tag to seach GameObjects with.</param>
    /// <returns></returns>
    public static T GetComponentWithTag<T>(string tag)
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag(tag))
        {
            T result = g.GetComponent<T>();
            if (result != null)
                return result; //we've found it
        }
        return default(T);
    }
}

/// <summary>
/// Class containing extension methods related to Unity's Vector classes.
/// </summary>
public static class VectorExtension
{
    /// <summary>
    /// Converts the vector to worldspace with respect to the main camera.
    /// </summary>
    /// <param name="screenPoint">The location in screen space to convert.</param>
    /// <returns>The location in world space.</returns>
    public static Vector3 toWorldPoint(this Vector3 screenPoint)
    {
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }

    /// <summary>
    /// Converts the vector to worldspace with respect to a particular camera.
    /// </summary>
    /// <param name="screenPoint">The location in screen space to convert.</param>
    /// <param name="camera">The camera to use when converting from screen space to world space.</param>
    /// <returns>The location in world space.</returns>
    public static Vector3 toWorldPoint(this Vector3 screenPoint, Camera camera)
    {
        return camera.ScreenToWorldPoint(screenPoint);
    }

    /// <summary>
    /// Uses the main camera to convert a vector from player screen space to world space, while maintaining scaling.
    /// </summary>
    /// <param name="input">Screen space input vector.</param>
    /// <returns>World Space input vector.</returns>
    public static Vector3 ConvertFromInputToWorld(this Vector2 input)
    {
        Vector3 forward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        forward.Normalize();
        Vector3 right = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up);
        right.Normalize();
        Assert.AreApproximatelyEqual(Vector3.Dot(forward, right), 0);

        return right * input.x + forward * input.y;
    }

    /// <summary>
    /// Returns the angle of the vector in radians.
    /// </summary>
    /// <param name="dir">The vector to convert.</param>
    /// <returns>The angle in radians.</returns>
    public static float ToAngleRad(this Vector2 dir)
    {
        return Mathf.Atan2(dir.y, dir.x);
    }

    /// <summary>
    /// Returns the angle of the vector in degrees.
    /// </summary>
    /// <param name="dir">The vector to convert.</param>
    /// <returns>The angle in radians.</returns>
    public static float ToAngle(this Vector2 dir)
    {
         return dir.ToAngleRad() * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Returns the Quaternion whose forward vector is dir in the context of Unity 2D.
    /// </summary>
    /// <param name="dir">The forward vector of the resulting Quaternion.</param>
    /// <returns>The Quaternion facing in the direction of dir.</returns>
    public static Quaternion ToRotation(this Vector2 dir)
    {
        return dir.ToRotation(Vector3.forward);
    }

    /// <summary>
    /// Returns the Quaternion whose forward vector is dir, and whose up vector is axis.
    /// </summary>
    /// <param name="dir">The forward vector of the resulting Quaternion.</param>
    /// <param name="axis">The up vector of the resulting Quaternion.</param>
    /// <returns>The Quaternion facing in the direction of dir.</returns>
    public static Quaternion ToRotation(this Vector2 dir, Vector3 axis)
    {
        return Quaternion.AngleAxis(dir.ToAngle(), axis);
    }

    /// <summary>
    /// Returns the orthogonal vector.
    /// </summary>
    /// <returns>The orthogonal vector.</returns>
    public static Vector2 normal(this Vector2 dir)
    {
        return new Vector2(-dir.y, dir.x);
    }

    /// <summary>
    /// Returns the vector whose angle is angle, in degrees.
    /// </summary>
    /// <param name="angle">The angle of the resulting vector.</param>
    /// <returns>A vector whose angle is angle, in degrees.</returns>
    public static Vector2 DegreeToVector2(this float angle)
    {
        float angleRad = Mathf.Deg2Rad * angle;
        return angleRad.RadToVector2();
    }

    /// <summary>
    /// Returns the vector whose angle is angle, in radians.
    /// </summary>
    /// <param name="angle">The angle of the resulting vector.</param>
    /// <returns>A vector whose angle is angle, in radians.</returns>
    public static Vector2 RadToVector2(this float angleRad)
    {
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}