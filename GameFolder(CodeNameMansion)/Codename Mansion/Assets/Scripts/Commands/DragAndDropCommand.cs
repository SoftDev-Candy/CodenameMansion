using UnityEngine;

public class DragAndDropCommand : ICommand
{
    ItemBase item_1;
    ItemData item;
    TaskStates currentState;

    public DragAndDropCommand(ItemBase item_1, ItemData item)
    {
        this.item_1 = item_1;
        this.item = item;
        currentState = TaskStates.WAITING;
    }
    public void execute() 
    {
        currentState = TaskStates.RUNNING;
        item_1.DragAndDrop(item);
        currentState = TaskStates.COMPLETED;
        //call Item_1 interaction with Item 2
        //Item_1.DragAndDropInteraction(Item_2). Maybe something like this?
    }

    public TaskStates GetTaskState()
    {
        return currentState;
    }

    public void CheckTaskCondition()
    {

    }
}
