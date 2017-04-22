using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogPanel;
    [SerializeField]
    private GameObject introPanel;
    [SerializeField]
    private GameObject saloonPanel;
    [SerializeField]
    private GameObject shopPanel;
    [SerializeField]
    private Resources resources;

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
        GameObject panel = null;
        GameObject menuCursor = null;
        if (mode == DialogMode.Start)
        {
            Time.timeScale = 0;
            dialogPanel.SetActive(true);
            introPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                mode = DialogMode.None;
            }
        }
        else if (mode == DialogMode.Shop || mode == DialogMode.Saloon)
        {
            Time.timeScale = 0;
            if (mode == DialogMode.Shop)
            {
                panel = shopPanel;
            }
            else if (mode == DialogMode.Saloon)
            {
                panel = saloonPanel;
            }
            panel.SetActive(true);
            dialogPanel.SetActive(true);

            foreach (Transform child in panel.transform)
            {
                if (child.tag == "MenuCursor")
                {
                    menuCursor = child.gameObject;
                }
            }
            if (menuCursor == null) return;

            float x = menuCursor.GetComponent<RectTransform>().anchoredPosition.x;
            float y = menuCursor.GetComponent<RectTransform>().anchoredPosition.y;
            if (Input.GetKeyDown(KeyCode.UpArrow) && y > -90f)
            {
                menuCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, -21f);
            }
            else if ((Input.GetKeyDown(KeyCode.DownArrow) && y > -50f) || (Input.GetKeyDown(KeyCode.UpArrow) && y < -90f))
            {
                menuCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, -63f);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                menuCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, -101f);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (y == -21f)
                {
                    //thing 1
                }
                else if( y == -63f)
                {
                    //thing 2
                }
                else
                {
                    mode = DialogMode.None;
                    resources.CanSaloon(false);
                    resources.CanShop(false);
                }
            }
        }
        else if (mode == DialogMode.None)
        {
            Time.timeScale = 1;
            dialogPanel.SetActive(false);
            introPanel.SetActive(false);
            shopPanel.SetActive(false);
            saloonPanel.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
    }

    public void SetMode(DialogMode mode)
    {
        this.mode = mode;
    }
}

public enum DialogMode
{
    Start = 0,
    Saloon,
    Shop,
    Monolog,
    End,
    None
}