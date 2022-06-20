using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GhostableBase : MonoBehaviour
{
    public virtual void InitGhost(Material ghostMaterial)
    {
        GetComponent<MeshRenderer>().sharedMaterial = ghostMaterial;
        if (TryGetComponent(out Collider col))
        {
            col.enabled = false;
        }

        if (TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
    }
}