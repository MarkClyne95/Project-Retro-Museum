using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(SceneManager.GetActiveScene().name == "Temple")
        {
            SceneManager.LoadScene("Jungle");
        }

        if (SceneManager.GetActiveScene().name == "Jungle")
        {
            SceneManager.LoadScene(1);
        }
    }
}
