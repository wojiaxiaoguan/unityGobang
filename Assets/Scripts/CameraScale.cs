using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraScale:MonoBehaviour {
    void Start() {
        int ManualWidth = 640;
        int ManualHeight = 960;
        int manualWidth;

        if (System.Convert.ToSingle(Screen.width) / Screen.height > System.Convert.ToSingle(ManualWidth) / ManualHeight)
            manualWidth = Mathf.RoundToInt(System.Convert.ToSingle(ManualHeight) / Screen.height * Screen.width);
        else
            manualWidth = ManualWidth;
        
        Camera camera = GetComponent<Camera>();

        float scale = System.Convert.ToSingle(manualWidth/ 640f);
        camera.fieldOfView*=scale;
    }
}
