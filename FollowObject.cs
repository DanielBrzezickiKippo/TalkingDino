using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject target;

    public bool coins = true;

    public Vector2 newVector;
    public float variance;
    public float speed;

    bool was = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    bool afterInit = false;
    public void Init(GameObject _target)
    {
        Vector2 currPos = gameObject.transform.localPosition;

        newVector = new Vector2(currPos.x - Random.RandomRange(-variance, variance), currPos.y - Random.RandomRange(-variance, variance));
        target = _target;

        speed = Random.RandomRange(0.3f, 0.4f);
        afterInit = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!was)
        {
            gameObject.transform.localPosition = Vector2.Lerp(gameObject.transform.localPosition, newVector, speed);
            if (Vector2.Distance(gameObject.transform.localPosition, newVector) < 5f)
                was = true;
        }
        else
            if(afterInit) Follow(target);
    }

    public void Follow(GameObject target)
    {
        Vector3 pos = target.transform.localPosition;
        gameObject.transform.localPosition = Vector2.Lerp(gameObject.transform.localPosition, pos, speed);

        if (Vector2.Distance(gameObject.transform.localPosition, pos) < 1f)
        {
            gameObject.transform.localScale = Vector2.Lerp(gameObject.transform.localScale, new Vector2(0f, 0f), speed);
            if (Vector2.Distance(gameObject.transform.localScale, new Vector2(0f, 0f)) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
        //gameObject.transform.localPosition = new Vector3(pos.x, pos.y, pos.z);

    }
}
