using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletBehaviour : TriggerAction
{

    private Manager sceneManager;
    private PlayerStats playerStats;
    private Animator playerAnimator;
    private ToiletSystem toiletSystem;
    private bool once = false;



    public void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
        playerStats = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerStats>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        toiletSystem = GameObject.FindGameObjectWithTag("ToiletSystem").GetComponent<ToiletSystem>();
    }

    public void Update()
    {
        if (playerAnimator == null)
            playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    }

    public override void Action()
    {
        if (!once)
        {
            once = true;
            sceneManager.OpenScene(4);
            toiletSystem.PlaySequence();
            Invoke("Reload", 120f);
            sceneManager.ForceEditButton(false);
        }
        else
        {
            Debug.Log("dont want to go to toilet");
            playerAnimator.Play("No 0");
        }
            //Audio decline go to toilet;
    }


    void Reload()
    {
        once = false;
    }

}
