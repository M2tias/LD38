using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDig : MonoBehaviour
{
    [SerializeField]
    private Resources resources;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            resources.CanDig(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            resources.CanDig(false);
        }
    }
}
