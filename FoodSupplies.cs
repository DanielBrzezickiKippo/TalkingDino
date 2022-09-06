using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Food
{
    public int id;
    public string name;
    public GameObject foodObject;
    public float cost;
}

[System.Serializable]
public class FoodData
{
    public int id;
    public int amount;
}


public class FoodSupplies : MonoBehaviour
{
    [SerializeField] public List<FoodData> playerSupplies;
    public List<Food> food;
    public Food freeFood;

    [SerializeField] private GameObject[] platePos;


    private List<GameObject> generatedFood;

    [HideInInspector] public List<GameObject> assortmentObjects;
    int currentFood = 0;

    [SerializeField] private SaveManager saveManager;

    [HideInInspector] public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float RemoveObject(GameObject obj)
    {
        int id=-1;
        for(int i = 0; i < generatedFood.Count; i++)
        {
            if (generatedFood[i] == obj)
            {
                id = playerSupplies[i].id;
                break;
            }
        }

        if (id != -1)
        {
            generatedFood.RemoveAt(id);
            playerSupplies.RemoveAt(id);

            saveManager.SaveFood();
            Destroy(obj);

            GenerateFood();
        }

        return food[id].cost;
    }

    public void ClearAssortment()
    {
        if (assortmentObjects.Count > 0)
        {
            foreach (GameObject go in assortmentObjects)
            {
                Destroy(go);
            }
            assortmentObjects.Clear();
        }
    }

    public void GenerateFood()
    {
        ClearAssortment();

        if (generatedFood==null)
            generatedFood = new List<GameObject>();
        if (generatedFood.Count > 0)
        {
            foreach (GameObject f in generatedFood)
            {
                Destroy(f);
            }
            generatedFood.Clear();
        }

        for(int i=0;i<playerSupplies.Count;i++)
        {
            GameObject pos;
            if (i < 3)
                pos = platePos[i+1];
            else
                pos = platePos[4];

            GameObject foodCreated;
            if (playerSupplies[i].id != 16)
                foodCreated = Instantiate(food[playerSupplies[i].id].foodObject, transform);
            else
                foodCreated = Instantiate(freeFood.foodObject, transform);

            if (foodCreated.GetComponent<MoveItem>() == null)
                foodCreated.AddComponent<MoveItem>();

            if (foodCreated.GetComponent<FoodBehaviour>() == null)
                foodCreated.AddComponent<FoodBehaviour>();

            float scale = 1.525f;
            foodCreated.transform.localScale = new Vector3(scale, scale, scale);
            foodCreated.name = "foodPrefab" + i;
            foodCreated.transform.position = pos.transform.position;
            foodCreated.GetComponent<MoveItem>().mainPlace = pos.transform;
            foodCreated.GetComponent<MoveItem>().zDistance = 6.1f;

            generatedFood.Add(foodCreated);
        }


        for(int i = 0; i < currentFood; i++)
        {
            Right();
        }

    }

    public void Right()
    {
        if (playerSupplies.Count > 3)
        {
            MoveItem lastFoodMI = generatedFood[generatedFood.Count - 1].GetComponent<MoveItem>();
            if (lastFoodMI.mainPlace.position != platePos[3].transform.position)
            {
                currentFood++;

                for (int i = 0; i < generatedFood.Count; i++)
                {
                    MoveItem foodMI = generatedFood[i].GetComponent<MoveItem>();
                    if (foodMI.mainPlace.position == platePos[0].transform.position)
                        foodMI.mainPlace = platePos[0].transform;
                    else if (foodMI.mainPlace.position == platePos[1].transform.position)
                        foodMI.mainPlace = platePos[0].transform;
                    else if (foodMI.mainPlace.position == platePos[2].transform.position)
                        foodMI.mainPlace = platePos[1].transform;
                    else if (foodMI.mainPlace.position == platePos[3].transform.position)
                        foodMI.mainPlace = platePos[2].transform;
                    else if (foodMI.mainPlace.position == platePos[4].transform.position)
                    {
                        foodMI.mainPlace = platePos[3].transform;
                        break;
                    }

                }
            }
        }
    }

    public void Left()
    {
        if (playerSupplies.Count > 3)
        {
            MoveItem firstFoodMI = generatedFood[0].GetComponent<MoveItem>();
            if (firstFoodMI.mainPlace.position != platePos[1].transform.position)
            {
                currentFood--;
                for (int i = generatedFood.Count - 1; i >= 0; i--)
                {
                    MoveItem foodMI = generatedFood[i].GetComponent<MoveItem>();
                    if (foodMI.mainPlace.position == platePos[4].transform.position)
                        foodMI.mainPlace = platePos[4].transform;
                    else if (foodMI.mainPlace.position == platePos[3].transform.position)
                        foodMI.mainPlace = platePos[4].transform;
                    else if (foodMI.mainPlace.position == platePos[2].transform.position)
                        foodMI.mainPlace = platePos[3].transform;
                    else if (foodMI.mainPlace.position == platePos[1].transform.position)
                        foodMI.mainPlace = platePos[2].transform;
                    else if (foodMI.mainPlace.position == platePos[0].transform.position)
                    {
                        foodMI.mainPlace = platePos[1].transform;
                        break;
                    }
                }
            }
        }
    }


    public void AddPlayerFood(int id)
    {
        FoodData fd = new FoodData();
        fd.id = id;
        fd.amount = 1;
        playerSupplies.Add(fd);
        saveManager.SaveFood();
    }

    /*public void GeneratePlates()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject plate = Instantiate(platePrefab, transform);
            Vector3 platePos = new Vector3(
    plate.transform.position.x,
    plate.transform.position.y + 0.8f,
    plate.transform.position.z
);




            GameObject pos = Instantiate(new GameObject(), transform);

            Vector3 objectPos = new Vector3(
                plate.transform.position.x,
                plate.transform.position.y + 0.8f,
                plate.transform.position.z
            );
            pos.transform.position = objectPos;
        }

    }*/

}
