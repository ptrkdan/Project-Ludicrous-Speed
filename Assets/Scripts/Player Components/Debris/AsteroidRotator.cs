using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotator : MonoBehaviour
{
    [SerializeField] float rotateSpeedMin = 0f;
    [SerializeField] float rotateSpeedMax = 90f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Random.Range(rotateSpeedMin, rotateSpeedMax) * Time.deltaTime);
    }
}
