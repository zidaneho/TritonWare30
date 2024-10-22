using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Monster2Spawner : MonoBehaviour
{
    [SerializeField] private GameObject monster;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private Transform[] startWaypoints;
    private Light2D[] lights;
    [SerializeField] private float minTimeBetweenFlicker = 0.25f;
    [SerializeField] private float maxTimeBetweenFlicker = 0.4f;
    [SerializeField] private int flickerCount = 7; 

    private float timer;
    private GameObject currentMonsterInstance;

    private void Awake()
    {
        lights = FindObjectsOfType<Light2D>();
    }

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
            StartCoroutine(FlickerLightsCoroutine());
            if (monster != null) currentMonsterInstance = Instantiate(monster, GetRandomWaypoint(), Quaternion.identity);
            
        }
    }

    Vector2 GetRandomWaypoint()
    {
        if (startWaypoints.Length <= 0) return Vector2.zero;
        var randomIndex = Random.Range(0, startWaypoints.Length);
        return startWaypoints[randomIndex].position;
    }

    IEnumerator FlickerLightsCoroutine()
    {
        for (int i = 0; i < flickerCount; i++)
        {
            foreach (var light2D in lights)
            {
                if (light2D.lightType != Light2D.LightType.Global) light2D.enabled = !light2D.enabled;
            }
            var randomFlickerTime = Random.Range(minTimeBetweenFlicker,maxTimeBetweenFlicker);
            yield return new WaitForSeconds(randomFlickerTime);
        }
    }
}