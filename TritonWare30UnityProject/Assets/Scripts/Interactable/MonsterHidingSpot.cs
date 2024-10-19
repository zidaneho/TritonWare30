using UnityEngine;

public abstract class MonsterHidingSpot : MonoBehaviour
{
    public abstract bool ContainsMonster();
    public abstract bool HideMonster();
}