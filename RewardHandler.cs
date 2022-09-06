using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardHandler : MonoBehaviour
{
    RewardHandler Instance;

    [HideInInspector] public float coins = 0;
    [HideInInspector] public float gems = 0;
    [HideInInspector] public float exp = 0;


    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {


        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }


    ItemAnimator itemAnimator;
    public void Reward(int boost=1)
    {
        if (itemAnimator == null)
            itemAnimator = GameObject.FindGameObjectWithTag("ItemAnimator").GetComponent<ItemAnimator>();
        //type
        if (Random.Range(0, 100) < 20)
        {
            int amount;
            switch (Random.Range(0, 3))
            {
                case 0:
                    amount = Random.Range(1, 5) * boost;

                    itemAnimator.Spawn(0, amount);
                    coins += amount;
                    break;
                case 1:
                    amount = Random.Range(5, 20) * boost;
                    itemAnimator.Spawn(1, amount);
                    exp += amount;
                    break;
                case 2:
                    amount = Random.Range(1, 4) * boost;
                    itemAnimator.Spawn(2, amount);
                    gems += amount;
                    break;
            }
        }

        itemAnimator.SetUI();
    }


    public void GReward(int type, int amount)
    {
        if (itemAnimator == null)
            itemAnimator = GameObject.FindGameObjectWithTag("ItemAnimator").GetComponent<ItemAnimator>();
        itemAnimator.Spawn(type ,amount);

        if (type == 0)
            coins += amount;
        else if (type == 1)
            exp += amount;
        else if (type == 2)
            gems += amount;

        itemAnimator.SetUI();
    }

    public void GiveEarnings()
    {
        if (itemAnimator == null)
            itemAnimator = GameObject.FindGameObjectWithTag("ItemAnimator").GetComponent<ItemAnimator>();

        //0-coin
        //1-gem
        //2-exp
        itemAnimator.Spawn(0, (int)Mathf.Round(coins /3f));
        itemAnimator.Spawn(2, (int)Mathf.Round(gems /3f));
        itemAnimator.Spawn(1, (int)Mathf.Round(exp/3f));

        PlayerStats playerStats = Camera.main.gameObject.GetComponent<PlayerStats>();

        playerStats.AddGems((int)gems);
        playerStats.AddMoney((int)coins);
        playerStats.AddProgress((int)exp);


        coins = 0;
        gems = 0;
        exp = 0;

    }


}
