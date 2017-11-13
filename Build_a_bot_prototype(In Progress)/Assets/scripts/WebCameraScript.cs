using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this code displays the phone's camera as the background for augmented reality
/// </summary>
public class WebCameraScript : MonoBehaviour {

    private bool camAvailable;
    private WebCamTexture backCam;
    //private Texture defaultBackground;

    [SerializeField]
    RawImage background;

    AspectRatioFitter fit;

    private void Start()
    {
        //defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        //ensure that phone has operable camera
        if(devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        //locate and use the rear-facing camera 
        //later versions of this code will allow the player to select
        //which camera to use
        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if(backCam == null)
        {
            Debug.Log("Unable to find back camera");
            return;
        }

        backCam.Play();
        background.texture = backCam;

        camAvailable = true;
    }

    private void Update()
    {
        if (!camAvailable)
            return;

        //determine the screen's aspect ratio for the phone 
        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        //adjust display for phone's orientation
        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
