using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    [SerializeField] float scrollSpeedMax = 5f;

    Material backgroundMaterial;
    Vector2 offset;

    private void Start()
    {
        backgroundMaterial = GetComponent<MeshRenderer>().material;
        offset = new Vector2(scrollSpeed, 0);
    }

    private void Update()
    {
        backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;
    }

    public void UpdateVelocity(float speed)
    {
        offset = new Vector2(Mathf.Clamp(scrollSpeed + speed, 0, scrollSpeedMax), 0);
    }
}
