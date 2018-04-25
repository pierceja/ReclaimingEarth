using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float timeGone;
    float sparkTime = -100;
    GameObject spark;

    // The fly speed (used by the weapon later)
    public float speed = 2000.0f;

    // explosion prefab (particles)
    public GameObject explosionPrefab;

    // find out when it hit something
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag != "Bullet")
        {
            spark = Instantiate(explosionPrefab,
                        transform.position,
                        Quaternion.identity);

            sparkTime = 0f;
        }
        

    }

    void Update()
    {
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
