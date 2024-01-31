using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _reloadTime;

    [SerializeField] private Transform _spawnPos;
    [SerializeField] private GameObject _projectilePrefab;

    private int _currentAmmo;
    private bool _isReloading;

    private Coroutine _reloadingCoroutine;

    private void OnEnable()
    {
        _currentAmmo = _maxAmmo;
    }

    public void OnFIRE(InputAction.CallbackContext context)
    {
        if(context.performed && !_isReloading)
        {
            if(_currentAmmo <= 0 && _reloadingCoroutine == null)
            {
                Debug.Log("Ammo empty");
                _reloadingCoroutine = StartCoroutine(WaitForReload(_reloadTime));
            }

            if(_currentAmmo != 0) Fire();
        }
    }

    public void Fire()
    {
        Debug.Log("Fired Projectile");
        _currentAmmo--;
        Debug.Log("current ammo: " + _currentAmmo);

        Instantiate(_projectilePrefab, _spawnPos.position, _spawnPos.rotation);
    }

    public IEnumerator WaitForReload(float t)
    { 
        _isReloading = true;
        yield return new WaitForSeconds(t);
        _isReloading = false;
        _currentAmmo = _maxAmmo;
        _reloadingCoroutine = null;

        //Debug.Log("Started Reload");
        //while(t > 0)
        //{
        //    Debug.Log("Is Reloading");
        //    t -= Time.deltaTime;

        //}
    }
}
