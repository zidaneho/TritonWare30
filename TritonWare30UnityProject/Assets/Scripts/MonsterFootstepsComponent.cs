using FMODUnity;
using Pathfinding;
using UnityEngine;

public class MonsterFootstepsComponent : MonoBehaviour
{
    [SerializeField] private EventReference footStepsSoundEvent;
    [SerializeField] private float footstepTime;
    private float _footStepsTimer;

    private IAstarAI _ai;
    // Start is called before the first frame update
    private void Awake()
    {
        _ai = GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFootsteps();
    }
    void UpdateFootsteps()
    {
        //Player isnt moving, return
        if (!_ai.canMove)
        {
            _footStepsTimer = 0f;
            return;
        }

        
        _footStepsTimer += Time.deltaTime;
        if (_footStepsTimer >= footstepTime)
        {
            _footStepsTimer = 0f;
            Util.PlaySound(footStepsSoundEvent.Path,gameObject);
        }
    }
}