using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    ParticleManager particleManager;

    void Start()
    {
        particleManager = GameObject.FindGameObjectWithTag("particleManager").GetComponent<ParticleManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag ==)
        if (collision.gameObject.tag == "destructables")
        {
            int r = Random.Range(0, particleManager.Explosion.Count);
            particleManager.SpawnParticle(particleManager.Explosion[r], transform.position);

            //r = Random.Range(0, particleManager.TextEffects.Count);
            //particleManager.SpawnParticle(particleManager.TextEffects[r], transform.position);

            //baseball.obstacles.Remove(collision.gameObject.GetComponentInChildren<Transform>());
            collision.gameObject.GetComponent<DestructableObject>().ChangeSkin();
            //Destroy(collision.gameObject);
        }
    }
}
