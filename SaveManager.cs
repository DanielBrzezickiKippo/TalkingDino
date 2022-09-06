using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;


public class SaveManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private MapEditor mapEditor;
    [SerializeField] private FoodSupplies foodSupplies;
    [SerializeField] private PlayerEditor playerEditor;

    [SerializeField] private WheelOfFortune wheelOfFortune;

    private List<float> statistics;
    public string path;

    private void Awake()
    {
        LoadPlayerStats();
        LoadFurnitures();
        LoadFood();
        LoadSkin();
        LoadLastLogin();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SavePlayerStats()
    {

        string destination = Application.persistentDataPath + "/playerStats.dat";

        FileStream file;

        if (File.Exists(destination))
            file = File.OpenWrite(destination);
        else
            file = File.Create(destination);

        List<float> data = playerStats.GetStatisticsList();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadPlayerStats()
    {
        string destination = Application.persistentDataPath + "/playerStats.dat";
        path = destination;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found created new");
            SavePlayerStats();
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        List<float> data = (List<float>)bf.Deserialize(file);
        file.Close();

        playerStats.SetStatistics(data);
    }


    public void SaveFurnitures()
    {
        string destination = Application.persistentDataPath + "/playerFurnitures.dat";

        FileStream file;

        if (File.Exists(destination))
            file = File.OpenWrite(destination);
        else
            file = File.Create(destination);


        if (mapEditor.boughtObjects == null)
            mapEditor.createSelectablesData();
        List<List<BoughtObject>> data = mapEditor.boughtObjects;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadFurnitures()
    {
        string destination = Application.persistentDataPath + "/playerFurnitures.dat";
        path = destination;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found created new");
            SaveFurnitures();
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        List<List<BoughtObject>> data = (List<List<BoughtObject>>)bf.Deserialize(file);
        file.Close();

        mapEditor.createSelectablesData(data);
    }


    public void SaveFood()
    {
        string destination = Application.persistentDataPath + "/playerFood.dat";

        FileStream file;

        if (File.Exists(destination))
            file = File.OpenWrite(destination);
        else
            file = File.Create(destination);



        if (foodSupplies.playerSupplies == null)
            foodSupplies.playerSupplies = new List<FoodData>();
        List<FoodData> data = foodSupplies.playerSupplies;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadFood()
    {
        string destination = Application.persistentDataPath + "/playerFood.dat";
        path = destination;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found created new");
            SaveFood();
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        List<FoodData> data = (List<FoodData>)bf.Deserialize(file);
        file.Close();

        foodSupplies.playerSupplies = data;
        foodSupplies.GenerateFood();
    }

    public void SaveSkin()
    {
        string destination = Application.persistentDataPath + "/playerSkins.dat";

        FileStream file;

        if (File.Exists(destination))
            file = File.OpenWrite(destination);
        else
            file = File.Create(destination);



        if (playerEditor.skinsBought == null || playerEditor.skinsBought.Count==0)
            playerEditor.createSkinsData();
        List<bool> data = playerEditor.skinsBought;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

        PlayerPrefs.GetInt("currentSkin", playerEditor.currentSkin);
    }

    public void LoadSkin()
    {
        string destination = Application.persistentDataPath + "/playerSkins.dat";
        path = destination;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found created new");
            SaveSkin();
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        List<bool> data = (List<bool>)bf.Deserialize(file);
        file.Close();

        playerEditor.skinsBought = data;

        playerEditor.currentSkin = PlayerPrefs.GetInt("currentSkin", 0);
        playerEditor.SetSkin();
    }

    public void SaveLastLogin()
    {
        DateTime dateTime;
        bool didParse = System.DateTime.TryParse(PlayerPrefs.GetString("DateTime", ""), System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime);

        string dateTimeString = DateTime.UtcNow.ToString(System.Globalization.CultureInfo.InvariantCulture);
        PlayerPrefs.SetString("DateTime", dateTimeString);

    }

    public void LoadLastLogin()
    {
        DateTime dateTime;
        bool didParse = System.DateTime.TryParse(PlayerPrefs.GetString("DateTime",""), System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime);

        if (didParse)
        {
            DateTime now = DateTime.UtcNow;
            TimeSpan timeSpan = now - dateTime;
            double minutesPassed = timeSpan.TotalMinutes;

            if (PlayerPrefs.GetInt("goToSleep", 0) == 1)
            {
                float asleep = ((float)minutesPassed * 100f) / 60f;
                playerStats.AddSleepy(Mathf.Floor(asleep));
                PlayerPrefs.SetInt("goToSleep", 0);
            }
            else
            {
                float asleep = ((float)minutesPassed * 100f) / 120f;
                playerStats.DealSleepy(Mathf.Floor(asleep));
            }

           // Debug.Log(minutesPassed);
            float bore = ((float)minutesPassed * 100f) / 30f;
           // Debug.Log(bore);
            playerStats.DealFun(Mathf.Floor(bore));

            float hunger = ((float)minutesPassed * 100f) / 180f;
            playerStats.DealHunger(Mathf.Floor(hunger));

           // Debug.Log(hunger);

            float physiology = ((float)minutesPassed * 100f) / 60f;
            playerStats.DealHigene(Mathf.Floor(physiology));

          //  Debug.Log(physiology);


            if (now.Day != dateTime.Day)
            {
                //Reset FREE
                wheelOfFortune.ResetFreeSpins();

            }
            SaveLastLogin();
        }
        else
        {
            SaveLastLogin();
        }
    }

    private AdMobVideo admobVideo;
    public void WatchADEarnSleep()
    {
        if (admobVideo == null)
            admobVideo = GameObject.FindGameObjectWithTag("AdManager").GetComponent<AdMobVideo>();

        admobVideo.WatchAdFreeSleep();
    }

    public void WatchADEarnGems()
    {
        if (admobVideo == null)
            admobVideo = GameObject.FindGameObjectWithTag("AdManager").GetComponent<AdMobVideo>();

        admobVideo.WatchAdFreeGems();
    }

}
