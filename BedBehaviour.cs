using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedBehaviour : TriggerAction
{
    private Manager sceneManager;

    public void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
    }

    public override void Action()
    {
        sceneManager.audioManager.ForcePlay("JumpOnBed");
        sceneManager.OpenScene(8);
        sceneManager.ForceEditButton(false);
    }
}
