using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPhone : MonoBehaviour
{
    private void OnMouseDown()
    {
        PlayerPrefs.DeleteAll();
    }
}
