using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerNetworkMover : Photon.MonoBehaviour
{

    public delegate void Respawn(float time);
    public event Respawn RespawnMe;

    Vector3 position;
    Quaternion rotation;
    float smoothing = 10f;
    float health = 100f;

    Animator anim;
    bool idle = true;
    bool walking = false;
    bool running = false;
    bool left = false;
    bool right = false;
    bool jump = false;

    SimpleHealthBar healthBar;
    int passedTime = 0;

    void Start()
    {
        healthBar = GameObject.Find("/Player HUD/HealthUI/HealthBar").GetComponent<SimpleHealthBar>();

        anim = GetComponentInChildren<Animator>();
        if (photonView.isMine)
        {
            GetComponentInChildren<Shoot_assult>().enabled = true;
            GetComponent<FirstPersonController>().enabled = true;
            GetComponentInChildren<ModelSwitch>().enabled = true;
            GetComponentInChildren<Pistol>().enabled = true;
            
            foreach (Camera cam in GetComponentsInChildren<Camera>())
                cam.enabled = true;
        }
        else
        {
            StartCoroutine("UpdateData");
        }
    }

    IEnumerator UpdateData()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * smoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothing);
            anim.SetBool("Idle", idle);
            anim.SetBool("Walking", walking);
            anim.SetBool("Running", running);
            anim.SetBool("Left", left);
            anim.SetBool("Right", right);
            anim.SetBool("Jump", jump);
            yield return null;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(health);
            stream.SendNext(anim.GetBool("Idle"));
            stream.SendNext(anim.GetBool("Walking"));
            stream.SendNext(anim.GetBool("Running"));
            stream.SendNext(anim.GetBool("Left"));
            stream.SendNext(anim.GetBool("Right"));
            stream.SendNext(anim.GetBool("Jump"));
        }
        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
            health = (float)stream.ReceiveNext();
            idle = (bool)stream.ReceiveNext();
            walking = (bool)stream.ReceiveNext();
            running = (bool)stream.ReceiveNext();
            left = (bool)stream.ReceiveNext();
            right = (bool)stream.ReceiveNext();
            jump = (bool)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void GetShot(float damage)
    {   
        if (photonView.isMine)
        {
            health -= damage;
            // Updates health bar
            healthBar.UpdateBar(health, 100);

            if (health <= 0)
            {
                if (RespawnMe != null)
                    RespawnMe(3f);

                PhotonNetwork.Destroy(gameObject);
            }
        }
        
    }

    //void FixedUpdate()
    //{
    //    passedTime += 1;

    //    if (passedTime == 90)
    //    {
    //        GetShot(5);
    //        passedTime = 0;
    //    }
    //}
}