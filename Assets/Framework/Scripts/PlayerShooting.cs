using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private AudioSource _audioSource1;
    [SerializeField] private AudioSource _audioSource2;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioClip _shotClip;
    [SerializeField] private AudioClip _reloadClip;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _lineDuration = 0.2f;
    [SerializeField] private Transform _shotPoint;
    public bool isGameOver = false;

    [SerializeField] private float _shootCD = 0.5f;
    private float _shotime = -1;

    void Update()
    {
        if (isGameOver) return;

        if (Input.GetMouseButtonDown(0) && Time.time >= _shotime + _shootCD)
        {
            StartCoroutine(PlayReload());
            _shotime = Time.time;

            _audioSource1.PlayOneShot(_shotClip);
            _particleSystem.Play();

            Vector3 start = Camera.main.transform.position;
            Vector3 end = start + Camera.main.transform.forward * 100f;

            if (Physics.Raycast(start, Camera.main.transform.forward, out RaycastHit hit, float.PositiveInfinity, _targetMask))
            {
                end = hit.point;
                Debug.DrawLine(start, end, Color.red, 1f);

                if (hit.collider.CompareTag("Enemy"))
                {
                    MonsterBehaviour monsterBehaviour = hit.collider.GetComponent<MonsterBehaviour>();
                    if (monsterBehaviour != null)
                    {
                        monsterBehaviour.Shot();
                    }
                }
                else if (hit.collider.CompareTag("Wall"))
                {
                    _audioSource2.Play();
                }
                else if (hit.collider.CompareTag("Barrel"))
                {
                    hit.collider.GetComponent<ExplosionBarrel>().Explode();
                }
            }

            StartCoroutine(ShowShotLine(end));
        }
    }

    IEnumerator PlayReload()
    {
        yield return new WaitForSeconds(0.2f);
        _audioSource1.PlayOneShot(_reloadClip);
    }

    IEnumerator ShowShotLine(Vector3 end)
    {
        _lineRenderer.SetPosition(0, _shotPoint.position);
        _lineRenderer.SetPosition(1, end);
        _lineRenderer.enabled = true;

        Material mat = _lineRenderer.material;
        Color originalColor = mat.color;
        float duration = 0.2f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration); 
            mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); 
        _lineRenderer.enabled = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * 100f);
    }
}

