using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureTest : MonoBehaviour
{
    public void CaptureScreen()
    {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "BRUNCH-SCREENSHOT-" + timestamp + ".png";

        Debug.Log("ScreenShot");
        ScreenCapture.CaptureScreenshot(fileName);
    }
}
