using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class carrotScript : MonoBehaviour
{
    public float HungerValue = 10;

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
