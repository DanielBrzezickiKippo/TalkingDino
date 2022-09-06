using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable]
public class BoughtObject//??
{
    public int id;
    public bool bought;
    public bool selected;
}

[System.Serializable]
public class BuyObject
{
    public string name;
    public int id;
    public int cost;

    //0-money
    //1-gems
    public int type;
    public GameObject obj;
}

[System.Serializable]
public class Selectable
{
    [SerializeField] public int pickedID;
    [SerializeField] public Transform originalTransform;
    [SerializeField] public List<BuyObject> selectableObjects;
}

public class MapEditor : MonoBehaviour
{
    Manager manager;

    public List<GameObject> UIs;

    [SerializeField] public List<Selectable> selectables;
    [SerializeField] public List<List<BoughtObject>> boughtObjects;//toSave
    //[SerializeField] private Transform wardrobeTransform;
    //[SerializeField] private List<BuyObject> wardrobeObjects;
    public List<BuyObject> currentObjects;
   // private List<BoughtObject> boughtObject;
    private int currentID = 0;

    [Header("GamePanel")]
    [SerializeField] private GameObject gamePanel;
    [Header("EditorPanel")]
    [SerializeField] private GameObject editorPanel;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Image costImage;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private Sprite[] costImageSprites;


    [SerializeField] private Button closeBut;
    [SerializeField] private Button buyBut;


    private Transform currentTransform;

    private PlayerStats playerStats;

    [Header("Save System")]
    [SerializeField] private SaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerStats>();
        manager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
        closeUI();
        //createSelectablesData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createSelectablesData(List<List<BoughtObject>> bos=null)
    {
        if (bos == null)
        {
            boughtObjects = new List<List<BoughtObject>>();
            for (int i = 0; i < selectables.Count; i++)
            {
                List<BoughtObject> boughtObjectList = new List<BoughtObject>();
                for (int j = 0; j < selectables[i].selectableObjects.Count; j++)
                {
                    BoughtObject boughtObject = new BoughtObject();
                    boughtObject.id = j;
                    boughtObjectList.Add(boughtObject);
                    if (boughtObject.id == 0)
                    {
                        boughtObject.bought = true;
                        boughtObject.selected = true;
                    }
                }

                boughtObjects.Add(boughtObjectList);
            }
        }
        else
        {
            boughtObjects = bos;
            for (int i = 0; i < selectables.Count; i++)
            {
                for (int j = 0; j < selectables[i].selectableObjects.Count; j++)
                {
                    if (boughtObjects[i][j].selected)
                    {
                        selectables[i].pickedID = j;

                        currentTransform = selectables[i].originalTransform;

                        GameObject toBuy = Instantiate(selectables[i].selectableObjects[j].obj, currentTransform.parent.transform);

                        if (toBuy.GetComponent<Collider>() && toBuy.GetComponent<ClickableObject>()==null)
                            toBuy.GetComponent<Collider>().enabled = false;
                        toBuy.transform.position = currentTransform.position;
                        Destroy(currentTransform.gameObject);
                        currentTransform = toBuy.transform;
                        selectables[i].originalTransform = toBuy.transform;
                    }
                }
            }
        }
    }



    //0-homepage
    //1-kitchen
    //2-toilet
    //6-bedroom
    public void openUI()
    {
        int id = manager.currentID;
        closeUI();

        switch (id)
        {
            case 0:
                UIs[0].SetActive(true);
                closeBut.gameObject.SetActive(true);
                break;
            case 1:
                UIs[1].SetActive(true);
                closeBut.gameObject.SetActive(true);
                break;
            case 2:
                UIs[2].SetActive(true);
                closeBut.gameObject.SetActive(true);
                break;
            case 6:
                UIs[3].SetActive(true);
                closeBut.gameObject.SetActive(true);
                break;
            default:
                break;
        }


        closeBut.onClick.RemoveAllListeners();
        closeBut.onClick.AddListener(() =>
        {
            closeUI();
        });

        gamePanel.SetActive(false);
    }

    public void spawnOnClose()
    {
        for (int i = 0; i < selectables[selectedID].selectableObjects.Count; i++)
        {
            if (selectables[selectedID].selectableObjects[i].id == selectables[selectedID].pickedID)
            {
                GameObject obj = selectables[selectedID].selectableObjects[i].obj;
                GameObject picked = Instantiate(obj, currentTransform.parent.transform);
                if(picked.GetComponent<ClickableObject>()!=null)
                    picked.GetComponent<ClickableObject>().enabled = false;
                picked.transform.position = currentTransform.position;
                //Debug.Log(currentTransform.localRotation);
                picked.transform.localRotation= currentTransform.localRotation;
                Destroy(currentTransform.gameObject);
                currentTransform = picked.transform;
                selectables[selectedID].originalTransform = picked.transform;
                break;
            }
        }
        SetClickableObjectsInScene(true);
    }

    public void closeUI()
    {

        foreach (GameObject UI in UIs)
        {
            UI.SetActive(false);
        }
        editorPanel.SetActive(false);
        gamePanel.SetActive(true);
        closeBut.gameObject.SetActive(false);
    }

