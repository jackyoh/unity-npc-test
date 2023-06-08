using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTest : MonoBehaviour {
    
    void Start() {
        ScreenCapture.CaptureScreenshot("/home/user1/test.png");
    }

}
