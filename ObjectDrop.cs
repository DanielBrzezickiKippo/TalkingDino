using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    public Vector3 mainPos = new Vector3(0f,0.85f,-5.5f);
    public float speed = 0.005f;

    public float delay = 0.5f;
    public float time = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(transform.up*5.5f, ForceMode.Impulse);
        transform.GetComponent<MeshCollider>().isTrigger = false;
        Invoke("StopScript", time);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, mainPos, speed);
    }

    void StopScript()
    {
        GetComponent<ObjectDrop>().enabled = false;
    }

}
