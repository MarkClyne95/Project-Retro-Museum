using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using UnityEngine;

public class S_FactItem : MonoBehaviour
{
    public void Pickup() {
        EventManager.Broadcast(Events.ItemPickedUpEvent);
        EventManager.Broadcast(Events.FactFoundEvent);
    }
}
