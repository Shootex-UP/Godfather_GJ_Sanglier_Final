using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : MonoBehaviour {
        public Transform EndCanvas;
        private Text EndText;
        private static bool created = false;

        void Awake()
        {
            if (!created)
            {
                DontDestroyOnLoad(this.gameObject);
                created = true;
            }
        }

        private void Start()
        {
            EndText = EndCanvas.Find("EndText").GetComponent<Text>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
                SceneManager.LoadScene("Lobby");
        }

        public void Loose()
        {
            DisplayEndCanvas();
            EndText.text = "Game Over";
            EndText.color = Color.red;
            Time.timeScale = 0;
        }

        public void Win()
        {
            DisplayEndCanvas();
            EndText.text = "Win";
            EndText.color = Color.green;
            Time.timeScale = 0;
        }

        private void DisplayEndCanvas()
        {
            EndCanvas.gameObject.SetActive(true);
        }
    }
}

