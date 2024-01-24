using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField, Header("Attributes")] private int _maxAmmo;
    [SerializeField] private float _reloadTime;

    [SerializeField, Header("References")] private Transform _spawnPos;
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
        if (context.performed && !_isReloading)
        {
            if(_currentAmmo <= 0 && _reloadingCoroutine == null)
            {
                Debug.Log("Ammo Empty");
                _isReloading = true;
                _reloadingCoroutine = StartCoroutine(WaitForReload(_reloadTime));
                return;
            }

            Fire();
        }
    }

    public void Fire()
    {
        Debug.Log("Fired Projectile");
        _currentAmmo -= 1;
        Debug.Log("Current Ammo: " + _currentAmmo);

        Instantiate(_projectilePrefab, _spawnPos.position, _spawnPos.rotation);
    }

    public IEnumerator WaitForReload(float t)
    {
        Debug.Log("Started Reload");
        while(t > 0)
        {
            Debug.Log("Is Reloading");
            t -= Time.deltaTime;
            yield return null;
        }

        Debug.Log("Finished Reloading");
        _isReloading = false;
        _currentAmmo = _maxAmmo;
        _reloadingCoroutine = null;
        yield return null;
    }
}
