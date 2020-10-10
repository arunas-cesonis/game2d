using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class GameManager : MonoBehaviour
{
    private Player player;
    private TilemapCollider2D tilemapCollider2D;
    private List<Enemy> enemies;
    private Vector3 cameraVelocity = Vector3.zero;
    private CameraFollow cameraFollow;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        Debug.Assert(player != null, "Player not found");
        Debug.Log("Found player on scene");

        tilemapCollider2D = FindObjectOfType<TilemapCollider2D>();
        Debug.Assert(tilemapCollider2D != null, "TilemapCollider2D not found");
        Debug.Log("Found tilemap on scene");

        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
        Debug.Log("Found " + enemies.Count + " enemies on scene");

        cameraFollow = new CameraFollow(player.gameObject, Camera.main, tilemapCollider2D);
    }

    private void Start()
    {
        cameraFollow.Update(0.0f);
    }

    private void FixedUpdate()
    {
        FixedUpdateEnemies();
    }

    private void FixedUpdateEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            Vector3 d = enemy.target - enemy.transform.position;
            float distance = d.magnitude;
            if (distance > 0.1f)
            {
                d.Normalize();
                enemy.transform.position = enemy.transform.position + d * Time.fixedDeltaTime;
            }
            else
            {
                enemy.NextTarget();
            }
        }
    }

    private void LateUpdate()
    {
        cameraFollow.Update(Time.deltaTime);
    }

}
