using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float maxRotation;
    public float speed=0.02f;

    public List<string> praise;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
    }

    public void HandleRotation()
    {
        //Debug.Log(text.gameObject.transform.localScale.x);
        if (text.gameObject.transform.localScale.x > 0f)
        {
            //Debug.Log(text.gameObject.transform.rotation);
            //Debug.Log(text.gameObject.transform.localEulerAngles);
            text.gameObject.transform.rotation = Quaternion.Lerp(text.gameObject.transform.rotation, Quaternion.Euler(0f, 0f, rotation), speed*Time.deltaTime);


        }
    }
    float rotation;
    public void ShowMessage(string msg,float fontSize)
    {
        rotation = Random.Range(-maxRotation, maxRotation);
        text.text = msg;
        text.fontSizeMax = fontSize;
        text.gameObject.GetComponent<Animator>().Play("SendMessage");
        text.gameObject.transform.localEulerAngles = Vector3.zero;

        text.gameObject.transform.localPosition = new Vector3(Random.Range(-150f, 150f),
        Random.Range(-700f, -300f),
        0f);


    }

}
