// GameDev.tv Challenge Club. Got questions or want to share your nifty solution?
// Head over to - http://community.gamedev.tv

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private PlayerMovement player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        print("collision");
        print(other);
        if (other.gameObject.CompareTag("Rock"))
        {
            print("collisionWithRock");
            Destroy(other);
            player.SetTargetBlock(other.gameObject);
        }
    }
}
