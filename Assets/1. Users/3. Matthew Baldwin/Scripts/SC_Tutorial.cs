using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Tutorial : MonoBehaviour
{
    [SerializeField] private SC_SwitchLevel level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishTutorial()
    {
        level.NextScene();
    }
}
