using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponWithRaycast : MonoBehaviour
{
    [SerializeField, Header("Attributes")] private int _maxAmmo;
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

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, _range))
        {
            Debug.Log(hit.collider.name);
            //GameObject gameObject = hit.collider.gameObject;
            //gameObject.GetComponent<MeshRenderer>().material.color = Color.red;

            if(hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage();
            }

            //if(hit.collider.TryGetComponent(out Enemy enemy))
            //{
            //    enemy.TakeDamage(_dmg);
            //}
        }
    }

    public IEnumerator WaitForReload(float t)
    {
        Debug.Log("Started Reload");
        while (t > 0)
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
