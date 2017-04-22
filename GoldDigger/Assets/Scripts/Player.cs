using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Resources resources;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float speed = 1f;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float speedFactor = 0.5f;

    private float goldDigTime = 0f;
    private bool diggin = false;
    [SerializeField]
    [Range(0f, 100f)]
    private float maxGoldDigTime = 20f;


    private Vector2 targetSpeed;
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer renderer;

    // Use this for initialization
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float move_h = 0;
        float move_v = 0;
        goldDigTime++;

        if (diggin)
        {
            if (goldDigTime > maxGoldDigTime)
            {
                diggin = false;
                goldDigTime = 0;
                renderer.enabled = true;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                move_h = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                move_h = 1;
            }
            else
            {
                move_h = 0;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                move_v = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                move_v = -1;
            }
            else
            {
                move_v = 0;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                move_v = 0;
                move_h = 0;
                if (resources.DigGold())
                {
                    diggin = true;
                    goldDigTime = 0;
                    renderer.enabled = false;
                    Debug.Log("Diggin' sum gold");
                }
                else if(resources.RefineGold())
                {
                    diggin = true;
                    goldDigTime = 0;
                    renderer.enabled = false;
                    Debug.Log("Refinin' sum gold");
                }
                else if (resources.Fish())
                {
                    diggin = true;
                    goldDigTime = 0;
                    //renderer.enabled = false;
                    Debug.Log("Fishin'");
                }
                else if (resources.Fry())
                {
                    diggin = true;
                    goldDigTime = 0;
                    //renderer.enabled = false;
                    Debug.Log("Ahh, food!");
                }
            }
        }

        targetSpeed = new Vector2(speed * move_h, speed * move_v);
        rigidBody2D.AddForce(speedFactor * (targetSpeed - rigidBody2D.velocity), ForceMode2D.Impulse);

    }
}
