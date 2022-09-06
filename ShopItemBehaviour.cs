using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemBehaviour : TriggerAction
{

    [SerializeField] public FoodSupplies foodSupplies;
    [SerializeField] public int id;
    [SerializeField] public float cost;
    [SerializeField] private bool forAd = false;
    private PlayerStats playerStats;

    Manager sceneManager;
    public void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
    }

    public void Update()
    {
        
    }

    AdMobVideo admobvideo;


    public override void Action()
    {
        if(playerStats==null)
            playerStats = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerStats>();

        if (forAd)
        {
            if(admobvideo==null)
                admobvideo = GameObject.FindGameObjectWithTag("AdManager").GetComponent<AdMobVideo>();
            admobvideo.WatchAdFreeFries();
            return;
        }

        if (playerStats.GetMoney() >= cost)
        {
            sceneManager.audioManager.ForcePlay("CashRegister");

            playerStats.DealMoney(cost);
            //Debug.Log("player paid " + cost + " for food");
            foodSupplies.AddPlayerFood(id);

            GameObject food = Instantiate(transform.gameObject, transform.position, Quaternion.identity);
            //food.GetComponent<BoxCollider>().isTrigger = true;
            Destroy(food.GetComponent<ClickableObject>());
            Destroy(food.GetComponent<ShopItemBehaviour>());
            food.AddComponent<ObjectDrop>();
            foodSupplies.assortmentObjects.Add(food);
        }
    }

    public void giveForAd()
    {
        sceneManager.audioManager.ForcePlay("CashRegister");

        //Debug.Log("player paid " + cost + " for food");
        foodSupplies.AddPlayerFood(id);

        GameObject food = Instantiate(transform.gameObject, transform.position, Quaternion.identity);
        //food.GetComponent<BoxCollider>().isTrigger = true;
        Destroy(food.GetComponent<ClickableObject>());
        Destroy(food.GetComponent<ShopItemBehaviour>());
        food.AddComponent<ObjectDrop>();
        foodSupplies.assortmentObjects.Add(food);
    }


}
