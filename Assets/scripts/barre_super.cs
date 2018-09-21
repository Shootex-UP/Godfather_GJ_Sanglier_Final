using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barre_super : MonoBehaviour {

    private float current_fill = 0;
    public GameObject GameOver_Line;


    // Use this for initialization
    void Start () {
        current_fill = GetComponent<Image>().fillAmount;

    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Image>().fillAmount < current_fill)
        {
            GetComponent<Image>().fillAmount += 0.005f;

        }

        //SUPER
        if (Input.GetKeyUp(KeyCode.Joystick1Button3) && current_fill >=1f)
        {
            GetComponent<Image>().fillAmount = 0;
            current_fill = 0;
            StartCoroutine(reset_game_over_line());

        }
    }

    public void remplisage()
    {
        current_fill += 0.1f;
    }

    IEnumerator reset_game_over_line()
    {
        GameOver_Line.GetComponent<HorsZone>().enabled = false;
        yield return new WaitForSeconds(3);
        GameOver_Line.GetComponent<HorsZone>().enabled = true;
    }
}
