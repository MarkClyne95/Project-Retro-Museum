using System;
using UnityEngine;
using UnityEngine.UI;

namespace _2._Shared_Scripts
{
    public class S_GameSelector : MonoBehaviour
    {
        public Image background;
        public int gameIndex;
        public S_GameScreen gameScreen;
        public Image gameImage;

        private void Start()
        {
            background = GetComponent<Image>();
            background.enabled = false;
        }

        public void OnClick()
        {
            foreach (var t in FindObjectsOfType<S_GameSelector>())
            {
                t.background.enabled = false;
            }

            gameScreen.gameImage.sprite = gameImage.sprite;
            gameScreen.index = gameIndex;
            background.enabled = true;
            gameScreen.OnChange();
        }
    }
}