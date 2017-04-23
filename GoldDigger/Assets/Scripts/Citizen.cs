﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Citizen : MonoBehaviour
{
    [SerializeField]
    private WaypointMarker currentMarker;
    [SerializeField]
    private Text thoughts;
    [SerializeField]
    private Resources resources;
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject saloon;
    [SerializeField]
    [Range(0.5f, 5f)]
    private float speed = 1f;
    [SerializeField]
    [Range(1f, 30f)]
    private float waitTime = 5f;
    [SerializeField]
    [Range(1f, 30f)]
    private float randomTalk = 5f;
    private float randomTalkTime = 0f;

    private float startedWaiting;
    private Vector3 velocity = Vector3.zero;

    private bool waiting = false;
    private GameObject target;

    // Use this for initialization
    void Start()
    {
        target = player.gameObject;
        randomTalkTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (target == player.gameObject)
        {
            float currentDistance = Vector3.Distance(currentMarker.transform.position, transform.position);
            resources.TickChaseSuspicion(currentDistance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - randomTalkTime > randomTalk && target == player.gameObject)
        {
            if(thoughts.text == "")
            {
                thoughts.text = "What is he up to?";
            }
            else
            {
                thoughts.text = "";
            }
            randomTalkTime = Time.time;
        }
        if(target == player.gameObject && player.IsFishing())
        {
            target = saloon;
            thoughts.text = "Oh! He's just fishing";
            resources.ReduceSuspicion(2);
        }
        else if (target == player.gameObject && player.IsDigging())
        {
            target = saloon;
            thoughts.text = "Oh boy! I've got to tell others about this!";
            resources.ReduceSuspicion(-15);
        }

        if (waiting && target == saloon)
        {
            Destroy(gameObject);
        }
        if(waiting && (Time.time - startedWaiting) > waitTime)
        {
            thoughts.text = "He's boring...";
            waiting = false;
            target = saloon;
        }

        float currentDistance = Vector3.Distance(currentMarker.transform.position, transform.position);
        if (currentDistance > 0.2f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, currentMarker.transform.position, ref velocity, 0.3f, speed);
        }
        else
        {
            WaypointMarker closest = currentMarker;
            float minDistance = Vector3.Distance(closest.transform.position, target.transform.position);
            foreach(WaypointMarker marker in currentMarker.GetNeighbours())
            {
                float distance = Vector3.Distance(marker.transform.position, target.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = marker;
                }
            }

            if(closest == currentMarker && !waiting)
            {
                waiting = true;
                startedWaiting = Time.time;
                Debug.Log("FUCK THIS");
            }

            currentMarker = closest;
        }
    }
}
