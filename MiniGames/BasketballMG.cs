using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasketballMG : MonoBehaviour
{
    [SerializeField] private GameObject basketballPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private MessageManager messageManager;
    [SerializeField] private Animator basketballHoop;
     int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewBall(float delay)
    {
        StartCoroutine(Spawn(delay));
    }

    IEnumerator Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject ball = Instantiate(basketballPrefab, spawnPoint);
        
    }

    void ChangeBasketPos()
    {
        basketballHoop.SetInteger("pos", Random.Range(0, 3));
    }
    public void Add()
    {
        Invoke("ChangeBasketPos", 0.8f);
        count++;
        messageManager.ShowMessage(count.ToString(), 200f);
        //Debug.Log(count);
    }


}
