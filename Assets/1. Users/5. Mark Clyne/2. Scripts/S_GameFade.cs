using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines.Primitives;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class S_GameFade : MonoBehaviour
{
    public Light2D globalLight;
    // Start is called before the first frame update
    private void Start()
    {
        globalLight.intensity = 0f;
        StartCoroutine(LightFade());
    }

    public void FadeOutAndIn()
    {
        S_MetroidVaniaPlayerController.instance.gameObject.transform.position = new Vector3(501, 309.779999f, 0);
    }

    private IEnumerator LightFade()
    {
        var current = globalLight.intensity;

        while (Math.Abs(current - 1f) > 0.001f)
        {
            current = Mathf.MoveTowards(current, 1, Time.deltaTime / 1);
            globalLight.intensity = current;
            yield return 0;
        }
    }
}
