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

    public void DeactivateLevel()
    {
        switch (levelIndex)
        {
            case 1:
                menu.SetActive(false);
                break;
            case 2:
                level1.SetActive(false);
                break;
            case 3:
                level2.SetActive(false);
                break;
            case 4:
                level3.SetActive(false);
                break;
            case 5:
                info1.SetActive(false);
                break;
            case 6:
                info2.SetActive(false);
                break;
            case 7:
                info3.SetActive(false);
                break;
            case 0:
                Debug.Log("Its 0");
                break;
        }
    }

    #region Activate Levels
    public void ShowMenu()
    {
        DeactivateLevel();
        menu.SetActive(true);
        levelIndex = 1;
    }

    public void ShowLevel1()
    {
        DeactivateLevel();
        level1.SetActive(true);
        levelIndex = 2;
    }

    public void ShowLevel2()
    {
        DeactivateLevel();
        level2.SetActive(true);
        levelIndex = 3;
    }

    public void ShowLevel3()
    {
        DeactivateLevel();
        level3.SetActive(true);
        levelIndex = 4;
    }

    public void ShowInfo1()
    {
        DeactivateLevel();
        info1.SetActive(true);
        levelIndex = 5;
    }

    public void ShowInfo2()
    {
        DeactivateLevel();
        info2.SetActive(true);
        levelIndex = 6;
    }

    public void ShowInfo3()
    {
        DeactivateLevel();
        info3.SetActive(true);
        levelIndex = 7;
    }
    #endregion
}
