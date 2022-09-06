using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathBehaviour : TriggerAction
{
    private Manager sceneManager;
    

    public void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
    }

    public override void Action()
    {
        sceneManager.audioManager.ForcePlay("WaterSplash");
        sceneManager.OpenScene(3);
        sceneManager.ForceEditButton(false);
    }
}
