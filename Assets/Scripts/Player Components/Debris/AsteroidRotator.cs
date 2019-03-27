using UnityEngine;

public class AsteroidRotator : MonoBehaviour
{
    [SerializeField] float rotateSpeedMin = -90;
    [SerializeField] float rotateSpeedMax = 90f;

    float rotateSpeed = 0f;

    private void Start() {
        rotateSpeed = Random.Range(rotateSpeedMin, rotateSpeedMax);
    }

    private void Update() {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
