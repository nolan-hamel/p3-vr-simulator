using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class wolfScript : MonoBehaviour
{
    public UnityEvent wolfEatEvent;
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rabbit")
        {
            wolfEatEvent.Invoke();
        }
    }
}