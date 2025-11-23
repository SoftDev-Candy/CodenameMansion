using System.Xml.Linq;
using UnityEngine;

public class InteractItem : ICommand
{
    ItemBase item;
    TaskStates currentState;

    public InteractItem(ItemBase item) 
    {
        this.item = item;
        currentState = TaskStates.WAITING;
    }

    public void execute()
    {
        currentState = TaskStates.RUNNING;
        item.Interact();
        currentState = TaskStates.COMPLETED;
    }


    public TaskStates GetTaskState()
    { 
        return currentState;
    }

    public void CheckTaskCondition()
    {

    }

}
