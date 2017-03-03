using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHappen : MonoBehaviour {
    public GameObject info;

    public void InfoOpen()
    {
       info.SetActive(true);
    }

    public void InfoClose()
    {
        info.SetActive(false);
    }
}
