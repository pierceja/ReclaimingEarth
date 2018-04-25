using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    public Transform player;

    private void Start()
    {
        player = this.GetComponentInParent<Transform>();
    }

    void LateUpdate ()
    {
        // MiniMap follows player
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        // MiniMap rotates with player
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
