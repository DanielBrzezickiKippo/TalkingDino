using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMG : MonoBehaviour
{
    Transform bat;
    Rigidbody rb;

    public float m_Thrust = 10f;

    BaseballMG baseball;
    ParticleManager particleManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseball = GetComponentInParent<BaseballMG>();
        bat = baseball.hitPoint;

        particleManager = baseball.particleManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, bat.position) < 0.2f && baseball.shouldBallBeHit())
        {

            baseball.playerStats.rewardHandler.GReward(0, Random.Range(0, 3));
            baseball.playerStats.rewardHandler.GReward(1, Random.Range(0, 10));
            baseball.playerStats.rewardHandler.GiveEarnings();
            Hit(Vector3.Distance(transform.position, bat.position));
        }
    }

    private void Hit(float distance)
    {
        float strength=0f;
        if (distance >= 0.15f)
            strength = Random.Range(0.3f, 0.5f);
        else if (distance < 0.15f && distance > 0.1f)
            strength = Random.Range(0.6f, 0.8f);
        else if (distance < 0.1f)
            strength = 1f;


        MessageManager messageManager = baseball.messageManager;
        List<string> praises = messageManager.praise;
        messageManager.ShowMessage(praises[Random.Range(0, praises.Count)],100f);

        rb.mass = 1f;
        rb.drag = 0f;
        rb.angularDrag = 0.05f;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Transform obstacle = baseball.obstacles[Random.Range(0,baseball.obstacles.Count)];
        float x = obstacle.position.x- transform.position.x;//Random.Range(-3f, 3f)
        float y = obstacle.position.y- transform.position.y;// +4.5f;//Random.Range(2f,7f)
        float z = obstacle.position.z - transform.position.z;
        Vector3 dir = obstacle.position - transform.position;
        rb.AddForce(dir *2.5f*strength ,ForceMode.Impulse);
        //Debug.Log("Ale zakurwil");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag ==)
        if(collision.gameObject.tag == "destructables")
        {
            Camera.main.gameObject.GetComponent<PlayerStats>().AddFun(Random.Range(8, 15));

            int r = Random.Range(0, particleManager.Explosion.Count);
            particleManager.SpawnParticle(particleManager.Explosion[r],transform.position);

            //r = Random.Range(0, particleManager.TextEffects.Count);
            //particleManager.SpawnParticle(particleManager.TextEffects[r], transform.position);

            //baseball.obstacles.Remove(collision.gameObject.GetComponentInChildren<Transform>());
            collision.gameObject.GetComponent<DestructableObject>().ChangeSkin();
            //Destroy(collision.gameObject);
        }
    }
}
