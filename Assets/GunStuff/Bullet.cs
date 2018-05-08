using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    float timeGone;
    float sparkTime = -100;
    GameObject spark;
    float startTime;
    float damage = 35f;

    // The fly speed (used by the weapon later)
    public float speed = 2000.0f;

    // explosion prefab (particles)
    public GameObject explosionPrefab;

    //Range Handling
    public float timeToLive = 3f;

    void Start()
    {
        startTime = Time.time; 
    }

    // find out when it hit something
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            print("hit player");
            Destroy(gameObject);
            //c.gameObject.transform.GetComponent().RPC("GetShot", PhotonTargets.All, damage)
        }
        else if (c.gameObject.tag != "Bullet")
        {
            //spark = Instantiate(explosionPrefab,
             //           transform.position,
            //            Quaternion.identity);

            sparkTime = 0f;
            Destroy(gameObject);
        }
        

    }

    void Update()
    {
        if (Time.time - startTime > timeToLive)
        {
            Destroy(gameObject);
            Destroy(spark);
            return;
        }

        if (sparkTime !=-100)
        {
            sparkTime += 1;
            if (sparkTime == 10)
            {
                Destroy(spark);
                Destroy(gameObject);
            }
        }
        
    }
}
