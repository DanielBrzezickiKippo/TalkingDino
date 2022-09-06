using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickUIObject : MonoBehaviour
{

    [SerializeField] public bool isOpen = false;
    private Vector3 posIn = new Vector3(0f,0f,0f);
    private Vector3 posOff = new Vector3(0f,-250f,0f);
    private float speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
            transform.localPosition = Vector3.Lerp(transform.localPosition, posIn, speed);
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, posOff, speed);
    }
}
