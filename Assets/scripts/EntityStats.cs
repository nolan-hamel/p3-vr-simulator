using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityStats : MonoBehaviour
{
    [SerializeField] public GameObject prefab;

    [SerializeField] private TextMeshPro title;
    public void Update()
    {
        title.text = GameObject.FindGameObjectsWithTag(prefab.tag).Length.ToString();
    }
}
