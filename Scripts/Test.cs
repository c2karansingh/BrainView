using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Test : MonoBehaviour
{

    public string screenShotURL = "http://44103bf4.ngrok.io/";

    // Use this for initialization
    void Start()
    {
        StartCoroutine(UploadPNG());
    }

    IEnumerator UploadPNG()
    {
        // We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

       
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

       
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        
        WWWForm form = new WWWForm();
        form.AddField("frameCount", Time.frameCount.ToString());
        form.AddBinaryData("fileUpload", bytes, "screenShot.png", "image/png");
        
        
        using (var w = UnityWebRequest.Post(screenShotURL, form))
        {
            yield return w.SendWebRequest();
            if (w.isNetworkError || w.isHttpError)
            {
                print(w.error);
            }
            else
            {
                print("Finished Uploading Screenshot");
            }
        }
    }
}