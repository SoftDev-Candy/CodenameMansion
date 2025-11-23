using UnityEngine;
using UnityEngine.AI;

public class MoveToDestination : ICommand
{
    Vector3 goalDestination;
    NavMeshAgent agent;
    TaskStates currentState;
    Animator playerAnimation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public MoveToDestination(Vector3 goalDestination, NavMeshAgent navMeshAgent,Animator animator)
    {
        this.goalDestination = goalDestination;
        agent = navMeshAgent;
        currentState = TaskStates.WAITING;
        playerAnimation = animator;
    }

    public void execute()
    {
        currentState = TaskStates.RUNNING;
        agent.SetDestination(goalDestination);
        playerAnimation.SetBool("IsMoving",true);
    }

    public void CheckTaskCondition() 
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            playerAnimation.SetBool("IsMoving", false);
            agent.ResetPath();
            currentState = TaskStates.COMPLETED;
           
        }
    }

    public TaskStates GetTaskState()
    {
        return currentState;
    }
}
