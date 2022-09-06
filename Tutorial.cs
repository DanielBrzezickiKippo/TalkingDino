using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    float timeToInvokeAnim=1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        Invoke("ShowAnim", timeToInvokeAnim);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowAnim()
    {
        if (this.gameObject.activeSelf)
        {
            animator.Play("tut1");
            Invoke("ShowAnim",Random.Range(3f,5f));
        }
    }

}
