using UnityEngine;

public class KeyComponent : MonoBehaviour
{
    [field : SerializeField] public int Keys { get; private set; }

    public void AddKey()
    {
        Keys++;
    }

    public void RemoveKeys(int amount)
    {
        Keys -= amount;
    }

    public void ResetKeys()
    {
        Keys = 0;
    }
}