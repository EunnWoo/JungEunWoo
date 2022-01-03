using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    public GameObject direction_light;
    public Transform DropDown;
    void Start()
    {

    }

    void Update()
    {
        if (DropDown.GetComponent<Dropdown>().value == 0)
        {
            direction_light.GetComponent<Light>().shadows = LightShadows.Soft;
        }

        if (DropDown.GetComponent<Dropdown>().value == 1)
        {
            direction_light.GetComponent<Light>().shadows = LightShadows.Hard;
        }
        if (DropDown.GetComponent<Dropdown>().value == 2)
        {
            direction_light.GetComponent<Light>().shadows = LightShadows.None;
        }
    }
}
