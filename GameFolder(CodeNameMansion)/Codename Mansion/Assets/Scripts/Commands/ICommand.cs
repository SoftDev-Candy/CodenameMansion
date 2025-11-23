using UnityEngine;

public interface ICommand
{
    void execute();

    TaskStates GetTaskState();

    void CheckTaskCondition();

}