    void DeselectObjects(List<List<BoughtObject>> bos,int category)
    {
        for(int i = 0; i < bos[category].Count; i++)
        {
            bos[category][i].selected = false;
        }
    }

    public void SetUI(BuyObject obj)
    {
        int _id = obj.id;
        buyBut.onClick.RemoveAllListeners();
        title.text = obj.name;

        if (!boughtObjects[selectedID][currentID].bought)
        {
            costImage.gameObject.SetActive(true);
            buyBut.GetComponent<Image>().sprite = costImageSprites[0];
            cost.text = obj.cost.ToString();

            //Change image
            costImage.sprite = sprite[obj.type];

            buyBut.onClick.RemoveAllListeners();
            buyBut.onClick.AddListener(() => {
                if (obj.type == 0 && playerStats.GetMoney() >= obj.cost)
                {
                    manager.audioManager.ForcePlay("CashRegister");
                    
                    playerStats.DealMoney(obj.cost);
                    selectables[selectedID].pickedID = _id;
                    boughtObjects[selectedID][currentID].bought = true;
                    DeselectObjects(boughtObjects, selectedID);
                    boughtObjects[selectedID][currentID].selected = true;
                    SetUI(obj);
                    saveManager.SaveFurnitures();
                }
                else if (obj.type == 1 && playerStats.GetGems() >= obj.cost)
                {
                    manager.audioManager.ForcePlay("CashRegister");

                    playerStats.DealGems(obj.cost);
                    selectables[selectedID].pickedID = _id;
                    boughtObjects[selectedID][currentID].bought = true;
                    DeselectObjects(boughtObjects, selectedID);
                    boughtObjects[selectedID][currentID].selected = true;
                    SetUI(obj);
                    saveManager.SaveFurnitures();
                }
            });
        }
        else
        {
            costImage.gameObject.SetActive(false);
            if (selectables[selectedID].pickedID != obj.id)
            {
                buyBut.GetComponent<Image>().sprite = costImageSprites[1];
                cost.text = "SELECT";
                buyBut.onClick.RemoveAllListeners();
                buyBut.onClick.AddListener(() =>
                {
                    manager.audioManager.ForcePlay("Pop2");
                    selectables[selectedID].pickedID = _id;
                    DeselectObjects(boughtObjects, selectedID);
                    boughtObjects[selectedID][currentID].selected = true;
                    SetUI(obj);
                    saveManager.SaveFurnitures();
                });
            }
            else
            {
                buyBut.GetComponent<Image>().sprite = costImageSprites[2];
                cost.text = "SELECTED";
            }
        }
        /*switch (obj.type)
        {
            case 0:
                costImage = sprite[0];
                break;
            case 1:
                costImage.sprite = sprite[1];
                break;
            default:
                break;
        }*/

        GameObject toBuy = Instantiate(obj.obj, currentTransform.parent.transform);
        if (toBuy.GetComponent<ClickableObject>() != null)
            toBuy.GetComponent<ClickableObject>().enabled = false;
        if (toBuy.GetComponentInParent<ClickableObject>() != null)
            toBuy.GetComponentInParent<ClickableObject>().enabled = false;
        toBuy.transform.position = currentTransform.position;
        Destroy(currentTransform.gameObject);
        currentTransform = toBuy.transform;
        selectables[selectedID].originalTransform = toBuy.transform;
        //wardrobeTransform = toBuy.transform;
    }

    public void SetClickableObjectsInScene(bool turn)
    {
        ClickableObject[] cos = currentTransform.parent.GetComponentsInChildren<ClickableObject>();
        //Debug.Log(cos.Length + " znaleziono");
        if (cos.Length > 0)
        {
            foreach (ClickableObject co in cos)
            {
                co.enabled = turn;
            }
        }
    }

    int selectedID = 0;
    public void PickToChange(int id)
    {
        foreach (GameObject UI in UIs)
        {
            UI.SetActive(false);
        }


        selectedID = id;
        currentTransform = selectables[selectedID].originalTransform.transform;
        SetClickableObjectsInScene(false);

        LoadObjects(selectables[selectedID].selectableObjects);

        closeBut.onClick.RemoveAllListeners();
        closeBut.onClick.AddListener(() =>
        {
            
            spawnOnClose();
            closeUI();
        });

        /*if (id == 0)
        {
            currentTransform = selectables[selectedID].originalTransform.transform;
            SetClickableObjectsInScene(false);

            LoadObjects(selectables[selectedID].selectableObjects);

        }
        else if (id == 2)
            Debug.Log("chuj");*/

    }



    void LoadObjects(List<BuyObject> objs)
    {
        currentID = 0;
        currentObjects = objs;

        editorPanel.SetActive(true);
        SetUI(currentObjects[currentID]);
    }

    public void Next()
    {
        manager.audioManager.ForcePlay("Pop3");
        if (currentID + 1 < currentObjects.Count)
            currentID++;
        else
            currentID = 0;

        SetUI(currentObjects[currentID]);
    }

    public void Previous()
    {
        manager.audioManager.ForcePlay("Pop3");
        if (currentID>0)
            currentID--;
        else
            currentID = currentObjects.Count-1;

        SetUI(currentObjects[currentID]);
    }

}
