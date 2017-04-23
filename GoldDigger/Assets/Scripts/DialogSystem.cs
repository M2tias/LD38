using System;
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
    private GameObject monologPanel;
    [SerializeField]
    private GameObject hintPanel;
    [SerializeField]
    private GameObject saloonPanel;
    [SerializeField]
    private GameObject shopPanel;
    [SerializeField]
    private Resources resources;
    [SerializeField]
    [TextArea]
    private List<String> CitizenMonolog;
    [SerializeField]
    [TextArea]
    private List<String> Tips;

    private String nextMonolog;
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
        if (mode == DialogMode.Start | mode == DialogMode.Monolog | mode == DialogMode.End)
        {
            Time.timeScale = 0;
            dialogPanel.SetActive(true);
            if (mode == DialogMode.Start)
            {
                introPanel.SetActive(true);
            }
            else if (mode == DialogMode.Monolog || mode == DialogMode.End)
            {
                panel = monologPanel;
                panel.SetActive(true);

                Text monologText = null;
                foreach (Transform child in panel.transform)
                {
                    if (child.tag == "MonologText")
                    {
                        monologText = child.gameObject.GetComponent<Text>();
                    }
                }
                if (monologText == null) return;

                monologText.text = nextMonolog;
            }

            if(mode == DialogMode.End)
            {
                Application.Quit();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                mode = DialogMode.None;
            }
        }
        else if (mode == DialogMode.Hint)
        {
            Time.timeScale = 0;
            panel = hintPanel;
            dialogPanel.SetActive(true);
            panel.SetActive(true);

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
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                menuCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, -21f);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                menuCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, -63f);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (y == -21f)
                {
                    if (Tips.Count > 0)
                    {
                        if (resources.BuyTip())
                        {
                            nextMonolog = Tips[0];
                            Tips.RemoveAt(0);
                            mode = DialogMode.Monolog;
                            panel.SetActive(false);
                        }
                    }
                    else
                    {
                        nextMonolog = "\"You have all the knowledge.\"";
                        mode = DialogMode.Monolog;
                        panel.SetActive(false);
                    }
                }
                else if (y == -63f)
                {
                    //change the menu text
                    mode = DialogMode.None;
                }
                else
                {
                    mode = DialogMode.None;
                    resources.CanSaloon(false);
                    resources.CanShop(false);
                }
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
                    if (mode == DialogMode.Shop)
                    {
                        if (resources.BuyPickAxe())
                        {
                            //change the menu text
                            mode = DialogMode.None;
                        }
                    }
                    else if (mode == DialogMode.Saloon)
                    {
                        if (resources.BuyFood())
                        {
                            mode = DialogMode.None;
                        }
                    }
                }
                else if (y == -63f)
                {
                    if (mode == DialogMode.Shop)
                    {
                        if (resources.BuyFishingRod())
                        {
                            //change the menu text
                            mode = DialogMode.None;
                        }
                    }
                    else if (mode == DialogMode.Saloon)
                    {
                        if (resources.SellFish())
                        {
                            mode = DialogMode.None;
                        }
                    }
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
            monologPanel.SetActive(false);
            hintPanel.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
    }

    public bool HasCitizenMonolog()
    {
        if (CitizenMonolog.Count > 0)
        {
            nextMonolog = CitizenMonolog[0];
            CitizenMonolog.RemoveAt(0);
            return true;
        }
        return false;
    }

    public void SetMode(DialogMode mode)
    {
        this.mode = mode;
    }

    public void SetNextMonolog(string text)
    {
        nextMonolog = text;
    }
}

public enum DialogMode
{
    Start = 0,
    Saloon,
    Shop,
    Monolog,
    Hint,
    End,
    None
}