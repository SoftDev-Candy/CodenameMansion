using UnityEngine;

public class PlayingState : State
{
    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 1f; // Ensure normal game speed
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StateMachine.Singleton.SwitchState<PausedState>(); // Switch to PausedState when Escape is pressed
        }
    }
}
