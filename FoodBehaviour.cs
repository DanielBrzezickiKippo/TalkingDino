using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : TriggerAction
{
    private float distanceFromPlayer = 1.86f;
    private Vector3 mainPlace;
    private PlayerStats playerStats;

    GameObject player;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainPlace =player.GetComponent<PlayerMove>().mainPlace;

        playerStats = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerStats>();
    }

    public override void Action()
    {
        if (Vector3.Distance(transform.localPosition, mainPlace) < distanceFromPlayer)
        {
            if (playerStats.GetHunger()<100f)
            {
                //Audio opierdol
                player.GetComponentInChildren<Animator>().Play("Eat 0");
                //playerStats.AddHunger(Random.Range(15f, 25f));
                FoodSupplies fs = GetComponentInParent<FoodSupplies>();
                float multiplier = fs.RemoveObject(this.gameObject);
                playerStats.AddHunger(Random.Range(1f, 3f) * multiplier);
                Camera.main.gameObject.GetComponent<SaveManager>().SavePlayerStats();

                int r = Random.Range(0, 3);
                if (r == 0)
                    fs.audioManager.ForcePlay("Bite1");
                else if (r == 1)
                    fs.audioManager.ForcePlay("Bite2");
                else
                    fs.audioManager.ForcePlay("Bite3");

            }
            else
            {
                //Audio dont want;
                player.GetComponentInChildren<Animator>().Play("No 0");
            }
        }
    }


}
