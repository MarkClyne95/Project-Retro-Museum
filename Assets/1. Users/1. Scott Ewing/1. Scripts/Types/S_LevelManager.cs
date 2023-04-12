using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class S_LevelManager : MonoBehaviour{
    [SerializeField] private int qualityLevel = 5;
    [SerializeField] private int width = 1920;
    [SerializeField] private int height = 1440;
    
    void Start() {
        Screen.SetResolution(width, height, true);
        QualitySettings.SetQualityLevel(qualityLevel);
    }
}
