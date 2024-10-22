using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster4Spawner : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private float timeBetweenSpawns = 60f;
    [SerializeField] private float maxDistance = 25f;
    [SerializeField] private int maxInstances = 3;
    [SerializeField] private Transform[] waypoints;
    private PlayerController _player;
    private List<GameObject> _currentInstances = new List<GameObject>();
    private float _timer;

    void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        
        foreach (var currentInstance in _currentInstances)
        {
            if (currentInstance != null && Vector2.Distance(transform.position, _player.transform.position) > maxDistance)
            {
                Destroy(currentInstance);
                _timer = 0f;
            }
        }

        if (_currentInstances.Count < maxInstances)
        {
            _timer += Time.deltaTime;
            List<int> waypointIndexes = new List<int>();
            for(int i = 0; i < waypoints.Length; i++)
            {
                waypointIndexes.Add(i);
            }

            if (_timer >= timeBetweenSpawns)
            {
                var waypointIndex = Random.Range(0, waypointIndexes.Count);
                waypointIndexes.Remove(waypointIndexes[waypointIndex]);
                _currentInstances.Add(Instantiate(monsterPrefab, waypoints[waypointIndexes[waypointIndex]].position, Quaternion.identity));
            }
        }
    }

    Vector2 GetRandomWaypointIndex()
    {
        var waypointIndex = Random.Range(0, waypoints.Length);
        return waypoints[waypointIndex].position;
    }
}
