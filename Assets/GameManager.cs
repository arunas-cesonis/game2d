using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public TilemapCollider2D tilemapCollider2D;
    private Vector3 cameraVelocity = Vector3.zero;

    void LateUpdate()
    {
        Camera camera = Camera.main;
        Vector3 follow = player.transform.position;

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

        camera.transform.position =
            Vector3.SmoothDamp(camera.transform.position, target, ref cameraVelocity, 0.1f);
    }
}
