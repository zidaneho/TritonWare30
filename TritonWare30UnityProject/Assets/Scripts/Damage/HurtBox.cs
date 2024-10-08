using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public HealthComponent healthComponent { get; private set; }
    public TeamComponent teamComponent { get; private set; }

    private void Awake()
    {
        healthComponent = GetComponentInParent<HealthComponent>();
        teamComponent = GetComponentInParent<TeamComponent>();
    }
}