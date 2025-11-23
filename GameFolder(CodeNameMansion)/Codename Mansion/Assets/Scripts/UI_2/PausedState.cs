using UnityEngine;

public class PausedState : State
{
    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0f; // Pause the game
        UIManager.instance.ShowPauseMenu();
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1f; // Resume game
        UIManager.instance.HidePauseMenu();
    }
}
