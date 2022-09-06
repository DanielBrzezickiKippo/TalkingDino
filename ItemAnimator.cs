using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemAnimator : MonoBehaviour
{

    //0-coin
    //1-gem
    //2-exp
    //[SerializeField] private int type;
    Canvas canvas;

    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private List<Transform> targetsPos;


    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI gemsText;
    [SerializeField] private TextMeshProUGUI expText;

    public int angle = 0;

    public float recoil = 150f;
    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();

        SetPos();
        SetUI();

    }

    public void SetPos()
    {
        for(int i=0;i<targets.Count;i++){

           // Debug.Log(gameObject.transform.InverseTransformPoint(targetsPos[i].position));
            targets[i].transform.localPosition = gameObject.transform.InverseTransformPoint(targetsPos[i].position);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn(int type, int amount)
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition, canvas.worldCamera,
            out movePos);

        //transform.position = canvas.transform.TransformPoint(movePos);

        for (int i = 0; i < amount; i++)
        {
            GameObject o = Instantiate(prefabs[type], gameObject.transform);

            o.transform.localRotation = Quaternion.Euler(0f, 0f, angle);

            Vector3 finalPos = canvas.transform.TransformPoint(movePos);

            o.transform.position = new Vector3(finalPos.x + Random.Range(-recoil, recoil), finalPos.y);//canvas.transform.TransformPoint(movePos);

            o.GetComponent<FollowObject>().Init(targets[type]);
        }
    }

    private RewardHandler rewardHandler;
    public void SetUI()
    {
        if (rewardHandler == null)
            rewardHandler = GameObject.FindGameObjectWithTag("RewardHandler").GetComponent<RewardHandler>();


        if(coinsText!=null)
            coinsText.text = rewardHandler.coins.ToString();
        if (expText != null)
            expText.text = rewardHandler.exp.ToString();
        if (gemsText != null)
            gemsText.text = rewardHandler.gems.ToString();

        //coinsText.text = 
    }




}
