using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseballMG : MonoBehaviour
{
    [SerializeField] private Transform baseballBat;
    [SerializeField] public Transform hitPoint;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnPos;
    private float waitingY;
    private float hitY=60f;
    [SerializeField] private float hitSpeed;
    [SerializeField] private float comebackSpeed;
    [SerializeField] private float waitingSpeed;
    [SerializeField] public List<Transform> obstacles;
    [SerializeField] public MessageManager messageManager;
    [SerializeField] public ParticleManager particleManager;

    private baseballState state;

    public PlayerStats playerStats;

    enum baseballState
    {
        waitingFirst,
        waitingSecond,
        comeBack,
        hit
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = timeToNextBall;
        waitingY = baseballBat.localEulerAngles.y;
        destination = waitingY;
        //Debug.Log("tessssssssst "+baseballBat.localEulerAngles.y);
        state = baseballState.waitingFirst;

        playerStats = Camera.main.gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            state!=baseballState.hit &&
            state!=baseballState.comeBack)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Hit();
        }

        StateHandler();

        BallSpawner();
    }



    private float timer;
    private float timeToNextBall = 2f;
    void BallSpawner()
    {
        if (state == baseballState.waitingFirst || state == baseballState.waitingSecond)
        {
            if (timer <= 0f)
            {
                GameObject ball =Instantiate(ballPrefab, ballSpawnPos.transform);
                Destroy(ball.gameObject, 10f);
                timer = timeToNextBall;
            }
            else
                timer -= Time.deltaTime;
        }
    }

    float destination;
    void StateHandler()
    {
        switch (state)
        {
            case baseballState.waitingFirst:
                baseballBat.localEulerAngles = Vector3.LerpUnclamped(baseballBat.localEulerAngles, SetRotation(), waitingSpeed);
                if (Vector3.Distance(baseballBat.localEulerAngles, SetRotation()) < 1f)
                    StartCoroutine(ChangeState(baseballState.waitingSecond, waitingY-5f, 0f));
                break;
            case baseballState.waitingSecond:
                baseballBat.localEulerAngles = Vector3.LerpUnclamped(baseballBat.localEulerAngles, SetRotation(), waitingSpeed);
                if (Vector3.Distance(baseballBat.localEulerAngles, SetRotation()) < 1f)
                    StartCoroutine(ChangeState(baseballState.waitingFirst, waitingY + 5f, 0f));
                break;
            case baseballState.comeBack:
                baseballBat.localEulerAngles = Vector3.Lerp(baseballBat.localEulerAngles, SetRotation(), comebackSpeed);
                if(Vector3.Distance(baseballBat.localEulerAngles, SetRotation())<1f)
                    StartCoroutine(ChangeState(baseballState.waitingFirst, waitingY+5, 0f));
                break;
            case baseballState.hit:
                baseballBat.localEulerAngles = Vector3.Lerp(baseballBat.localEulerAngles, SetRotation(), hitSpeed);
                if (Vector3.Distance(baseballBat.localEulerAngles, SetRotation()) < 1f)
                    StartCoroutine(ChangeState(baseballState.comeBack, waitingY, 0f));
                break;
        }
    }

    public bool shouldBallBeHit()
    {
        return state != baseballState.comeBack;
    }

    IEnumerator ChangeState(baseballState newState,float y,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        state = newState;
        destination = y;
    }

    Vector3 SetRotation() {
        Vector3 v = new Vector3(0f, destination, 68.01f);
        return v;
    }


    void Hit()
    {
        if (playerStats == null)
            playerStats = Camera.main.gameObject.GetComponent<PlayerStats>();


        playerStats.AddFun(3f);

        StartCoroutine(ChangeState(baseballState.hit, hitY, 0f));
        StartCoroutine(ChangeState(baseballState.comeBack, waitingY, 0.8f));

    }





}
