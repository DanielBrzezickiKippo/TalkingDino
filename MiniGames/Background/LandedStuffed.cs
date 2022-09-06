using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandedStuffed : MonoBehaviour
{
    [SerializeField] private float speed=0.5f;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    private bool side = true;

    //private float _normalSpeed;
    // Start is called before the first frame update
    void Start()
    {
       // _normalSpeed = speed;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    //float timer;
    //float TimeToNextJump = 0.45f;
    void HandleMovement()
    {
        /*timer -= Time.deltaTime;
        if (timer <= -0.1)
            timer = TimeToNextJump;
        else if (timer <= 0)
            speed = _normalSpeed/5f;
        else if (timer > 0)
            speed = _normalSpeed;*/

        if (side == true)
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, endPos, speed*Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.localPosition, endPos) <= 0.25f)
            {
                side = false;
                ChangeSide();
            }
        }
        else
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, startPos, speed * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.localPosition, startPos) <= 0.25f)
            {
                side = true;
                ChangeSide();
            }
        }
    }

    void ChangeSide() {
        gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x,
            gameObject.transform.localScale.y,
            gameObject.transform.localScale.z
            );
    }


    public void RestartPos()
    {
        gameObject.transform.localPosition = side ? startPos : endPos;
    }

}
