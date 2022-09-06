using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private Vector3 startPos;
    float y = -0.4f;
    float z = 6.6f;
    private Vector3 destinationPos;

    [SerializeField] float speed=1f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.localPosition;
        RestartPos();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePos();
    }

    public void HandlePos()
    {
        if (Vector3.Distance(gameObject.transform.localPosition, destinationPos) > 0.2f)
        {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, destinationPos, speed * Time.deltaTime);
        }
    }

    public void RestartPos()
    {

        destinationPos = new Vector3(Random.Range(-2.2f,2.2f),
            Random.Range(y-0.3f,y+0.3f),
            z
            );
        Vector3 pos = new Vector3(destinationPos.x,
            -4.5f,
            z
            );

        gameObject.transform.localPosition = pos;

    }

}
