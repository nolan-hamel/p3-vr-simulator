using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VRButtonController : MonoBehaviour
{
    [SerializeField] Collider pressCollider;
    [SerializeField] GameObject pushable;
    [SerializeField] float unpressedHeight = 0.125f;
    [SerializeField] float pressedHeight = 0.1f;

    public UnityEvent onPress;
    public UnityEvent onRelease;
    public bool isPressed;

    private GameObject presser;

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

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            pushable.transform.localPosition = new Vector3(0, pressedHeight, 0);
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            pushable.transform.localPosition = new Vector3(0, unpressedHeight, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void Test()
    {
        Debug.Log("Button Working!");
    }
}
