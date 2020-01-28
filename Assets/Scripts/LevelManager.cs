using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform playerPosition;

    void Start()
    {
        playerPosition.position = spawnPoint.position;
    }

    void Update()
    {
        if (playerPosition.position.y < -10)
            playerPosition.position = spawnPoint.position;
    }
}
