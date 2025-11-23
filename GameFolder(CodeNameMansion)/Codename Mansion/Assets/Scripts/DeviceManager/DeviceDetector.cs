using UnityEngine;
using UnityEngine.UI;

public class DeviceDetector : MonoBehaviour
{
    public GameObject pcUI;       // Assign the PC-specific UI layout
    public GameObject mobileUI;   // Assign the Mobile-specific UI layout

    void Start()
    {
        DetectDevice();
    }

    void DetectDevice()
    {
        if (Application.isMobilePlatform)
        {
            // Running on Mobile (Android/iOS)
            Debug.Log("Running on Mobile");
            pcUI.SetActive(false);
            mobileUI.SetActive(true);
        }
        else
        {
            // Running on PC (Windows/Mac)
            Debug.Log("Running on PC");
            pcUI.SetActive(true);
            mobileUI.SetActive(false);
        }
    }
}
