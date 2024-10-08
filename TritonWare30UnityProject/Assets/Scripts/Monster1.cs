using System.Collections;
using UnityEngine;


//Monster 1 has a patrol, windup, and rush state
//During its rush state, Monster 1 will path toward the player. Once reaching its destination, it will choose a random waypoint and start running there.
public class Monster1 : MonsterController
{
    public enum MonsterState { PATROL, WINDUP, RUSH }

    public MonsterState monsterState;
    
    [SerializeField] private float timeBetweenRush = 30f;
    [SerializeField] private float windupTime = 5f;
    [SerializeField] private float rushSpeed = 20f;

    private float timer;
    

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenRush)
        {
            timer = 0f;
            WindupRush();
        }
    }

    void WindupRush()
    {
        StartCoroutine(WindupCoroutine());
    }

    IEnumerator WindupCoroutine()
    {
        monsterState = MonsterState.WINDUP;
        yield return new WaitForSeconds(windupTime);
        //Play light events and starting sounds here.
        Rush();
    }
    
    

    void Rush()
    {
        monsterState = MonsterState.RUSH;
    }
}