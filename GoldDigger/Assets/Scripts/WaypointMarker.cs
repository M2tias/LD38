using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMarker : MonoBehaviour
{
    [SerializeField]
    private List<WaypointMarker> neighbours;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<WaypointMarker> GetNeighbours()
    {
        return neighbours;
    }
}
