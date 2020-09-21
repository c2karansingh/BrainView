using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Windows.WebCam;
using UnityEngine.Networking;
using TMPro;

public class PhotoCap : MonoBehaviour
{
    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;
    public GameObject quad;
    byte[] imgData;
    public string url ="";
    public GameObject detected;
    // Use this for initialization

    public IEnumerator PostImageToServer()
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", imgData, "test_image.jpg", "image/jpeg");

        var upload = UnityWebRequest.Post(url, form);
        yield return upload.SendWebRequest();

        if (upload.isHttpError)
            Debug.Log(upload.error);
        else
            Debug.Log("Uploaded Successfully");
        detected.SetActive(true);
        detected.GetComponent<TextMeshPro>().text = upload.downloadHandler.text;
        Debug.Log(upload.downloadHandler.text);    // display whether Server got the File
        
    }
    public void click()
    {
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);
        

        // Create a PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
            photoCaptureObject = captureObject;
            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

            // Activate the camera
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {
                // Take a picture
                photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
            });
        });
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        // Copy the raw image data into the target texture
        photoCaptureFrame.UploadImageDataToTexture(targetTexture);

        // Create a GameObject to which the texture can be applied
        
        Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;


        quadRenderer.material.SetTexture("_MainTex", targetTexture);

        // Deactivate the camera
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
        imgData = ImageConversion.EncodeToJPG(targetTexture);
        StartCoroutine(PostImageToServer());
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Shutdown the photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }
}