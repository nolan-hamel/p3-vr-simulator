using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InfiniteSocket : MonoBehaviour
{

    public GameObject prefabToSpawn;
    // Start is called before the first frame update
    public void Refill(XRSocketInteractor sock)
    {
        Instantiate(prefabToSpawn, sock.transform.position, Quaternion.identity);
    }

}
