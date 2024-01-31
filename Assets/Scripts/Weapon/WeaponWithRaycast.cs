using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponWithRaycast : MonoBehaviour
{
    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _dmg;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _range;

    private int _currentAmmo;
    private bool _isReloading;

    private Coroutine _reloadingCoroutine;

    private void OnEnable()
    {
        _currentAmmo = _maxAmmo;
    }

    public void OnFIRE(InputAction.CallbackContext context)
    {
        if (context.performed && !_isReloading)
        {
            if (_currentAmmo <= 0 && _reloadingCoroutine == null)
            {
                Debug.Log("Ammo empty");
                _reloadingCoroutine = StartCoroutine(WaitForReload(_reloadTime));
            }

            if (_currentAmmo != 0) Fire();
        }
    }

    public void Fire()
    {
        Debug.Log("Fired Projectile");
        _currentAmmo--;
        Debug.Log("current ammo: " + _currentAmmo);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, _range))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage();
            }
        }
    }

    public IEnumerator WaitForReload(float t)
    {
        _isReloading = true;
        yield return new WaitForSeconds(t);
        _isReloading = false;
        _currentAmmo = _maxAmmo;
        _reloadingCoroutine = null;
    }
}
