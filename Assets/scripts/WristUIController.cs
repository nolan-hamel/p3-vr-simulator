using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristUIController : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Transform headTransform;
    [SerializeField] float renderAngle;
    [SerializeField] List<Button> hideButtons;

    void Update()
    {
        float angle = Vector3.Angle(headTransform.forward, -transform.forward);
        if (angle > renderAngle) { 
            foreach(Button button in hideButtons)
            {
                button.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Button button in hideButtons)
            {
                button.gameObject.SetActive(false);
            }
        }
    }
}
