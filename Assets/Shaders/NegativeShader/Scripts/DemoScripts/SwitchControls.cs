using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchControls : MonoBehaviour {

    public bool IsControlSet1On = true;

    public GameObject ControlSet1;
    public GameObject ControlSet2;

    public void Switch()
    {
        IsControlSet1On = !IsControlSet1On;

        if (IsControlSet1On)
        {
            ControlSet1.SetActive(true);
            ControlSet2.SetActive(false);
        }
        else
        {
            ControlSet1.SetActive(false);
            ControlSet2.SetActive(true); 
        }

    }

}
