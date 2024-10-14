using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster2Spawner : MonoBehaviour
{
    [SerializeField] private GameObject monster;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private Transform[] startWaypoints;

    private float timer;
    private GameObject currentMonsterInstance;

    private void Update()
    {
        timer += Time.deltaTime;

        if (currentMonsterInstance != null)
        {
            timer = 0f;
        }

        if (timer >= timeBetweenSpawns)
        {
            timer = 0f;
            currentMonsterInstance = Instantiate(monster, GetRandomWaypoint(), Quaternion.identity);
            
        }
    }

    Vector2 GetRandomWaypoint()
    {
        var randomIndex = Random.Range(0, startWaypoints.Length);
        return startWaypoints[randomIndex].position;
    }
}