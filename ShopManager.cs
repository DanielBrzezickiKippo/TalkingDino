using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private FoodSupplies foodSupplies;
    [SerializeField] private Transform[] foodPos;
    [SerializeField] private TextMeshPro[] priceTexts;
    [SerializeField] private List<GameObject> createdAssortment;

    Manager sceneManager;
    public void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
    }

    public void SpawnAssortment()
    {
        if (createdAssortment.Count > 0)
        {
            foreach(GameObject go in createdAssortment)
            {
                Destroy(go);
            }
            createdAssortment.Clear();
        }

        for(int i = actualAssortment; i < actualAssortment + 4; i++)
        {
            if (foodSupplies.food.Count > i)
            {
                GameObject food = Instantiate(foodSupplies.food[i].foodObject, transform);
                food.name = "assortment" + i;
                food.transform.position = foodPos[i % 4].position;
                Destroy(food.GetComponent<MoveItem>());
                Destroy(food.GetComponent<FoodBehaviour>());
                if(food.GetComponent<MeshCollider>()!=null)
                    food.GetComponent<MeshCollider>().isTrigger = true;

                food.transform.localScale = new Vector3(2f, 2f, 2f);
                food.AddComponent<ClickableObject>();
                ShopItemBehaviour sih = food.AddComponent<ShopItemBehaviour>();
                sih.foodSupplies = foodSupplies;
                sih.id = foodSupplies.food[i].id;
                sih.cost = foodSupplies.food[i].cost;

                priceTexts[i%4].text = foodSupplies.food[i].cost + ".00";

                createdAssortment.Add(food);

            }
        }
    }

    int actualAssortment = 0;
    public void Right()
    {
        sceneManager.audioManager.ForcePlay("PopSplash");
        if (actualAssortment+4 < foodSupplies.food.Count) {
            actualAssortment += 4;
            SpawnAssortment();
        }
    }

    public void Left()
    {
        sceneManager.audioManager.ForcePlay("PopSplash");
        if (actualAssortment > 0)
        {
            actualAssortment -= 4;
            SpawnAssortment();
        }
    }

}
