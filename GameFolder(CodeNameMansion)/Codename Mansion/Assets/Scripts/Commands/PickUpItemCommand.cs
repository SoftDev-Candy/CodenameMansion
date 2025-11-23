using UnityEngine;

public class PickUpItemCommand : ICommand
{
    ItemBase item;
    TaskStates currentState;
    //player

    public PickUpItemCommand(ItemBase item)
    {
        this.item = item;
        currentState = TaskStates.WAITING;
    }
   
    public void execute()
    {
        currentState = TaskStates.RUNNING;
        Debug.Log("PICKING UP");
        item.PickUp();
        currentState = TaskStates.COMPLETED;
    }

    public void CheckTaskCondition() { }

    public TaskStates GetTaskState()
    {
        return currentState;
    }
}
