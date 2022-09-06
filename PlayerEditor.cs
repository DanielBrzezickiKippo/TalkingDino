using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Skin
{
    public GameObject skin;
    public string name;
    public float cost;
    public int type;
}


public class PlayerEditor : MonoBehaviour
{
    public int currentSkin = 0;
    [SerializeField] private List<Skin> skins;
    [HideInInspector]public List<bool> skinsBought;
    [SerializeField] private Transform playerSkin;
    [SerializeField] private JellyManager jellyManager;

    [Header("UI")]
    [SerializeField] private GameObject BottomPanelUI;
    [SerializeField] private GameObject playerEditorUI;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private GameObject LockedUI;
    [SerializeField] private TextMeshProUGUI reqLevel;

    [SerializeField] private Image costImage;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private Sprite[] costImageSprites;
    

    [SerializeField] private Button buyBut;

    [SerializeField] private SaveManager saveManager;

    private Vector3 vectorOfSkin = new Vector3(0f, -0.471f, 0f);



    private PlayerStats playerStats;

    private Manager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerStats>();
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
        closeUI();
        //createSkinsData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createSkinsData()
    {
        skinsBought = new List<bool>();
        for(int i = 0; i < skins.Count; i++)
        {
            if (i<=playerStats.GetLevel())
                skinsBought.Add(true);
            else
                skinsBought.Add(false);


            /*if (i == 0)
                skinsBought.Add(true);
            else
            {
                if (playerStats.GetLevel() >= i)
                    skinsBought.Add(true);
                else
                    skinsBought.Add(false);

            }*/
                   
        }
    }

    void SetUI()
    {
        if (playerStats.GetLevel() >= currID)
            LockedUI.SetActive(false);
        else {
            LockedUI.SetActive(true);
            reqLevel.text = "Lv." + currID;
        }


        title.text = skins[currID].name;

        OnlySelectSystem();
    }

    /*void BuySystem()
    {

        if (!skinsBought[currID])
        {
            costImage.gameObject.SetActive(true);
            cost.text = skins[currID].cost.ToString();
            buyBut.GetComponent<Image>().sprite = costImageSprites[0];
            costImage.sprite = sprite[skins[currID].type];

            buyBut.onClick.RemoveAllListeners();
            buyBut.onClick.AddListener(() => {
                if (skins[currID].type == 0 && playerStats.GetMoney() >= skins[currID].cost)
                {
                    sceneManager.audioManager.ForcePlay("Woohoo");
                    sceneManager.audioManager.ForcePlay("CashRegister");

                    playerStats.DealMoney(skins[currID].cost);
                    currentSkin = currID;
                    skinsBought[currID] = true;
                    SetUI();
                    saveManager.SaveSkin();
                }
                else if (skins[currID].type == 1 && playerStats.GetGems() >= skins[currID].cost)
                {
                    sceneManager.audioManager.ForcePlay("Woohoo");
                    sceneManager.audioManager.ForcePlay("CashRegister");

                    playerStats.DealGems(skins[currID].cost);
                    currentSkin = currID;
                    skinsBought[currID] = true;
                    SetUI();
                    saveManager.SaveSkin();
                }
            });
        }
        else
        {
        costImage.gameObject.SetActive(false);
        if (currID != currentSkin)
        {
            buyBut.GetComponent<Image>().sprite = costImageSprites[1];
            cost.text = "SELECT";
            buyBut.onClick.RemoveAllListeners();
            buyBut.onClick.AddListener(() =>
            {
                sceneManager.audioManager.ForcePlay("Woohoo");
                currentSkin = currID;
                saveManager.SaveSkin();
                SetUI();
            });
        }
        else
        {
            buyBut.GetComponent<Image>().sprite = costImageSprites[2];
            cost.text = "SELECTED";
        }
        }
    }*/

    void OnlySelectSystem()
    {
        costImage.gameObject.SetActive(false);
        if (currID != currentSkin)
        {
            buyBut.GetComponent<Image>().sprite = costImageSprites[1];
            cost.text = "SELECT";
            buyBut.onClick.RemoveAllListeners();
            buyBut.onClick.AddListener(() =>
            {
                sceneManager.audioManager.ForcePlay("Woohoo");
                currentSkin = currID;
                saveManager.SaveSkin();
                SetUI();
            });
        }
        else
        {
            buyBut.GetComponent<Image>().sprite = costImageSprites[2];
            cost.text = "SELECTED";
        }
    }

    void PreviewSkin()
    {
        Animator skin = playerSkin.GetComponentInChildren<Animator>();
        Destroy(skin.gameObject);

        GameObject obj = Instantiate(skins[currID].skin, playerSkin);
        obj.transform.localPosition = vectorOfSkin;

        SetUI();

        jellyManager.CheckJelly();
    }

    int currID = 0;
    public void Right()
    {
        sceneManager.audioManager.ForcePlay("PopSplash");

        if (currID + 1 <= skins.Count-1)
            currID++;
        else
            currID = 0;

        PreviewSkin();
    }

    public void Left()
    {
        sceneManager.audioManager.ForcePlay("PopSplash");

        if (currID - 1 >= 0)
            currID--;
        else
            currID = skins.Count - 1;

        PreviewSkin();
    }


    public void SetSkin()
    {

        Animator skin = playerSkin.GetComponentInChildren<Animator>();
        Destroy(skin.gameObject);

        GameObject obj = Instantiate(skins[currentSkin].skin, playerSkin);
        obj.transform.localPosition = vectorOfSkin;


        jellyManager.CheckJelly();
        PlayerPrefs.SetInt("currentSkin", currentSkin);
    }

    public void closeUI()
    {
        BottomPanelUI.SetActive(true);
        playerEditorUI.SetActive(false);
    }

    public void openUI()
    {
        BottomPanelUI.SetActive(false);
        playerEditorUI.SetActive(true);
        PreviewSkin();
    }


}
