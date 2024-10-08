using UnityEngine;

[CreateAssetMenu(menuName = "Create Item", fileName = "Item", order = 0)]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
}