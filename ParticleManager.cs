using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] float maxTime = 5f;

    public List<GameObject> Explosion;
    public List<GameObject> TextEffects;

    public List<GameObject> happyEmoji;
    public List<GameObject> funEmoji;
    public List<GameObject> toiletEmoji;
    public List<GameObject> hungerEmoji;
    public List<GameObject> sleepyEmoji;

    float time;

    Vector3 oldPos;
    PlayerStats playerStats;

// Start is called before the first frame update
    void Start()
    {
        playerStats = Camera.main.gameObject.GetComponent<PlayerStats>();
        oldPos = player.transform.position;
        time = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        EmojiHandler();
    }

    public void EmojiHandler()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            if (player.activeSelf)
            {
                SpawnAdequeteEmoji();
                time = maxTime;
            }
        }
    }

    public void SpawnParticle(GameObject particleObj, Vector3 pos)
    {
        GameObject p = Instantiate(particleObj, pos, Quaternion.identity);
        Destroy(p, 3f);
    }

    public void SpawnEmoji(List<GameObject> emojis)
    {
        //Vector3 pos = player.transform.position;
        Vector3 pos = player.activeSelf == true ? player.transform.position : oldPos;
        int p = Random.Range(0, 100);
        float x = 0;
        if (p<50)
            x = pos.x + Random.Range(-1.2f,-0.75f);
        else
            x = pos.x + Random.Range(0.75f, 1.2f);
        float y = pos.y + Random.Range(0.5f, 1.2f);
        float z = pos.z;

        GameObject e = Instantiate(emojis[Random.Range(0, emojis.Count)], new Vector3(x, y, z), Quaternion.identity);
        Destroy(e.gameObject, 4f);
    }

    public void SpawnAdequeteEmoji()
    {
        List<GameObject> ems = new List<GameObject>();
        if (playerStats.higene < 15f)
            ems = toiletEmoji;
        else if (playerStats.hunger < 30f)
            ems = hungerEmoji;
        else if (playerStats.sleepy < 10f)
            ems = sleepyEmoji;
        else if (playerStats.fun < 50f)
            ems = funEmoji;

        if (ems.Count > 0)
            SpawnEmoji(ems);
    }

}
