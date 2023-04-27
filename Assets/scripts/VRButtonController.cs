using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VRButtonController : MonoBehaviour
{
    // Configs
    [SerializeField] GameObject pushable;
    [SerializeField] float unpressedHeight = 0.125f;
    [SerializeField] float pressedHeight = 0.1f;

    // Function Variables
    private GameObject lastPresser;

    [SerializeField] Vector3 teleportLocation = Vector3.zero;

    // Events and States
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        pushable.transform.localPosition = new Vector3(0, unpressedHeight, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeleportTo()
    {
        if (lastPresser)
        {
            lastPresser.transform.position = teleportLocation;
            Debug.Log($"teleporting to: {teleportLocation.x} {teleportLocation.y} {teleportLocation.z}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            pushable.transform.localPosition = new Vector3(0, pressedHeight, 0);
            lastPresser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == lastPresser)
        {
            pushable.transform.localPosition = new Vector3(0, unpressedHeight, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }
}
