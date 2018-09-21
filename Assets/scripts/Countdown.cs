using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
    public float TimeToStart = 3;
    private float Timer;
    public Transform Canvas;
    private Text text;
    public GameObject[] ToActive;
	// Use this for initialization
	void Start () {
        text = Canvas.GetChild(0).GetComponent<Text>();
        Timer = TimeToStart;
	}

    // Update is called once per frame
    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Canvas.gameObject.SetActive(false);
            foreach (var item in ToActive)
            {
                item.SetActive(true);
            }
        }
        text.text = Mathf.CeilToInt(Timer).ToString();
    }
}
