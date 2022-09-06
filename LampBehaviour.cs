using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampBehaviour : TriggerAction
{
    [SerializeField] private BedroomSystem bedroomSystem;

    private Manager sceneManager;

    public void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Manager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Action()
    {
        sceneManager.audioManager.ForcePlay("SwitchLamp");
        sceneManager.audioManager.ForcePlay("JumpOnBed");
        bedroomSystem.SwitchLamp();
    }

}
