using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Skybox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.deltaTime * 10);
    }
}
