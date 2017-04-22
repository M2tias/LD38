using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock3TriggerIn : MonoBehaviour
{
    [SerializeField]
    private Rock3Manager rockManager;

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
            Debug.Log("OnIn");
            rockManager.OnIn();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("OffIn");
            rockManager.OffIn();
        }
    }
}
