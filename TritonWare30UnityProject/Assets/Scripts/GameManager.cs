using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Monster2Spawner[] monster2Spawners;
    [SerializeField] private Monster3Spawner[] monster3Spawners;
    public const string jumpScareSoundEvent = "event:/jumpscare";
    public const string idleSoundEvent = "event:/idle";
    public const  string chaseSoundEvent = "event:/chase";
    public const string flickerLightsSoundEvent = "event:/flickering_light";

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void KillMonsterSpawners()
    {
        foreach (var spawner in monster2Spawners)
        {
            Destroy(spawner.gameObject);
        }

        foreach (var spawner in monster3Spawners)
        {
            Destroy(spawner.gameObject);
        }
    }

    public void KillAllMonsters()
    {
        var monsters = FindObjectsOfType<MonsterController>();
        foreach (var monster in monsters)
        {
            Destroy(monster.gameObject);
        }
    }
    
    
}
