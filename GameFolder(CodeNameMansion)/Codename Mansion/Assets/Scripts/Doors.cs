using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField]public bool open = false;
    [SerializeField]private Transform door1;
    [SerializeField]private Transform door2;
    private int door1y = 0;
    private int door2y = 0;
    public List<Button> buttons = new List<Button>();

    private EventInstance closetDoor;

    private void Start()
    {
        closetDoor = AudioManager.instance.CreateEventInstance(FMODEvents.instance.closetDoor);
    }

    void Update()
    {
        foreach(Button button in buttons)
        {
            if(button.Pressed)
            {
                open = true;
            }
            else
            {
                open = false;
                break;
            }
        }
        if(open)
        {
            closetDoor.start();
            if(door1y > -130)
            {
                door1.Rotate(0,-1,0,0);
                door1y--;
            }
            if(door2y < 130)
            {
                door2.Rotate(0,1,0,0); 
                door2y++;
            }
        }
    }
}
