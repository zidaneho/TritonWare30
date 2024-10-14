using System;
using UnityEngine;

//
public class Drawer : MonoBehaviour, IInteractable
{
    public string popupDescription => "Open";

    [SerializeField] private Transform[] lootSpawnPoints;
    [SerializeField] private LootTable lootTable;
    private Animator _animator;

    private bool opened;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnInteract(GameObject interactor)
    {
        if (opened) return;
        opened = true;
        foreach (var spawnPoint in lootSpawnPoints)
        {
            SpawnLoot(spawnPoint.position);
        }
    }

    void SpawnLoot(Vector3 position)
    {
        var itemPrefab = lootTable.GetRandomLoot().item?.worldPrefab;
        if (itemPrefab != null) Instantiate(itemPrefab, position, Quaternion.identity);
    }
}