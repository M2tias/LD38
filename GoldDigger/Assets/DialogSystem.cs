using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogPanel;
    private DialogMode mode;

    // Use this for initialization
    void Start()
    {
        mode = DialogMode.Start;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == DialogMode.Start)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1;
                dialogPanel.SetActive(false);
            }
        }
        if(mode == DialogMode.Shop || mode == DialogMode.Saloon)
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {

            }
        }
    }

    private void FixedUpdate()
    {
    }
}

enum DialogMode
{
    Start = 0,
    Saloon,
    Shop,
    Monolog,
    End
}