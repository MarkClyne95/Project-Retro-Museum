using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SC_Credits : MonoBehaviour
{
    public string levelName;
    [SerializeField] private VideoPlayer vPlayer;
    // Start is called before the first frame update
    void Start()
    {
        vPlayer.loopPointReached += VidOver;
    }

    void VidOver(VideoPlayer vp)
    {
        SceneManager.LoadScene(levelName);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene(levelName);
    }

    private void OnDisable()
    {
        vPlayer.loopPointReached -= VidOver;
    }
}
