using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    [SerializeField] private bool curtain = false;
    [SerializeField] private Vector3 pos1;
    [SerializeField] private Vector3 pos2;

    private float speed = 0.05f;


    // Update is called once per frame
    void Update()
    {
        if (curtain)
            this.gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, pos1, speed);
        else
            this.gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, pos2, speed);
    }

    public void SwitchCurtain()
    {
        if (curtain)
            curtain = false;
        else
            curtain = true;
    }

}
