using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navi_Men_Esc : MonoBehaviour {


    public GameObject[] Menu_elements;
    public int menu_selected = 0;
    public bool in_delay = false;
    public float f_delay = 0;

    // Use this for initialization
    void Start()
    {
        Menu_elements[menu_selected].GetComponent<Selectable>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (f_delay <1.5f)
        {
            f_delay += 0.1f;
        }
        else
        {
            if (Input.GetAxis("DPad_YAxis_1") != 0 && !in_delay)
            {
                if (Input.GetAxis("DPad_YAxis_1") > 0)
                {
                    menu_selected--;
                    if (menu_selected < 0)
                    {
                        menu_selected = Menu_elements.Length-1;
                    }
                }
                else
                {
                    menu_selected++;
                    if (menu_selected >= Menu_elements.Length)
                    {
                        menu_selected = 0;
                    }
                    
                }
                f_delay = 0;

            }
            else if (Input.GetKeyUp(KeyCode.Joystick1Button0) && !in_delay)
            {
                Menu_elements[menu_selected].SetActive(true);
                if (menu_selected == 0)
                {
                    Debug.Log("Start");
                }
            }
        }

        Menu_elements[menu_selected].GetComponent<Selectable>().Select();

    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.2f);
        in_delay = false;
    }
}
