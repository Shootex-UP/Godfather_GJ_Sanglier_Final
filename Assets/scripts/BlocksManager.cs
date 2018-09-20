using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour {
    //Highest Block
    private Transform HighestBlock;
    public float HighestBlockValue;

    //Camera
    private Transform MainCamera;
    public float CameraMovementSpeed;
    public float CameraOffsetY;
    private void Start()
    {
        HighestBlock = null;
        HighestBlockValue = 0f;
        MainCamera = GameObject.Find("Main Camera").transform;
        CameraOffsetY = MainCamera.position.y;
    }

    // Update is called once per frame
    void Update () {
        HighestBlockValue = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform CurrentChild = transform.GetChild(i);
            if (CurrentChild.position.y > HighestBlockValue && CurrentChild.tag == "landed")
            {
                HighestBlock = CurrentChild;
                HighestBlockValue = CurrentChild.position.y;
            }
        }
        Vector3 pos = new Vector2(0, HighestBlockValue + CameraOffsetY);
        pos.y = Mathf.Lerp(MainCamera.position.y, pos.y, CameraMovementSpeed);
        MainCamera.position = new Vector3(MainCamera.position.x, pos.y, MainCamera.position.z);
    }

    public float GetHighestValue()
    {
        return HighestBlockValue;
    }
}
