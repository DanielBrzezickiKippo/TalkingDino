using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadBehaviour : TriggerAction
{
    [SerializeField] private GameObject gamesUI;

    public override void Action()
    {
        gamesUI.SetActive(true);
    }
}
