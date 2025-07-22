using UnityEngine;
using System.Collections;

public class GunShooter : MonoBehaviour
{
    public Transform firePoint;
    public ParticleSystem shootParticle;
    public GameObject tracerPrefab;
    public float fireDistance = 100f;
    public float tracerSpeed = 100f;
    public LayerMask hitMask;
    public int damage = 10;

    private AudioSource audioSource;
    public AudioClip shootSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (shootParticle != null)
            shootParticle.Play();

        Vector3 direction = firePoint.forward;
        Vector3 targetPoint = firePoint.position + direction * fireDistance;

        bool hitSomething = false;

        if (Physics.Raycast(firePoint.position, direction, out RaycastHit hit, fireDistance, hitMask))
        {
            targetPoint = hit.point;
            hitSomething = true;

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }

        if (tracerPrefab != null)
        {
            GameObject tracer = Instantiate(tracerPrefab, firePoint.position, Quaternion.LookRotation(direction));
            StartCoroutine(MoveTracer(tracer.transform, targetPoint));
        }
        audioSource.PlayOneShot(shootSound);
    }

    IEnumerator MoveTracer(Transform tracer, Vector3 target)
    {
        while (Vector3.Distance(tracer.position, target) > 0.05f)
        {
            tracer.position = Vector3.MoveTowards(tracer.position, target, tracerSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(tracer.gameObject);
    }

    
}
