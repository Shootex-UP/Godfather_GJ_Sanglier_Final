using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {
    public GameObject[] ObjectList;//Objets Possible

    private SpriteRenderer CurrentRenderSprite;//Variable du sprite render;
    //public Sprite CurrentRenderSprite;
    private int CurrentRenderId;//Defini la piece a affiché

    private float LoadingTime;//le temps de selection
    //
    public bool CanBeTake;//La piece peut être prise par un joueur

    // Use this for initialization
    void Start ()
    {
        CurrentRenderSprite = transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        CurrentRenderId = Random.Range(0, ObjectList.Length);
        SpriteUpdate();
        //InvokeRepeating("randomizeSprite", 2.0f, 0.3f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //if (CanBeTake)
            //{
            //Crée une instance a l'anchor du player
            // OBJECT
            Transform anchor = other.transform.parent.Find("anchor");
            Anchor AnchorComponent = anchor.GetComponent<Anchor>();
            if (!AnchorComponent.Holding)
            {
                GameObject object_gererated = Instantiate(ObjectList[CurrentRenderId], anchor.position, Quaternion.identity, anchor);
                AnchorComponent.Holding = true;
                AnchorComponent.HoldingBlock = object_gererated.transform;
                AnchorComponent.AnchorBlock();
                CurrentRenderId = Random.Range(0, ObjectList.Length);
                SpriteUpdate();
            }
            //  object_gererated.SetActive(true);

            //reset Object id

            // SPRITE

            //CanBeTake = false;
            //Invoke("reset_transparency", 5f);
            //}
        }
    }

    public void reset_transparency()
    {
        Color sprite_color = CurrentRenderSprite.color;
        CurrentRenderSprite.color = new Color(r: sprite_color.r, g: sprite_color.g, b: sprite_color.b, a: 1f);
        CanBeTake = true;
    }

    private void SpriteUpdate()
    {
        CurrentRenderSprite.sprite = ObjectList[CurrentRenderId].GetComponent<SpriteRenderer>().sprite;
        Color sprite_color = ObjectList[CurrentRenderId].GetComponent<SpriteRenderer>().color;
        CurrentRenderSprite.color = new Color(sprite_color.r, sprite_color.g, sprite_color.b, .5f);
    }
}
