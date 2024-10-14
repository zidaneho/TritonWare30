using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Create LootTable", fileName = "LootTable", order = 0)]
public class LootTable : ScriptableObject
{
    public Loot[] items;

    public Loot GetRandomLoot()
    {
        var totalWeight = 0f;
        foreach (var loot in items)
        {
            totalWeight += loot.weight;
        }

        var randomValue = Random.Range(0, totalWeight);
        foreach (var loot in items)
        {
            Debug.Log(loot.item.itemName + " " +loot.weight + " " + randomValue);
            if (loot.weight < randomValue)
            {
                return loot;
            }

            randomValue -= loot.weight;
        }
        Debug.LogError("Could not find loot");
        return null;
    }
        
    [Serializable]
    public class Loot
    {
        public Item item;
        public int amount;
        public float weight;
    }
}
