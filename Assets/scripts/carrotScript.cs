using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class carrotScript : MonoBehaviour
{
    void Start()
    {
        Debug.Log(transform.GetComponent<CapsuleCollider>().GetComponent<Rigidbody>());
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
