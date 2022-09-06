using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChestBehaviour : TriggerAction
{
    //private Manager sceneManager;
    [SerializeField] private GameObject minigameUI;

    [SerializeField] private List<GameObject> minigamesObjects;
    [SerializeField] private List<string> minigamesNames;

    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI minigameText;

    [SerializeField] private GameObject editButton;
    [SerializeField] private GameObject speakerButton;

    [SerializeField] private List<ClickableObject> clickableObjects;

    [SerializeField] private ParticleManager particleManager;

    int currentMinigame = 0;

    Manager sceneManager;
    PlayerStats playerStats;
    public void Start()
    {
        playerStats = Camera.main.gameObject.GetComponent<PlayerStats>();
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
    }

    public override void Action()
    {
        if (playerStats.CanPlay())
        {
            sceneManager.audioManager.ForcePlay("PlayMinigame");
            OpenMinigameUI();
        }
        else
            particleManager.SpawnAdequeteEmoji();
        //sceneManager.OpenScene(5);
    }

    public void OpenMinigameUI()
    {
        editButton.SetActive(false);
        speakerButton.SetActive(false);

        minigameUI.SetActive(true);
        player.SetActive(false);
        ChangeMinigame(0);
        SetClickableObjects(false);
    }

    public void CloseMinigameUI()
    {
        editButton.SetActive(true);
        speakerButton.SetActive(true);

        minigameUI.SetActive(false);
        player.SetActive(true);
        ChangeMinigame(-1);

        SetClickableObjects(true);
    }

    public void NextGame()
    {
        sceneManager.audioManager.ForcePlay("Pop3");
        int current = currentMinigame;
        currentMinigame = currentMinigame < minigamesObjects.Count-1 ? current+1 : 0;
        ChangeMinigame(currentMinigame);
    }

    public void PreviousGame()
    {
        sceneManager.audioManager.ForcePlay("Pop3");
        int current = currentMinigame;
        currentMinigame = currentMinigame > 0 ? current-1 : (minigamesObjects.Count-1);
        ChangeMinigame(currentMinigame);
    }

    void ChangeMinigame(int game)
    {
        for (int i = 0; i < minigamesObjects.Count; i++)
        {
            if (i != game)
                minigamesObjects[i].SetActive(false);
            else
            {
                minigamesObjects[i].SetActive(true);
                minigameText.text = minigamesNames[i];
            }
        }
    }


    void SetClickableObjects(bool flag)
    {
        foreach(ClickableObject g in clickableObjects)
        {
            g.enabled = flag;
        }
    }

}
