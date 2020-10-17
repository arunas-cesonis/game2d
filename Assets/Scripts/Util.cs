using UnityEngine;
public class Util
{
    public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance)
    {
        Debug.DrawRay(origin, direction * distance, Color.red);
        return Physics2D.Raycast(origin, direction, distance);
    }
    public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, LayerMask layerMask)
    {
        Debug.DrawRay(origin, direction * distance, Color.red);
        return Physics2D.Raycast(origin, direction, distance, layerMask);
    }
    public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, LayerMask layerMask)
    {
        Debug.DrawRay(origin, direction * distance, Color.red);
        return Physics2D.RaycastAll(origin, direction, distance, layerMask);
    }
}
