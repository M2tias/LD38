using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock3Manager : MonoBehaviour
{
    [SerializeField]
    private LayerTrigger trigger;

    [SerializeField]
    private GameObject topCollider1;
    [SerializeField]
    private GameObject topCollider2;

    //currently on the "Out"/"In" triggers
    private bool onOut = false;
    private bool onIn = false;

    private bool onRock = false;
    private bool offRock = true;

    private SpriteRenderer renderer;
    private PolygonCollider2D collider;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onRock)
        {
            collider.enabled = false;
            topCollider1.SetActive(true);
            topCollider2.SetActive(true);
            renderer.sortingLayerName = "Bottom";
            trigger.enabled = false;
            trigger.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            collider.enabled = true;
            topCollider1.SetActive(false);
            topCollider2.SetActive(false);
            //renderer.sortingLayerName = "Top"; //needed??
            trigger.enabled = true;
            trigger.GetComponent<Collider2D>().enabled = true;
        }
    }

    //stepped on the "Out" trigger
    public void OnOut()
    {
        onOut = true;
        if(onIn)
        {
            offRock = true;
            onRock = false;
        }
    }

    //stepped off the "Out" trigger
    public void OffOut()
    {
        onOut = false;
    }

    public void OnIn()
    {
        onIn = true;
        if (onOut)
        {
            offRock = false;
            onRock = true;
        }
    }

    public void OffIn()
    {
        onIn = false;
    }
}
