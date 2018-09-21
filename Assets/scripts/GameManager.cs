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
        public int idlevel;
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
            idlevel = 1;
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
            //DisplayEndCanvas();
            EndText.text = "Win";
            EndText.color = Color.green;
            string NextLevel = "Niveau_";
            string currentlevelname = SceneManager.GetActiveScene().name;
            idlevel ++;
            Debug.Log(idlevel);
            Transform PlayerManager = GameObject.Find("PlayerManager").transform;
            for (int i = 0; i < PlayerManager.childCount; i++)
            {
                Destroy(PlayerManager.GetChild(i).gameObject);
            }
            SceneManager.LoadScene(NextLevel+(idlevel));
        }

        private void DisplayEndCanvas()
        {
            EndCanvas.gameObject.SetActive(true);
        }
    }
}

