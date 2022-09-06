using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeBath : MonoBehaviour
{
    [SerializeField] private MoveItem moveSpeaker;
    [SerializeField] private float timer = 0f;
    [SerializeField] public List<Transform> foam;
    float multiplier = 1.5f;
    private PlayerStats playerStats;
    private Animator playerAnimator;

    public void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerStats>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Handle();
    }

    public void Handle()
    {
        if (playerAnimator == null)
            playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();

        if (moveSpeaker.GetState())
        {
            Vector3 mousePos = Input.mousePosition;
            if ((mousePos.x >= Screen.width * 0.38f && mousePos.x <= Screen.width * 0.62f)&&
                (mousePos.y >= Screen.height *0.42f && mousePos.y<= Screen.height *0.58f)
                )
            {
                timer += Time.deltaTime * multiplier;
                playerAnimator.SetInteger("animation", 20);
                CheckPositions();
            }
            else
            {
                playerStats.HandlePlayerAnimation();

            }
        }
        else
        {
            playerStats.HandlePlayerAnimation();
        }


        if (timer > 0.25f)
        {
            timer = 0f;
            if (Random.Range(0, 100) > 95&& playerStats.GetHigene() < 95f)
            {
                playerStats.rewardHandler.GReward(0, Random.Range(2, 5));
                playerStats.rewardHandler.GReward(0, Random.Range(5, 12));
                playerStats.rewardHandler.GiveEarnings();
                //playerStats.AddMoney(Random.Range(2, 5));
            }

            playerStats.AddHigene(Random.Range(2, 5));

        }

    }

    public void CheckPositions()
    {
        foreach (Transform f in foam)
        {
            if (!f.gameObject.activeSelf)
            {
                if (Vector3.Distance(this.gameObject.transform.position, f.position) < 0.5f)
                {
                    f.gameObject.SetActive(true);
                }
            }
        }
    }



}
