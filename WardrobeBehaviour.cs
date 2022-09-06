using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeBehaviour : TriggerAction
{

    private Manager sceneManager;
    private PlayerEditor playerEditor;
    //private GameObject toOpen;


    public void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();
        playerEditor = GameObject.FindGameObjectWithTag("PlayerEditor").GetComponent<PlayerEditor>();
    }

    public override void Action()
    {
        sceneManager.audioManager.ForcePlay("DoorOpen");
        sceneManager.OpenScene(7);
        playerEditor.openUI();
    }
}
