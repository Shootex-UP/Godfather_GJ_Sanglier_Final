using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Select_Level : MonoBehaviour {

    public float f_delay;
    public bool in_delay = false;
    public int lvl_selected = 1;
    public int num_lvls = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (f_delay < 1.5f)
        {
            f_delay += 0.1f;
        }
        else
        {
            if (Input.GetAxis("DPad_XAxis_1") != 0 && !in_delay)
            {
                if (Input.GetAxis("DPad_XAxis_1") < 0)
                {
                    lvl_selected--;
                    if (lvl_selected < 1)
                    {
                        lvl_selected = 1;
                    }
                    else
                    {
                        gameObject.transform.GetChild(0).transform.position = gameObject.transform.GetChild(0).transform.position + new Vector3(+700, 0, 0);
                    }
                }
                else
                {
                    lvl_selected++;
                    if (lvl_selected > num_lvls)
                    {
                        lvl_selected = num_lvls;
                    }
                    else
                    {
                        Debug.Log("Rigth");
                        gameObject.transform.GetChild(0).transform.position = gameObject.transform.GetChild(0).transform.position + new Vector3(-700,0,0);
                    }

                }
                f_delay = 0;

            }
            else if (Input.GetKeyUp(KeyCode.Joystick1Button0))
            {
                Debug.Log("Level " + lvl_selected + " selected");
                SceneManager.LoadScene("Lobby");
            }
        }

    }

    /*IEnumerator Load_Level()
    {
        SceneManager.LoadScene("Lobby");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
            Debug.Log("Loop");
        }

        Debug.Log("Fin");
    }*/
}
