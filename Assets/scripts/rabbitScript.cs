using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class rabbitScript : MonoBehaviour
{
    public UnityEvent rabbitEatEvent;

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Carrot")
        {
            rabbitEatEvent.Invoke();
        }
    }
}
