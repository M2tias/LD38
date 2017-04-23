using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    /*no limit for prosperity!
    [SerializeField]
    [Range(0, 30)]
    private int maxGold = 10;*/

    private bool canDig = false;
    private bool canRefine = false;
    private bool canFish = false;
    private bool canFry = false;
    private bool canSaloon = false;
    private bool canShop = false;
    private bool canAskHints = false;

    [SerializeField]
    private CitizenFactor citizenFactor;
    [SerializeField]
    private Helicopter helicopter;

    [SerializeField]
    [Range(0, 30)]
    private int gold = 10;

    [SerializeField]
    [Range(0, 30)]
    private int maxGoldOre = 10;

    private int goldOre = 0;

    [SerializeField]
    [Range(0, 30)]
    private int maxFish = 10;

    private int fish = 0;

    [SerializeField]
    [Range(0, 30)]
    private int maxDays = 1;

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

    private float suspiciousness = 0;

    [SerializeField]
    [Range(0, 100)]
    private int maxHunger = 100;
    [SerializeField]
    [Range(0f, 2f)]
    private float hungerTick = 0.005f;

    private float hunger = 0;

    [SerializeField]
    private bool hasPickaxe = false;
    [SerializeField]
    private int pickaxeCost = 9;
    [SerializeField]
    private bool hasFishingRod = false;
    [SerializeField]
    private int fishingRodCost = 10;
    [SerializeField]
    private int foodCost = 1;
    [SerializeField]
    private int fishLot = 5;
    [SerializeField]
    private int fishLotPrice = 1;
    [SerializeField]
    private float chaseTick = 0.01f;
    [SerializeField]
    private float tipCost = 5f;
    [SerializeField]
    private float spawnTime = 10f;

    private float spawnTimer = 0f;

    #region UI Fields
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
    [SerializeField]
    private Text fishText;
    [SerializeField]
    private Image pickaxeImage;
    [SerializeField]
    private Image fishingRodImage;
    #endregion

    [SerializeField]
    private DialogSystem dialogSystem;

    // Use this for initialization
    void Start()
    {
        spawnTimer = Time.time + 15f;
    }

    // Update is called once per frame
    void Update()
    {
        suspiciousnessText.text = suspiciousness.ToString("0") + '/' + maxSuspiciousness.ToString();
        goldText.text = gold.ToString();
        goldOreText.text = goldOre.ToString() + '/' + maxGoldOre.ToString();
        hourText.text = hour.ToString("0") + '/' + hoursInDay.ToString();
        dayText.text = day.ToString() + '/' + maxDays.ToString();
        hungerText.text = hunger.ToString("0") + '/' + maxHunger.ToString();
        fishText.text = fish.ToString() + '/' + maxFish.ToString();

        if (hunger > 50f)
        {
            hungerText.color = Color.red;
        }
        else
        {
            hungerText.color = Color.black;
        }

        if (suspiciousness >= 60f)
        {
            suspiciousnessText.color = Color.red;
        }
        else
        {
            suspiciousnessText.color = Color.black;
        }

        if (suspiciousness > 100f)
        {
            suspiciousness = 100f;
            spawnTime = 1f;
        }

        if (goldOre >= 1)
        {
            goldOreText.color = Color.red;
        }
        else
        {
            goldOreText.color = Color.black;
        }

        if(hasFishingRod)
        {
            fishingRodImage.gameObject.SetActive(true);
        }

        if(hasPickaxe)
        {
            pickaxeImage.gameObject.SetActive(true);
        }

        if(hunger > 100f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(day >= maxDays)
        {
            helicopter.GetComponent<Animator>().SetTrigger("ToTheRescue");
        }

        if(Time.time - spawnTimer > spawnTime)
        {
            Debug.Log("Spawned");
            SpawnCitizen();
            spawnTimer = Time.time;
        }
    }

    private void FixedUpdate()
    {
        if (hour > hoursInDay)
        {
            hour = 0;
            day++;
        }
        hour += hourTick;

        if (hunger > maxHunger)
        {
            //die
            hunger = 0;
        }
        hunger += hungerTick;
    }

    public bool DigGold()
    {
        if (!canDig || !hasPickaxe)
        {
            Debug.Log("Not here, buddy!");
            return false;
        }

        if (goldOre < maxGoldOre)
        {
            goldOre++;
            gold++; //gold ore gives 1 gold, refining doubles it~
        }
        else
        {
            Debug.Log("Can't carry more gold!");
        }
        return true;
    }

    public bool RefineGold()
    {
        if (!canRefine)
        {
            Debug.Log("What are you trying to do?");
            return false;
        }

        if (goldOre > 0)
        {
            gold++;
            goldOre--;
        }
        else
        {
            Debug.Log("Nothing to refine!");
        }

        return true;
    }

    public bool Fish()
    {
        if (!canFish || !hasFishingRod)
        {
            Debug.Log("No fish on land...");
            return false;
        }

        if (fish < maxFish)
        {
            fish++;
        }
        else
        {
            Debug.Log("Too much to carry!");
        }

        return true;
    }

    public bool Fry()
    {
        if (!canFry)
        {
            Debug.Log("Try fire, buddy!");
            return false;
        }

        if (fish > 0)
        {
            if (hunger > 50)
            {
                hunger -= 50;
            }
            else
            {
                hunger = 0;
            }
            fish--;
        }
        else
        {
            Debug.Log("No fish to fry!");
        }

        return true;
    }

    public bool Saloon()
    {
        if (!canSaloon)
        {
            return false;
        }

        dialogSystem.SetMode(DialogMode.Saloon);

        return true;
    }

    public bool Shop()
    {
        if (!canShop)
        {
            return false;
        }

        dialogSystem.SetMode(DialogMode.Shop);

        return true;
    }

    public bool AskHints()
    {
        if (!canAskHints)
        {
            return false;
        }

        dialogSystem.SetMode(DialogMode.Hint);

        return true;
    }

    public void CanDig(bool can)
    {
        canDig = can;
    }

    public void CanRefine(bool can)
    {
        canRefine = can;
    }

    public void CanFish(bool can)
    {
        canFish = can;
    }

    public void CanFry(bool can)
    {
        canFry = can;
    }

    public void CanSaloon(bool can)
    {
        canSaloon = can;
    }

    public void CanShop(bool can)
    {
        canShop = can;
    }

    public void CanAskHints(bool can)
    {
        canAskHints = can;
    }

    public bool BuyPickAxe()
    {
        if (!hasPickaxe)
        {
            if (gold >= pickaxeCost)
            {
                gold -= pickaxeCost;
                hasPickaxe = true;
                return true;
            }
            return false;
        }
        return false;
    }

    public bool HasPickAxe()
    {
        return hasPickaxe;
    }

    public bool BuyFishingRod()
    {
        if (!hasFishingRod)
        {
            if (gold >= fishingRodCost)
            {
                gold -= fishingRodCost;
                hasFishingRod = true;
                return true;
            }
            return false;
        }
        return false;
    }

    public bool HasFishingRod()
    {
        return hasFishingRod;
    }

    public bool BuyFood()
    {
        float oreCoef = goldOre > 0 ? 1.5f : 1f;
        if (gold >= foodCost)
        {
            gold -= foodCost;
            suspiciousness += tipCost * oreCoef;
            hunger = 0;
            return true;
        }
        return false;
    }

    public bool BuyTip()
    {
        suspiciousness += tipCost;
        return true;
    }

    public bool SellFish()
    {
        if (fish >= fishLot)
        {
            gold += fishLotPrice;
            fish -= fishLot;
            suspiciousness -= 8;
            return true;
        }
        return false;
    }

    public void TickChaseSuspicion(float distance)
    {
        float oreCoef = goldOre > 0 ? 1.5f : 1f;

        suspiciousness += distance * chaseTick * oreCoef;
    }

    public void ReduceSuspicion(float amount)
    {
        float oreCoef = goldOre > 0 ? 1.5f : 1f;
        if (goldOre > 0)
        {
            suspiciousness += Mathf.Abs(amount*oreCoef);
        }
        else
        {
            suspiciousness -= amount;
        }
    }

    public void SpawnCitizen()
    {
        citizenFactor.Create(dialogSystem, this);
    }

    public void ResetSpawnTime()
    {
        spawnTimer = Time.time;
    }

    public void Win()
    {
        if (gold > 0)
        {
            dialogSystem.SetNextMonolog("You managed to mine some gold without being torched!\nYou mined " + gold + " ounces of gold!");
            dialogSystem.SetMode(DialogMode.Monolog);
        }
        else
        {
            dialogSystem.SetNextMonolog("You failed in what you set out to do but at least you didn't die in a small strange town!");
            dialogSystem.SetMode(DialogMode.Monolog);
        }
    }
}
