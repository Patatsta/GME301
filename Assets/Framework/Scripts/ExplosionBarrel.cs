using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBarrel : MonoBehaviour
{
    [SerializeField] private GameObject _explosionBarrel;

    [SerializeField] private Renderer _renderer;
    [SerializeField] private Collider _collider;
    [SerializeField] private Collider _shotColldier;
    private void Start()
    {
       _collider.enabled = false;
        _shotColldier.enabled = true;
        _explosionBarrel.SetActive(false);
    }

    public void Explode()
    {
        _shotColldier.enabled=false;
        _renderer.enabled = false;
        _explosionBarrel.SetActive(true);
        _collider.enabled=true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            MonsterBehaviour monsterBehaviour = other.GetComponent<MonsterBehaviour>();
            if (monsterBehaviour != null)
            {
                monsterBehaviour.Shot();
                Invoke("ExplosionEnd", 0.7f);
            }
        }
    }

    private void ExplosionEnd()
    {
        Destroy(gameObject);
    }
}
