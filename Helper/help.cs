using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class help : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        helpers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void helpers()
    {
        MeshRenderer[] gos = GetComponentsInChildren<MeshRenderer>();
        Debug.Log(gos.Length);
        for(int i = 0; i < gos.Length/2; i++)
        {
            gos[i + (gos.Length / 2)].gameObject.transform.localPosition = gos[i].gameObject.transform.localPosition;
            gos[i + (gos.Length / 2)].gameObject.transform.localRotation = gos[i].gameObject.transform.localRotation;
        }
    }
}
