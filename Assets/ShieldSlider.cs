using UnityEngine;
using UnityEngine.UI;

public class ShieldSlider : MonoBehaviour
{
    public void UpdateValue(float value)
    {
        GetComponent<Slider>().value = value;
    }
}
