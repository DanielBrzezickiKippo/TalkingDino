using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeBehaviour : TriggerAction
{
    private Manager sceneManager;
    [SerializeField] private ShopManager shopManager;
    public void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
    }

    public override void Action()
    {
        sceneManager.audioManager.ForcePlay("OpenFridge");
        sceneManager.OpenScene(5);
        shopManager.SpawnAssortment();
        sceneManager.ForceEditButton(false);
    }
}
