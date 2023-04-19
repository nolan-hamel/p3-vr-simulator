using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTeleport : MonoBehaviour
{
    public GameObject player;
    public void Shrink()
    {
        player.transform.localScale = Vector3.one;
    }

    public void Grow()
    {
        player.transform.localScale = new Vector3(1000,1000,1000);
    }
}
