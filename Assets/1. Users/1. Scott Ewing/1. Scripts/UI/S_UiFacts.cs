using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class S_UiFacts : MonoBehaviour
{
    private TextMeshProUGUI _factsText;
    private int factsFound = 0;
    private int totalFacts = 6;
    void Start()
    {
        _factsText = GetComponent<TextMeshProUGUI>();
        EventManager.AddListener<FactFoundEvent>(OnFactFound);
    }

    private void OnDestroy() {
        EventManager.RemoveListener<FactFoundEvent>(OnFactFound);
    }

    private void OnFactFound(FactFoundEvent obj) {
        factsFound++;
        _factsText.SetText(factsFound + "/" +totalFacts);

    }

    
}
