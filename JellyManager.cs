using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        CheckJelly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckJelly()
    {
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(SkinnedMeshRenderer smr in renderers)
        {
            if (smr.gameObject.GetComponent<JellyMesh>() == null)
                smr.gameObject.AddComponent<JellyMesh>();
        }
    }
}
