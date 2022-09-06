using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private ParticleManager particleManager;

    [SerializeField] public float money;
    [SerializeField] private float gems;
    [SerializeField] private float power;
    [SerializeField] private int level=1;
    [SerializeField] private float levelProgress=0;
    [SerializeField] private float levelMaxProgress = 100;
    [SerializeField] private string username="Username";

    [SerializeField] public float fun;
    [SerializeField] public float hunger;
    [SerializeField] public float higene;
    [SerializeField] public float sleepy;

    [SerializeField] private ParticleSystem higeneParticles;
    [SerializeField] private ParticleSystem houseflyParticles;

    [Header("Icons")]
    [SerializeField] private Image funImage;
    [SerializeField] private Image hungerImage;
    [SerializeField] private Image higeneImage;
    [SerializeField] private Image bedroomImage;
    [Header("Icons texts")]
    [SerializeField] private TextMeshProUGUI funText;
    [SerializeField] private TextMeshProUGUI hungerText;
    [SerializeField] private TextMeshProUGUI higeneText;
    [SerializeField] private TextMeshProUGUI bedroomText;

    [Header("UI texts")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI levelProgressText;
    [SerializeField] private TextMeshProUGUI nicnameText;
    [SerializeField] private TextMeshProUGUI levelUPText;


    [Header("Slider")]
    [SerializeField] private Slider levelProgressSlider;


    [Header("ToCheckActivity")]
    [SerializeField] private GameObject BedScene;
    [SerializeField] private GameObject MiniGameUI;

    [Header("SaveSystem")]
    [SerializeField] private SaveManager saveManager;


    [Header("Animations")]
    [SerializeField] private Animator levelUPanimator;

    //[Header("AudioManager")]
    [SerializeField] private AudioManager audioManager;

    public RewardHandler rewardHandler;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        rewardHandler = GameObject.FindGameObjectWithTag("RewardHandler").GetComponent<RewardHandler>();

        username = PlayerPrefs.GetString("uname", "Player");

    
        rewardHandler.GiveEarnings();
        SetUI();

    }

    // Update is called once per frame
    void Update()
    {
        HandleParticles();


        HandleColors(fun, funImage,funText);
        HandleColors(hunger, hungerImage, hungerText);
        HandleColors(higene, higeneImage,higeneText);
        HandleColors(sleepy, bedroomImage,bedroomText);

        HandleStats();
    }


    private float maxTimeSleep = 30f;
    private float timeSleep = 30f;


    private float maxTimeHigene = 30f;
    private float timeHigene = 30f;

    private float maxTimeHunger = 35f;
    private float timeHunger = 35;

    void HandleStats()
    {
        if (!BedScene.activeSelf)
        {
            if (timeSleep > 0)
                timeSleep -= Time.deltaTime;
            else
            {
                DealSleepy(1f);
                timeSleep = maxTimeSleep;
            }
        }

        if (MiniGameUI.activeSelf)
        {
            maxTimeHigene = 20f;
            maxTimeHunger = 28f;
        }
        else
        {
            maxTimeHigene = 30f;
            maxTimeHunger = 35f;
        }


        if (timeHigene > 0)
            timeHigene -= Time.deltaTime;
        else
        {
            DealHigene(1f);
            timeHigene = maxTimeHigene;
        }

        if (timeHunger > 0)
            timeHunger -= Time.deltaTime;
        else
        {
            DealHunger(1f);
            timeHunger = maxTimeHunger;
        }


    }

    void SetUI()
    {
        goldText.text = Mathf.Floor(money).ToString();
        gemText.text = Mathf.Floor(gems).ToString();
        levelText.text = level.ToString();
        levelProgressText.text = levelProgress + "/" + levelMaxProgress;
        nicnameText.text = username;
        levelProgressSlider.value = levelProgress / levelMaxProgress;
        saveManager.SavePlayerStats();
    }

    public void SetName(string name)
    {
        username = name;
        PlayerPrefs.SetString("uname", name);
        SetUI();
    }

    public string GetName()
    {
        return username;
    }

    public void AddProgress(float amount)
    {
        if (levelProgress + amount >= levelMaxProgress)
        {
            //change levelMaxProgress
            //level up

            level++;
            levelProgress = 0f;

            levelMaxProgress = (int)(levelMaxProgress * 1.25f);

            levelUPText.text = level.ToString();
            levelUPanimator.Play("LevelUp");

            if (audioManager == null)
                audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            audioManager.Play("LevelUp");
        }
        else
        {
            levelProgress+=amount;
        }
        SetUI();
    }

    public void AddMoney(float amount)
    {
        money += amount;
        SetUI();
    }

    public void DealMoney(float amount)
    {
        if (money - amount >= 0)
            money -= amount;
        else
            money = 0;

        SetUI();
    }

    public float GetMoney()
    {
        return money;
    }

    public void AddGems(float amount)
    {
        gems += amount;
        SetUI();
    }

    public void DealGems(float amount)
    {
        if (gems - amount >= 0)
            gems -= amount;
        else
            gems = 0;
        SetUI();
    }

    public float GetGems()
    {
        return gems;
    }

    public void AddPower(float amount)
    {
        power += amount;
    }

    public void DealPower(float amount)
    {
        if (power - amount >= 0)
            power -= amount;
        else
            power = 0;
    }

    float Floor(float amount)
    {
        if (amount > 100f)
        {
            if(fun<100f)
                particleManager.SpawnEmoji(particleManager.happyEmoji);
            return 100f;
        }
        else
            return amount;

    }


    public void AddFun(float amount)
    {
        fun += amount;
        fun = Floor(fun);

        HandlePlayerAnimation();
    }

    public void DealFun(float amount)
    {
        if (fun - amount >= 0)
            fun -= amount;
        else
            fun = 0;

        HandlePlayerAnimation();
    }

    public void AddHunger(float amount)
    {
        hunger += amount;
        hunger = Floor(hunger);

        HandlePlayerAnimation();
    }

    public void DealHunger(float amount)
    {
        if (hunger - amount >= 0)
            hunger -= amount;
        else
            hunger = 0;

        HandlePlayerAnimation();
    }

    public float GetHunger()
    {
        return hunger;
    }

    public void AddHigene(float amount)
    {
        higene += amount;
        higene = Floor(higene);

        HandlePlayerAnimation();
    }

    public void DealHigene(float amount)
    {
        if (higene - amount >= 0)
            higene -= amount;
        else
            higene = 0;

        HandlePlayerAnimation();
    }

    public float GetHigene()
    {
        return higene;
    }


    public void AddSleepy(float amount)
    {
        sleepy += amount;
        sleepy = Floor(sleepy);

        HandlePlayerAnimation();
    }

    public void DealSleepy(float amount)
    {
        if (sleepy - amount >= 0)
            sleepy -= amount;
        else
            sleepy = 0;

        HandlePlayerAnimation();
    }

    public int GetLevel()
    {
        return level;
    }

    public void HandlePlayerAnimation()
    {
        if (playerCharacter.activeSelf)
        {
            if (playerCharacter.GetComponentInChildren<Animator>().GetInteger("animation") != 7)
            {

                if (higene < 20f)
                {
                    playerCharacter.GetComponentInChildren<Animator>().SetInteger("animation", 9);
                }
                else if (sleepy < 10f)
                {
                    playerCharacter.GetComponentInChildren<Animator>().SetInteger("animation", 16);
                }
                else if (hunger < 30f)
                {
                    playerCharacter.GetComponentInChildren<Animator>().SetInteger("animation", 23);
                }
                else
                {
                    playerCharacter.GetComponentInChildren<Animator>().SetInteger("animation", 1);
                }
            }
        }
    }


    public void HandleParticles()
    {
        //if(GetHigene()<)
        var emission = higeneParticles.emission;
        //emission.rateOverTime = 5-(GetHigene() / 20f);

        var emissionFly = houseflyParticles.emission;
        if (GetHigene() < 40f)
        {
            if (!higeneParticles.gameObject.activeSelf)
                higeneParticles.gameObject.SetActive(true);
            emissionFly.rateOverTime = 5 - (GetHigene() / 20f);
            emission.rateOverTime = (5 - (GetHigene() / 20f)) *2f;

        }
        else
        {
            if (higeneParticles.gameObject.activeSelf)
                higeneParticles.gameObject.SetActive(false);
            emissionFly.rateOverTime = 0f;
            emission.rateOverTime = 0f;
        }
    }

    public void HandleColors(float value, Image img, TextMeshProUGUI text)
    {
        text.text = Mathf.Round(value) +"%";
        if (value <= 33f)
        {
            img.color = Color.Lerp(img.color, Color.red, 0.05f);
        }
        else if (value > 33f && value <= 66f)
        {
            img.color = Color.Lerp(img.color, Color.yellow, 0.05f);
        }
        else if(value > 66f)
        {
            img.color = Color.Lerp(img.color, Color.green, 0.05f);
        }
    }


    public bool CanPlay()
    {
        if (higene < 20f)
        {
            //play emoji
            return false;
        }
        else if (sleepy < 10f)
        {
            //play emoji
            return false;
        }
        else if (hunger < 30f)
        {
            //play emoji
            return false;
        }
        return true;
    }


    public List<float> GetStatisticsList()
    {
        List<float> stats = new List<float>();


        stats.Add(money);
        stats.Add(gems);
        stats.Add(power);
        stats.Add(level);
        stats.Add(levelProgress);
        stats.Add(levelMaxProgress);
        stats.Add(fun);
        stats.Add(hunger);
        stats.Add(higene);
        stats.Add(sleepy);



        return stats;
    }


    public void SetStatistics(List<float> stats)
    {
        money = stats[0];
        gems = stats[1];
        power = stats[2];
        level = (int)stats[3];
        levelProgress = stats[4];
        levelMaxProgress = stats[5];
        fun = stats[6];
        hunger = stats[7];
        higene = stats[8];
        sleepy = stats[9];

        SetUI();
        HandlePlayerAnimation();
    }

    public void RemoveBanner()
    {
        PlayerPrefs.SetInt("BannerAd", 1);
        SceneManager.LoadScene(0);
    }

}
