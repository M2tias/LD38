using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenFactor : MonoBehaviour
{
    [SerializeField]
    private Citizen citizenPrefab;
    [SerializeField]
    private WaypointMarker startMarker;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Saloon saloon;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Create(DialogSystem ds, Resources r)
    {
        Citizen newCitizen = Instantiate(citizenPrefab, transform, false) as Citizen;
        newCitizen.Init(startMarker, ds, r, player, saloon);
    }
}
