using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject buttonClick;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(buttonClick != null)
            buttonClick.SetActive(true);
    }

    private void OnDisable()
    {
        if (buttonClick != null)
            buttonClick.SetActive(false);
    }
}
