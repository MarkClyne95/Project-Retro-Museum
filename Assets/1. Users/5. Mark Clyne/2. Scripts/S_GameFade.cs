using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines.Primitives;
using UnityEngine;
using UnityEngine.UI;

public class S_GameFade : MonoBehaviour
{
    // Start is called before the first frame update
    public void FadeOutAndIn()
    {
        S_MetroidVaniaPlayerController.instance.gameObject.transform.position = new Vector3(501, 309.779999f, 0);
    }
}
