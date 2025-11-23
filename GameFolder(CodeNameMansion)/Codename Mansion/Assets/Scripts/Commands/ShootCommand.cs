using UnityEngine;

public class ShootCommand : ICommand
{
    EnemyCombat enemy;
    int totalDamage;
    TaskStates currentState;
    public ShootCommand(EnemyCombat enemy, int totalDamage )
    {
        currentState = TaskStates.WAITING;
        this.enemy = enemy;
        this.totalDamage = totalDamage;
    }
    
    public void CheckTaskCondition()
    {
        //check if shooting animation is still playing
        //change currentState to completed when animation is finished
    }

    public void execute()
    {
       
        currentState = TaskStates.RUNNING;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.gunShooting, GameManager.instance.player.transform.position);
        //turn the player towards enemy
        //play shoot animation
        enemy.TakeDamage(totalDamage);
        currentState = TaskStates.COMPLETED;
    }

    public TaskStates GetTaskState()
    {
        return currentState;
    }
}
