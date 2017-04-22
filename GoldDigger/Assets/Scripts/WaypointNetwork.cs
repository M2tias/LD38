using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNetwork : MonoBehaviour
{
    private List<WaypointMarker> markers;
    private List<WaypointMarker> drawn;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (drawn == null)
        {
            drawn = new List<WaypointMarker>();
        }
        
        //if (markers == null)
        //{
            //Debug.Log("wew");
            markers = new List<WaypointMarker>();
            foreach (Transform child in transform)
            {
                if (child.tag == "WaypointMarker")
                {
                    markers.Add(child.gameObject.GetComponent<WaypointMarker>());
                }
            }
        //}

        foreach (WaypointMarker mark in markers)
        {
            List<WaypointMarker> ns = mark.GetNeighbours();
            //Gizmos.DrawSphere(new Vector3(2f, 0f, 0f), 1);

            foreach (WaypointMarker n in ns)
            {
                if(!n.GetNeighbours().Contains(mark))
                {
                    n.AddNeighbour(mark);
                }

                if (!drawn.Contains(n))
                {
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawLine(mark.transform.position, n.transform.position);
                }
            }
        }
    }
}
