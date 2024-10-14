
//Monster2 will be spawned periodically.
//Monster2's spawn will trigger an event where lights will be flashing on and off, before the monster rushes the player.
//The player will have to hide while running from this loud monster.
//The monster will run over and past the player.
public class Monster2 : MonsterController
{
    public enum MonsterState { WINDUP, RUSH, END }

    public MonsterState monsterState = MonsterState.WINDUP;

    void Update()
    {
        
    }

}