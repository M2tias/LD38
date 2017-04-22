using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock3Manager : MonoBehaviour
{
    [SerializeField]
    private LayerTrigger trigger;

    //currently on the "Out"/"In" triggers
    private bool onOff = false;
    private bool onIn = false; 

    private SpriteRenderer renderer;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //stepped on the "Out" trigger
    public void OnOff()
    {
        onOff = true;
        //already on In trigger -> coming on the rock
        if (onIn)
        {
            renderer.sortingLayerName = "Bottom";

        }
        //leaving rock
        else
        {
            renderer.sortingLayerName = "Top"; //needed??
        }
    }

    //stepped off the "Out" trigger
    public void OffOut()
    {
        onOff = false;
    }

    public void OnIn()
    {
        onIn = true;
    }

    public void OffIn()
    {
        onIn = false;
    }
}
