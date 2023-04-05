using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Destroy : MonoBehaviour
{
  public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
}
