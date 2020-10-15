using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    private Player player;
    private TilemapCollider2D tilemapCollider2D;
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

        cameraFollow = new CameraFollow(player.gameObject, Camera.main, tilemapCollider2D);
    }

    private void Start()
    {
        cameraFollow.Update(0.0f);
    }

    private void LateUpdate()
    {
        cameraFollow.Update(Time.deltaTime);
    }

}
