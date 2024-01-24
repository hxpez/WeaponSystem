using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField, Header("Attributes")] private float _speed, _lifeSpan;
    [SerializeField] private int _damage;

    [SerializeField, Header("References")] private Rigidbody _rb;

    private Vector3 _direction;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _direction = transform.forward;

        Fire();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hitted enemy");
            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();
            meshRenderer.material.color = Color.red;
        }
    }

    public void Fire()
    {
        _rb.AddForce(_direction * _speed, ForceMode.Impulse);
        StartCoroutine(WaitBeforeDestroy(_lifeSpan));
    }

    private IEnumerator WaitBeforeDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


}
