using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField]
    private Player player;
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

    public void Landed()
    {
        player.gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            resources.Win();
        }
    }

    /*void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            resources.CanDig(false);
        }
    }*/
}
