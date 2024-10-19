using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster3Spawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxMonsters;

    private MonsterHidingSpot[] _hidingSpots;
    private float _timer;

    private void Awake()
    {
        _hidingSpots = FindObjectsByType<MonsterHidingSpot>(FindObjectsSortMode.None);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_hidingSpots.Length > 0 && _timer >= spawnInterval)
        {
            _timer = 0f;
            HideRandomAvaliableSpot();
            
        }
    }

   void HideRandomAvaliableSpot()
   {
       List<MonsterHidingSpot> avaliableSpots = new();

       foreach (var spot in _hidingSpots)
       {
           if (spot.ContainsMonster())
           {
               avaliableSpots.Add(spot);
           }
       }

       if (avaliableSpots.Count <= 0) return;

       int randomIndex = Random.Range(0, avaliableSpots.Count);
       avaliableSpots[randomIndex].HideMonster();
   }
}