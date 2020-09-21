using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksManager : MonoBehaviour
{
    // Start is called before the first frame update
    Light lightComp;
    public Color[] colors;
    public int multiplier = 200;
    void Start()
    {
        lightComp = GetComponent<Light>();
        lightComp.color = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        lightComp.intensity=MicInput.MicLoudness * multiplier + Random.Range(-3f, 3f);
    }
}
