using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletSystem : MonoBehaviour
{
    [SerializeField] private Curtain curtain;
    [SerializeField] private GameObject backButton;

    private PlayerStats playerStats;


    Manager sceneManager;
    public void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
        playerStats = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySequence()
    {
        sceneManager.audioManager.ForcePlay("Fart");
        FirstPart();
        Invoke("SecondPart", 1f);
        Invoke("ThirdPart", 3.5f);
    }

    void FirstPart()
    {
        backButton.SetActive(false);
        curtain.SwitchCurtain();//Switch to true;
    }

    void SecondPart()
    {
        Debug.Log("AUDIO of sranie and szczanie");
    }


    void ThirdPart()
    {
        playerStats.AddHigene(Random.Range(20f,25f));
        //playerStats.AddMoney(Random.Range(2, 5));
        playerStats.rewardHandler.GReward(0, Random.Range(1, 5));
        playerStats.rewardHandler.GReward(1, Random.Range(4, 10));
        playerStats.rewardHandler.GiveEarnings();


        curtain.SwitchCurtain();
        backButton.SetActive(true);
    }

}
