using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BedroomSystem : MonoBehaviour
{


    public bool goToSleep = false;
    private Animator playerAnimator;
    [SerializeField] private Color[] lightColors;
    [SerializeField] private Light mapLight;
    [SerializeField] private Light lampLight;

    [SerializeField] private float maxTime=5f;
    [SerializeField] private PlayerStats playerStats;

    public void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        time = maxTime;

        goToSleep = PlayerPrefs.GetInt("goToSleep") == 1 ? true : false;
        Set();

    }

    // Update is called once per frame
    void Update()
    {
        if(playerAnimator==null)
            playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();

        HandleTime();
    }

    float time;
    public void HandleTime()
    {
        if (goToSleep)
        {
            if (time > 0)
                time -= Time.deltaTime;
            else
            {
                playerStats.AddSleepy(1f);
                time = maxTime;
            }
        }

    }

    public void SwitchLamp()
    {
        if (goToSleep)
            goToSleep = false;
        else
        {
            goToSleep = true;
            playerStats.GetComponent<SaveManager>().SaveLastLogin();
        }

        PlayerPrefs.SetInt("goToSleep", goToSleep==true?1:0);
        Debug.Log(PlayerPrefs.GetInt("goToSleep"));

        Set();
    }

    
    public void Set()
    {
        if (goToSleep)
        {
            playerAnimator.SetInteger("animation", 7);
            mapLight.color = lightColors[0];
            lampLight.color = lightColors[0];
        }
        else
        {
            playerAnimator.SetInteger("animation", 1);
            playerStats.HandlePlayerAnimation();
            mapLight.color = lightColors[1];
            lampLight.color = lightColors[1];
        }
    }

}
