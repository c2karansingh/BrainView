using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelManager : MonoBehaviour
{
    public GameObject[] labels;
    // Start is called before the first frame update
    public void LabelToggle(int index)
    {
        if (labels[index].activeInHierarchy)
            labels[index].SetActive(false);
        else
            labels[index].SetActive(true);
    }

}
