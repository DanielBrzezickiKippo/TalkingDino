using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private List<GameObject> scenes;
    [SerializeField] private List<GameObject> scenesUI;
    [SerializeField] private List<ClickUIObject> bottomUIanim;
    [SerializeField] private int startSceneId;


    [HideInInspector] public int currentID;

    [SerializeField] private BedroomSystem bedroomSystem;

    [SerializeField] private ChestBehaviour chestBehaviour;

    [SerializeField] private SaveManager saveManager;

    [SerializeField] private GameObject watchSleepAD;
    [SerializeField] private PlayerStats playerStats;

    [HideInInspector] public AudioManager audioManager;
    [SerializeField] public GameObject Ambient;
    [SerializeField] private GameObject editButton;
    [SerializeField] private GameObject backgroundObjects;


    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        OpenScene(startSceneId);
    }

    public void OpenScene(int id)
    {
        
        ClickUI(id);
        for(int i = 0; i < scenes.Count; i++)
        {

            if (i == id)
            {
                currentID = id;

                if (scenesUI[i] != null)
                    scenesUI[i].SetActive(true);

                scenes[i].SetActive(true);

                if (scenes[i].GetComponent<SideRoomManager>() != null)
                {
                    SideRoomManager[] srm = scenes[i].GetComponents<SideRoomManager>();
                    foreach (SideRoomManager manager in srm)
                    {
                        manager.CheckCorrect();
                    }
                }



            }
            else
            {
                if (scenesUI[i] != null)
                    scenesUI[i].SetActive(false);

                scenes[i].SetActive(false);
            }

            exception(i);
        }

        HandleBackground();

        if (currentID == 6 && playerStats.sleepy<49f)
            watchSleepAD.SetActive(true);
        else
            watchSleepAD.SetActive(false);

        saveManager.SavePlayerStats();


    }


    void exception(int id)
    {
        if (bedroomSystem.goToSleep && id!=8)
        {
            bedroomSystem.SwitchLamp();
        }
    }

    void HandleBackground()
    {
        if (currentID == 2)
            backgroundObjects.SetActive(false);
        else
            backgroundObjects.SetActive(true);
    }

    void ClickUI(int i)
    {
        chestBehaviour.CloseMinigameUI();
        switch (i)
        {
            case 0:
                SetUI(0);
                audioManager.ForceLoopPlay("OutsideAmbient", Ambient);
                break;
            case 1:
                SetUI(1);
                audioManager.ForceLoopPlay("KitchenAmbient", Ambient);
                break;
            case 2:
                SetUI(2);
                audioManager.ForceLoopPlay("BathAmbient", Ambient);
                break;
            case 3:
                SetUI(2);
                audioManager.ForceLoopPlay("BathAmbient", Ambient);
                break;
            case 4:
                SetUI(2);
                audioManager.ForceLoopPlay("BathAmbient", Ambient);
                break;
            case 5:
                SetUI(1);
                Ambient.GetComponent<AudioSource>().Stop();
                break;
            case 6:
                SetUI(3);
                audioManager.ForceLoopPlay("RoomAmbient", Ambient);
                break;
            case 7:
                SetUI(3);
                audioManager.ForceLoopPlay("RoomAmbient", Ambient);
                break;
            case 8:
                SetUI(3);
                break;
        }
    }

    void SetUI(int id)
    {
        for(int i = 0; i < bottomUIanim.Count; i++)
        {
            if (i == id)
                bottomUIanim[i].isOpen = true;
            else
                bottomUIanim[i].isOpen =false;
        }
    }

    public void ClickButton()
    {
        audioManager.ForcePlay("SwipeSwoosh");
    }
    

    public void ForceEditButton(bool flag)
    {
        editButton.SetActive(flag);
    }


}
