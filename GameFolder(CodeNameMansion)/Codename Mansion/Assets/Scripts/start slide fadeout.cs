using UnityEngine;
using UnityEngine.UI;

public class startslidefadeout : MonoBehaviour
{
    [SerializeField]private float fadespeed = 0.005f;
    private float fade = 1;
    [SerializeField]private Image startImage;
    void Update()
    {
        startImage.color = new Color(1,1,1,fade);
        fade -= fadespeed;
        
        if(fade < 0)
        {
            gameObject.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            fade = -1;
        }
    }
}
