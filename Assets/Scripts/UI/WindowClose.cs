using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowClose : MonoBehaviour
{
    public void CloseTheWindow()
    {
        gameObject.SetActive(false);
    }
}
