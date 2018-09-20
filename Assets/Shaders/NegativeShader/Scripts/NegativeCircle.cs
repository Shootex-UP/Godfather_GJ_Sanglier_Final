/*
Creates and Manages the NagativeScreenEffect
*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class NegativeCircle : MonoBehaviour {

    //this is the material that will store the NagativeSE
    public Material mat;

    public NegativeCircle() 
    {
        /*Called in Awake instead*/
        //mat = new Material(Shader.Find("Custom/NegativeScreenEffect"));
    }

    void Awake()
    {
        mat = new Material(Shader.Find("Custom/NegativeCircleEffect"));
    }

    //the Radius of the first Circle NegativeCircle
    protected float _radius1;
    public float radius1
    {
      get { return _radius1; }
      set { 
          _radius1=value;
          mat.SetFloat("_Radius1",_radius1);
        }
    }

    //the Radius of the second Circle NegativeCircle
    protected float _radius2;
    public float radius2 
    {
        get { return _radius2; }
        set { 
            _radius2=value;
            mat.SetFloat("_Radius2",_radius2);
        }
    }

    //the Maximum Radius of the NegativeCircle
    protected float _maxRadius;
    public float maxRadius 
    {
        get { return _maxRadius; }
        set { 
            _maxRadius=value;
        }
    }

    //the time the NegativeCircle starts
    private float StartTm;

    //the delay between the first and the second Circle of the NegativeCircle
    private float _delay;
    public float delay 
    {
        get { return _delay; }
        set { 
            _delay = value;
        }
    }


    //the Speed of the first Circle NegativeCircle
    private float _speed1;
    public float speed1 
    {
        get { return _speed1; }
        set { 
            _speed1 = value;
        }
    }


    //the Speed of the second Circle NegativeCircle
    private float _speed2;
    public float speed2 
    {
        get { return _speed2; }
        set { 
            _speed2=value;
        }
    }

    //the length between the First Circle and the Second.
    protected float MaxWaveSize;

    //the length between the First Circle and the Second.
    protected float _waveSize;
    public float waveSize 
    {
        get { return _waveSize; }
        set { 
            _waveSize=value;
            mat.SetFloat("_WaveSize",_waveSize);
        }
    }
        
    //the target of the NagativeSE
    private GameObject _target;
    public GameObject target 
    {
        get { return _target; }
        set { 
            _target = value;
        }
    }

    //whether or not the NagativeSE is Paused
    private bool Paused = false;

    //Pause/UnPause NagativeSE
    public void PauseToggle()
    {
        Paused = !Paused;
    }

    //Pause the NagativeSE
    public void Pause()
    {
        Paused = true;
    }

    //UnPause the NagativeSE
    public void UnPause()
    {
        Paused = false;
    }
        
    #region StartIt

    //this if for the Vector2 Position NagativeSE
    public void StartIt(Vector2 Position,float MaxRadius, float Delay, float Speed1, float Speed2 ) 
    {
        //assign values to variables
        radius1=  0.0f;
        radius2=  0.0f;
        maxRadius = MaxRadius;
        delay = Delay;
        speed1 = Speed1;
        speed2 = Speed2;

        StartTm = Time.time;

        Vector2 V2 = Camera.main.ScreenToViewportPoint(Position);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.width/(float)Screen.height );

        StartCoroutine("Processing");

    }

    //this if for the Vector3 Position NagativeSE
    public void StartIt(Vector3 Position,float MaxRadius, float Delay, float Speed1, float Speed2 ) 
    {
        //assign values to variables
        radius1=  0.0f;
        radius2=  0.0f;
        maxRadius = MaxRadius;
        delay = Delay;
        speed1 = Speed1;
        speed2 = Speed2;

        StartTm = Time.time;

        Vector2 V2 = Camera.main.WorldToViewportPoint(Position);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("Processing");

    }

    //this if for the Vector3 Position NagativeSE
    public void StartIt(Vector3 Position, bool IsScreenPosition,float MaxRadius, float Delay, float Speed1, float Speed2 ) 
    {
        //assign values to variables
        radius1 = 0.0f;
        radius2 = 0.0f;
        maxRadius = MaxRadius;
        delay = Delay;
        speed1 = Speed1;
        speed2 = Speed2;

        StartTm = Time.time;

        Vector2 V2;
        if (IsScreenPosition)
        {
            V2 = Camera.main.ScreenToViewportPoint(Position);
        }
        else
        {
            V2 = Camera.main.WorldToViewportPoint(Position);
        }


        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("Processing");

    }
        
    //this if for the Vector3 Position NagativeSE
    public void StartIt(GameObject Target,float MaxRadius, float Delay, float Speed1, float Speed2 ) 
    {
        //assign values to variables
        radius1=  0.0f;
        radius2=  0.0f;
        maxRadius = MaxRadius;
        delay = Delay;
        speed1 = Speed1;
        speed2 = Speed2;

        StartTm = Time.time;
        target = Target;

        Vector2 V2;
        V2 = Camera.main.WorldToViewportPoint(Target.transform.position);


        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("Processing");

    }


    //this if for the Vector2 Position NagativeSE
    public void StartIt(Vector2 Position,float MaxRadius, float WaveSize, float Speed) 
    {
        //assign values to variables
        radius1 =  0.0f;
        maxRadius = MaxRadius;
        speed1 = Speed;
        waveSize = WaveSize;
        MaxWaveSize = WaveSize;

        Vector2 V2 = Camera.main.ScreenToViewportPoint(Position);

        mat.SetFloat("_UseWaveSize",1f);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.width/(float)Screen.height );

        StartCoroutine("Processing2");

    }

    //this if for the Vector3 Position NagativeSE
    public void StartIt(Vector3 Position,float MaxRadius, float WaveSize, float Speed) 
    {
        //assign values to variables
        radius1 =  0.0f;
        maxRadius = MaxRadius;
        speed1 = Speed;
        waveSize = WaveSize;
        MaxWaveSize = WaveSize;

        Vector2 V2 = Camera.main.WorldToViewportPoint(Position);

        mat.SetFloat("_UseWaveSize",1f);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("Processing2");

    }

    //this if for the Vector3 Position NagativeSE
    public void StartIt(Vector3 Position, bool IsScreenPosition,float MaxRadius, float WaveSize, float Speed) 
    {
        //assign values to variables
        radius1 =  0.0f;
        maxRadius = MaxRadius;
        speed1 = Speed;
        waveSize = WaveSize;
        MaxWaveSize = WaveSize;


        Vector2 V2;
        if (IsScreenPosition)
        {
            V2 = Camera.main.ScreenToViewportPoint(Position);
        }
        else
        {
            V2 = Camera.main.WorldToViewportPoint(Position);
        }

        mat.SetFloat("_UseWaveSize",1f);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("Processing2");

    }

    //this if for the Vector3 Position NagativeSE
    public void StartIt(GameObject Target,float MaxRadius, float WaveSize, float Speed) 
    {
        //assign values to variables
        radius1 =  0.0f;
        maxRadius = MaxRadius;
        speed1 = Speed;
        waveSize = WaveSize;
        MaxWaveSize = WaveSize;

        target = Target;

        Vector2 V2;
        V2 = Camera.main.WorldToViewportPoint(Target.transform.position);

        mat.SetFloat("_UseWaveSize",1f);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("Processing2");

    }

    #endregion

    //this processes the NagativeSE over time
    private IEnumerator Processing()
    {

        while (radius2 < maxRadius * 0.995f)
        {
            if (Paused)
            {
                
            }
            else
            {
                radius1 = Mathf.Lerp(radius1,maxRadius,Time.deltaTime * speed1);

                if (Time.time - StartTm >= delay)
                {
                    radius2 = Mathf.Lerp(radius2,maxRadius * 1.1f,Time.deltaTime * speed2);
                }

                //only Move the Nagative if the Target Exists
                if (target != null)
                {
                    Vector2 V2;
                    V2 = Camera.main.WorldToViewportPoint(target.transform.position);

                    mat.SetFloat("_CenterX",V2.x);
                    mat.SetFloat("_CenterY",V2.y);
                }

            }

            yield return null;
        }

        //destory after processing
        Destroy(this);
    }


    //this processes the NagativeSE over time
    private IEnumerator Processing2()
    {

        while (radius1 < maxRadius * 0.995f)
        {
            if (Paused)
            {

            }
            else
            {
                radius1 = Mathf.Lerp(radius1,maxRadius,Time.deltaTime * speed1);

                waveSize = (1f - (Mathf.Clamp(radius1,0f,maxRadius * 1.1f)/maxRadius * 1.1f)) * MaxWaveSize ;

                //only Move the Nagative if the Target Exists
                if (target != null)
                {
                    Vector2 V2;
                    V2 = Camera.main.WorldToViewportPoint(target.transform.position);

                    mat.SetFloat("_CenterX",V2.x);
                    mat.SetFloat("_CenterY",V2.y);
                }

            }

            yield return null;
        }

        //destory after processing
        Destroy(this);
    }

    #region ReverseIt

    //this if for the Vector2 Position NagativeSE
    public void ReverseIt(Vector2 Position,float MaxRadius, float Delay, float Speed1, float Speed2 ) 
    {
        //assign values to variables
        radius1=  MaxRadius;
        radius2=  MaxRadius;
        maxRadius = MaxRadius;
        delay = Delay;
        speed1 = Speed1;
        speed2 = Speed2;

        StartTm = Time.time;

        Vector2 V2 = Camera.main.ScreenToViewportPoint(Position);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.width/(float)Screen.height );

        StartCoroutine("RevProcessing");

    }

    //this if for the Vector3 Position NagativeSE
    public void ReverseIt(Vector3 Position,float MaxRadius, float Delay, float Speed1, float Speed2 ) 
    {
        //assign values to variables
        radius1=  MaxRadius;
        radius2=  MaxRadius;
        maxRadius = MaxRadius;
        delay = Delay;
        speed1 = Speed1;
        speed2 = Speed2;

        StartTm = Time.time;

        Vector2 V2 = Camera.main.WorldToViewportPoint(Position);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("RevProcessing");

    }

    //this if for the Vector3 Position NagativeSE
    public void ReverseIt(Vector3 Position, bool IsScreenPosition,float MaxRadius, float Delay, float Speed1, float Speed2 ) 
    {
        //assign values to variables
        radius1 = MaxRadius;
        radius2 = MaxRadius;
        maxRadius = MaxRadius;
        delay = Delay;
        speed1 = Speed1;
        speed2 = Speed2;

        StartTm = Time.time;

        Vector2 V2;
        if (IsScreenPosition)
        {
            V2 = Camera.main.ScreenToViewportPoint(Position);
        }
        else
        {
            V2 = Camera.main.WorldToViewportPoint(Position);
        }


        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("RevProcessing");

    }

    //this if for the Vector3 Position NagativeSE
    public void ReverseIt(GameObject Target,float MaxRadius, float Delay, float Speed1, float Speed2 ) 
    {
        //assign values to variables
        radius1 = MaxRadius;
        radius2 = MaxRadius;
        maxRadius = MaxRadius;
        delay = Delay;
        speed1 = Speed1;
        speed2 = Speed2;

        StartTm = Time.time;
        target = Target;

        Vector2 V2;
        V2 = Camera.main.WorldToViewportPoint(Target.transform.position);


        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("RevProcessing");

    }


    //this if for the Vector2 Position NagativeSE
    public void ReverseIt(Vector2 Position,float MaxRadius, float WaveSize, float Speed)
    {
        //assign values to variables
        radius1 = MaxRadius;
        maxRadius = MaxRadius;
        speed1 = Speed;
        waveSize = WaveSize;
        MaxWaveSize = WaveSize;


        Vector2 V2 = Camera.main.ScreenToViewportPoint(Position);

        mat.SetFloat("_UseWaveSize",1f);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.width/(float)Screen.height );

        StartCoroutine("RevProcessing2");

    }

    //this if for the Vector3 Position NagativeSE
    public void ReverseIt(Vector3 Position,float MaxRadius, float WaveSize, float Speed)
    {
        //assign values to variables
        radius1 = MaxRadius;
        maxRadius = MaxRadius;
        speed1 = Speed;
        waveSize = WaveSize;
        MaxWaveSize = WaveSize;


        Vector2 V2 = Camera.main.WorldToViewportPoint(Position);

        mat.SetFloat("_UseWaveSize",1f);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("RevProcessing2");

    }

    //this if for the Vector3 Position NagativeSE
    public void ReverseIt(Vector3 Position, bool IsScreenPosition,float MaxRadius, float WaveSize, float Speed) 
    {
        //assign values to variables
        radius1 = MaxRadius;
        maxRadius = MaxRadius;
        speed1 = Speed;
        waveSize = WaveSize;
        MaxWaveSize = WaveSize;


        Vector2 V2;
        if (IsScreenPosition)
        {
            V2 = Camera.main.ScreenToViewportPoint(Position);
        }
        else
        {
            V2 = Camera.main.WorldToViewportPoint(Position);
        }

        mat.SetFloat("_UseWaveSize",1f);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("RevProcessing2");

    }

    //this if for the Vector3 Position NagativeSE
    public void ReverseIt(GameObject Target,float MaxRadius, float WaveSize, float Speed)
    {
        //assign values to variables
        radius1 =  MaxRadius;
        maxRadius = MaxRadius;
        speed1 = Speed;
        waveSize = WaveSize;
        MaxWaveSize = WaveSize;

        target = Target;

        Vector2 V2;
        V2 = Camera.main.WorldToViewportPoint(Target.transform.position);

        mat.SetFloat("_UseWaveSize",1f);

        mat.SetFloat("_CenterX",V2.x);
        mat.SetFloat("_CenterY",V2.y);
        mat.SetFloat("_ScreenRatio", (int)Screen.height/(float)Screen.width );

        StartCoroutine("RevProcessing2");

    }


    #endregion


    //this processes the NagativeSE over time
    private IEnumerator RevProcessing()
    {

        while (radius1 > 0f)
        {
            if (Paused)
            {

            }
            else
            {
                radius2 = Mathf.Lerp(radius2,-0.2f,Time.deltaTime * speed2);

                if (Time.time - StartTm >= delay)
                {
                    radius1 = Mathf.Lerp(radius1,-0.01f,Time.deltaTime * speed1);
                }

                //only Move the Nagative if the Target Exists
                if (target != null)
                {
                    Vector2 V2;
                    V2 = Camera.main.WorldToViewportPoint(target.transform.position);

                    mat.SetFloat("_CenterX",V2.x);
                    mat.SetFloat("_CenterY",V2.y);
                }

            }

            yield return null;
        }

        //destory after processing
        Destroy(this);
    }

    //this processes the NagativeSE over time
    private IEnumerator RevProcessing2()
    {

        while (radius1 > 0f)
        {
            if (Paused)
            {

            }
            else
            {
                radius1 = Mathf.Lerp(radius1,-0.2f,Time.deltaTime * speed1);

                waveSize = (1f - (Mathf.Clamp(radius1,0f,maxRadius * 1.1f)/maxRadius * 1.1f)) * MaxWaveSize ;

                //only Move the Nagative if the Target Exists
                if (target != null)
                {
                    Vector2 V2;
                    V2 = Camera.main.WorldToViewportPoint(target.transform.position);

                    mat.SetFloat("_CenterX",V2.x);
                    mat.SetFloat("_CenterY",V2.y);
                }

            }

            yield return null;
        }

        //destory after processing
        Destroy(this);
    }

    //this attaches the script to the camera
    static public NegativeCircle Get() 
    {
        NegativeCircle NC=Camera.main.gameObject.AddComponent<NegativeCircle>(); 
        return NC;
    }

    //Pauses All NagativeSE that exist
    static public void AllPause()
    {
        NegativeCircle[] AllNagatives = GameObject.FindObjectsOfType<NegativeCircle>();

        for (int i =0 ; i < AllNagatives.Length; i++)
        {
            AllNagatives[i].Pause();
        }
    }

    //UnPauses All NagativeSE that exist
    static public void AllUnPause()
    {
        NegativeCircle[] AllNagatives = GameObject.FindObjectsOfType<NegativeCircle>();

        for (int i =0 ; i < AllNagatives.Length; i++)
        {
            AllNagatives[i].UnPause();
        }
    }

    //Destory All NagativeSE that exist
    static public void DestoryAll()
    {
        NegativeCircle[] AllNagatives = GameObject.FindObjectsOfType<NegativeCircle>();

        for (int i =0 ; i < AllNagatives.Length; i++)
        {
            Destroy(AllNagatives[i]);
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest) 
    {
        if (mat == null)
        {
            return;
        }

        Graphics.Blit(src, dest, mat);
    }
}
