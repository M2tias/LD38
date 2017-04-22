using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour {
    /*no limit for prosperity!
    [SerializeField]
    [Range(0, 30)]
    private int maxGold = 10;*/

    private int gold = 0;

    [SerializeField]
    [Range(0, 30)]
    private int maxGoldOre = 10;

    private int goldOre = 0;

    [SerializeField]
    [Range(0, 30)]
    private int maxDays = 10;

    private int day = 0;

    [SerializeField]
    [Range(0, 24)]
    private int hoursInDay = 10;
    [SerializeField]
    [Range(0f, 2f)]
    private float hourTick = 0.005f;

    private float hour = 0;
    
    [SerializeField]
    [Range(0, 100)]
    private int maxSuspiciousness = 100;

    private int suspiciousness = 0;

    [SerializeField]
    [Range(0, 100)]
    private int maxHunger = 100;
    [SerializeField]
    [Range(0f, 2f)]
    private float hungerTick = 0.005f;

    private float hunger = 0;

    #region textFields
    [SerializeField]
    private Text suspiciousnessText;
    [SerializeField]
    private Text goldText;
    [SerializeField]
    private Text goldOreText;
    [SerializeField]
    private Text hourText;
    [SerializeField]
    private Text dayText;
    [SerializeField]
    private Text hungerText;
    #endregion


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        suspiciousnessText.text = suspiciousness.ToString() + '/' + maxSuspiciousness.ToString();
        goldText.text = gold.ToString();
        goldOreText.text = goldOre.ToString() + '/' + maxGoldOre.ToString();
        hourText.text = hour.ToString() + '/' + hoursInDay.ToString();
        dayText.text = day.ToString() + '/' + maxDays.ToString();
        hungerText.text = hunger.ToString() + '/' + maxHunger.ToString();
    }

    private void FixedUpdate()
    {
        if(hour > hoursInDay)
        {
            hour = 0;
            day++;
        }
        hour += hourTick;

        if(hunger > maxHunger)
        {
            //die
            hunger = 0;
        }
        hunger += hungerTick;
    }

    public void DigGold()
    {
        if(goldOre < maxGoldOre)
        {
            goldOre++;
        }
        else
        {
            Debug.Log("Can't carry more gold!");
        }
    }

    public void RefineGold()
    {
        if(goldOre > 0)
        {
            gold++;
        }
        else
        {
            Debug.Log("Nothing to refine!");
        }
    }
}
