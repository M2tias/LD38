﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock3TriggerOut : MonoBehaviour
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
            Debug.Log("OnOut");
            rockManager.OnOut();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("OffOut");
            rockManager.OffOut();
        }
    }
}
