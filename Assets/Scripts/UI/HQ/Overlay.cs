using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    public virtual void Display() {
        Debug.Log($"Displaying {GetType()}");
    }

    public void GoBack() {
        FindObjectOfType<OverlayLoader>().GoBack(this);
    }
}
