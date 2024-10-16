using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RunParticles : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab;
    private PlayerController _player;
    private InputBank _input;

    public float timeBetweenBurst = 0.3f;
    private float _timer;
    public int maxPoolSize = 10;

    IObjectPool<GameObject> m_Pool;

    public IObjectPool<GameObject> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                m_Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 10, maxPoolSize);
            }
            return m_Pool;
        }
    }

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
        _input = GetComponentInParent<InputBank>();
    }

    private void Update()
    {
            
        
        if (_player.CurrentSpeed >= _player.RunSpeed)
        {
            _timer += Time.deltaTime;
            if (_timer > timeBetweenBurst)
            {
                _timer = 0f;
                Pool.Get();
            }
        }
    }
    GameObject CreatePooledItem()
    {
        GameObject go = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        float angle = Mathf.Rad2Deg * Mathf.Atan2(-_input.moveVector.y, -_input.moveVector.x);
        go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        // This is used to return GameObjects to the pool when they have stopped.
        var returnToPool = go.GetComponent<ReturnToPool>();
        returnToPool.pool = Pool;

        return go;
    }
    // Called when an item is returned to the pool using Release
    void OnReturnedToPool(GameObject go)
    {
        go.SetActive(false);
    }
    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(GameObject go)
    {
        float angle = Mathf.Rad2Deg * Mathf.Atan2(-_input.moveVector.y, -_input.moveVector.x);
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        go.SetActive(true);
    }
    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(GameObject go)
    {
        Destroy(go);
    }


}
