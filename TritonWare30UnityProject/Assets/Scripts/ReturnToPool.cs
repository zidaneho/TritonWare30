using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ParticleSystem))]
public class ReturnToPool : MonoBehaviour
{
    private ParticleSystem system;
    public IObjectPool<GameObject> pool;

    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
        var main = system.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void Start()
    {
    }

    private void OnEnable()
    {
        system.Play();
    }

    private void OnDisable()
    {
        system.Stop();
    }

    void OnParticleSystemStopped()
    {
        // Return to the pool
        pool.Release(gameObject);
    }
}