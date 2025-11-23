using UnityEngine;

public class Button : MonoBehaviour
{
    public bool Pressed = false;
    [SerializeField]private Doors Doors;
    [SerializeField]private MeshRenderer buttoncolor;
    [SerializeField]private Material PressedMaterial;
    [SerializeField]private Material unPressedMaterial;

    void OnTriggerEnter(Collider other)
    {
        Pressed = true;
        buttoncolor.material = PressedMaterial;
    }

    private void OnTriggerStay(Collider other)
    {
        Pressed = true;
        buttoncolor.material = PressedMaterial;
    }

    void OnTriggerExit(Collider other)
    {
        Pressed = false;
        buttoncolor.material = unPressedMaterial;
    }
    void Update()
    {
        if(Doors.open)
        {
            buttoncolor.material = PressedMaterial;
        }
    }
}
