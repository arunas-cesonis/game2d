using System.Collections;
using System;
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
    public GameObject enemyPrefab;

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

    private void Update()
    {
        UpdateEnemies();
    }

    private void SpawnEnemy()
    {
        GameObject o = Instantiate(enemyPrefab, new Vector3(UnityEngine.Random.Range(-5.0f, 5.0f), 3.0f, 0.0f), Quaternion.identity);
        Enemy e = o.GetComponent<Enemy>();
        enemies.Add(e);
    }

    private void UpdateEnemies()
    {
        List<Enemy> newEnemies = new List<Enemy>();
        foreach(Enemy enemy in enemies)
        {
            if (enemy.touchingBlade)
            {
                Destroy(enemy.gameObject);
                continue;
            }

            Vector3 d = enemy.target - enemy.transform.position;
            float distance = d.x;
            if (Mathf.Abs(distance) > 0.2f)
            {
                enemy.characterController.Move(distance);
            }
            else
            {
                enemy.NextTarget();
            }

            newEnemies.Add(enemy);
        }
        enemies = newEnemies;
        if(Input.GetKeyDown(KeyCode.F))
        {
            SpawnEnemy();
        }
    }

    private void LateUpdate()
    {
        cameraFollow.Update(Time.deltaTime);
    }

}
