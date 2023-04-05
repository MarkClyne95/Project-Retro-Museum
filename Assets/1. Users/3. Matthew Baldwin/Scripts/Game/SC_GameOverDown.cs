using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_GameOverDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
        this.gameObject.GetComponent<Animator>().SetTrigger("GameOver");
    }

    public void TextOff() => this.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;

    private void OnEnable()
    {
        SC_EventManager.OnGameOver += Spawn;
    }

    private void OnDisable()
    {
        SC_EventManager.OnGameOver -= Spawn;
    }
}
