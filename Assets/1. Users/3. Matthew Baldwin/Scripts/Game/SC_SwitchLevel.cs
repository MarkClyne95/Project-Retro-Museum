using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SwitchLevel : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private GameObject level3;

    [Header("InfoScreens")]
    [SerializeField] private GameObject info1;
    [SerializeField] private GameObject info2;
    [SerializeField] private GameObject info3;

    [Header("ActiveLevel")]
    [SerializeField] private int levelIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NextScene()
    {
        switch (levelIndex)
        {
            case 1:
                //Menu to Level 1
                menu.SetActive(false);
                level1.SetActive(true);
                levelIndex = 2;
                break;
            case 2:
                //Level 1 to Info 1
                level1.SetActive(false);
                info1.SetActive(true);
                levelIndex = 3;
                break;
            case 3:
                //Info 1 to Level 2
                info1.SetActive(false);
                level2.SetActive(true);
                levelIndex = 4;
                break;
            case 4:
                //Level 2 to Info 2
                level2.SetActive(false);
                info2.SetActive(true);
                levelIndex = 5;
                break;
            case 5:
                //Info 2 to Level3
                info2.SetActive(false);
                level3.SetActive(true);
                levelIndex = 6;
                break;
            case 6:
                //Level3 to Info3
                level3.SetActive(false);
                info3.SetActive(true);
                levelIndex = 7;
                break;
            case 7:
                //Info3 to Menu
                info3.SetActive(false);
                menu.SetActive(true);
                levelIndex = 1;
                break;
            case 0:
                Debug.Log("Its 0");
                break;
        }
    }

    public void FullReset()
    {
        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
        info1.SetActive(false);
        info2.SetActive(false);
        info3.SetActive(false);

        menu.SetActive(true);
        levelIndex = 1;
    }
}
