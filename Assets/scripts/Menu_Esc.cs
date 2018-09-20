using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Esc : MonoBehaviour {

    public Toggle m_MenuToggle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show_Controllers()
    {
        Debug.Log("Showing Controllers");
    }

    public void Continue_Menu()
    {
        m_MenuToggle.isOn = !m_MenuToggle.isOn;
        Cursor.visible = m_MenuToggle.isOn;//force the cursor visible if anythign had hidden it
    }
}
