using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow
{
    public GameObject gameObject;
    public Camera camera;
    public TilemapCollider2D tilemapCollider2D;
    private Vector3 velocity = Vector3.zero;
    public CameraFollow(GameObject gameObject, Camera camera, TilemapCollider2D tilemapCollider2D)
    {
        this.gameObject = gameObject;
        this.camera = camera;
        this.tilemapCollider2D = tilemapCollider2D;
    }
    public void Update(float dt)
    {
        camera.transform.position =
            Vector3.SmoothDamp(Camera.main.transform.position, GetTarget(), ref velocity, Time.deltaTime);
    }
    private Vector3 GetTarget()
    {
        Camera camera = Camera.main;
        Vector3 follow = gameObject.transform.position;

        Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f));
        Vector3 center = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));

        float cameraHalfWidth = Mathf.Abs(center.x - bottomLeft.x);
        float cameraHalfHeight = Mathf.Abs(center.y - bottomLeft.y);
        float minLeft = tilemapCollider2D.bounds.min.x + cameraHalfWidth;
        float minBottom = tilemapCollider2D.bounds.min.y + cameraHalfHeight;
        float maxRight = tilemapCollider2D.bounds.max.x - cameraHalfWidth;
        float x = Mathf.Min(maxRight, Mathf.Max(minLeft, follow.x));
        float y = Mathf.Max(minBottom, follow.y);

        Vector3 target = new Vector3(x, y, camera.transform.position.z);
        return target;
    }
}
