using UnityEngine;

public class Interactable : MonoBehaviour
{
    private float radius = 1f;
    private Transform interactableTransform;

    #region Methods: Unity

    private void OnDrawGizmosSelected()

    {
        if (interactableTransform == null)
        {
            interactableTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Interact only with interactable objects
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable)
        {
            Interact(interactable);
        }
    }

    #endregion Methods: Unity

    public virtual void Interact(Interactable other)
    {
        // Debug.Log($"<color=green>{name}</color>Interacting with <color=red>{other.gameObject.name}</color>");
    }


}
