using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed, _lifeSpan;
    [SerializeField] private int _dmg;

    private Rigidbody _rb;
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
        }
    }

    public void Fire()
    {
        _rb.AddForce(_direction * _speed, ForceMode.Impulse);
        StartCoroutine(WaitBeforeDestroy());
    }

    private IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(_lifeSpan);
        Destroy(gameObject);
    }
}
