using System.Collections;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private float explodeDelay;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;

    private float activeTime;

    private void Start()
    {
        StartCoroutine(ChargeAndExplode());
    }

    private IEnumerator ChargeAndExplode()
    {
        yield return new WaitForSeconds(explodeDelay);
        Explode();
    }

    private void Explode()
    {
        foreach (var col in Physics.OverlapSphere(transform.position, explosionRadius))
        {
            if (col.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1.0f, ForceMode.Impulse);
            }
        }

        AudioManager.instance.PlayExplosionAudio(transform.position);
        VFXManager.instance.PlayExplosionFX(transform.position);
        Destroy(gameObject);
    }
}