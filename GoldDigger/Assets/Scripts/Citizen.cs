using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    [SerializeField]
    private WaypointMarker currentMarker;
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

    private float startedWaiting;
    private Vector3 velocity = Vector3.zero;

    private bool waiting = false;
    private GameObject target;

    // Use this for initialization
    void Start()
    {
        target = player.gameObject;
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
        if(waiting && target == saloon)
        {
            Destroy(gameObject);
        }
        if(waiting && (Time.time - startedWaiting) > waitTime)
        {
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
