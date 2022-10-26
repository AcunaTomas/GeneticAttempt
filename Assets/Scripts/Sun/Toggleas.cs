using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Toggleas : MonoBehaviour
{
    Toggle a;
    void Start()
    {
        a = GetComponent<Toggle>();
    }

    public bool selected()
    {
        return a.isOn;
    }
}
