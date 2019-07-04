using UnityEngine;
using UnityEngine.UI;

public class HullArmourSlider : MonoBehaviour
{
    public void UpdateValue(float value)
    {
        GetComponent<Slider>().value = value;
    }
}
