using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathSpeaker : MonoBehaviour
{
    [SerializeField] private MoveItem moveSpeaker;
    [SerializeField] private GameObject waterParticles;
    [SerializeField] private float timer = 0f;
    [SerializeField] private SpongeBath spongeBath;
    float multiplier = 2f;
    private PlayerStats playerStats;
    private Animator playerAnimator;

    public void Start()
    {
        playerStats = Camera.main.gameObject.GetComponent<PlayerStats>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Handle();
    }

    public void Handle()
    {
        if(playerAnimator==null)
            playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();

        if (moveSpeaker.GetState())
        {
            waterParticles.SetActive(true);
            Vector3 mousePos = Input.mousePosition;
            if (mousePos.x >= Screen.width * 0.38f && mousePos.x <= Screen.width * 0.62f)
            {
                timer += Time.deltaTime * multiplier;
                playerAnimator.SetInteger("animation", 20);
                //Debug.Log("leje po smoku");
            }
            else
            {
                playerStats.HandlePlayerAnimation();

            }
        }
        else
        {
            waterParticles.SetActive(false);
            playerStats.HandlePlayerAnimation();
        }


        if (timer > 0.15f)
        {
            timer = 0f;
            //multiplier = 0f;
            //Debug.Log("umyty smok jak ta lala");
            if (Random.Range(0, 100) > 95 &&playerStats.GetHigene()<95f)
            {
                playerStats.rewardHandler.GReward(0, Random.Range(2, 5));
                playerStats.rewardHandler.GReward(1, Random.Range(4, 10));
                playerStats.rewardHandler.GiveEarnings();
                //playerStats.AddMoney(Random.Range(2, 5));
            }

            playerStats.AddHigene(Random.Range(2,5));
            FoamController();
        }


    }

    void FoamController()
    {
        for(int i = 0; i < spongeBath.foam.Count; i++)
        {
            if (spongeBath.foam[i].gameObject.activeSelf) {
                spongeBath.foam[i].gameObject.SetActive(false);
                break;
            }
        }
    }




}
