using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    [SerializeField] public BasketballMG basketballMG;

    AudioManager audioManager;

    PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        playerStats = Camera.main.gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Basket")
        {
            if(playerStats==null)
                playerStats = Camera.main.gameObject.GetComponent<PlayerStats>();

            playerStats.AddFun(Random.Range(8, 15));
            playerStats.rewardHandler.GReward(0, Random.Range(0, 3));
            playerStats.rewardHandler.GReward(1, Random.Range(0, 8));
            playerStats.rewardHandler.GiveEarnings();

            basketballMG.Add();
            audioManager.ForcePlay("BasketSwish");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Basket")
        {
            audioManager.PlayInObject("BallBounce", this.gameObject);
        }
    }

}
