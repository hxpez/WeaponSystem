using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponWithRaycast : MonoBehaviour
{
    [SerializeField, Header("Settings")] private int _maxAmmo;
    [SerializeField] private int _dmg;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _range;

    [SerializeField, Header("References")] private TextMeshProUGUI _ammoText;
    [SerializeField] private TextMeshProUGUI _reloadingText;
    [SerializeField] private Transform _barrel;
    [SerializeField] private Animator _anim;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _audioSource;

    private int _currentAmmo;
    private bool _isReloading;

    private Coroutine _reloadingCoroutine;

    private Action _onAmmoChanged;

    private void OnEnable()
    {
        _currentAmmo = _maxAmmo;
        _anim = GetComponentInChildren<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _onAmmoChanged += UpdateUI;
        _onAmmoChanged.Invoke();
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
        _anim.SetTrigger("Fire");
        _particleSystem.Play();
        _audioSource.Play();
        _currentAmmo--;
        _onAmmoChanged?.Invoke();
        Debug.Log("current ammo: " + _currentAmmo);

        RaycastHit hit;
        if(Physics.Raycast(_barrel.position, transform.forward, out hit, _range))
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
        _reloadingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(t);
        _reloadingText.gameObject.SetActive(false);
        _isReloading = false;
        _currentAmmo = _maxAmmo;
        _onAmmoChanged?.Invoke();
        _reloadingCoroutine = null;
    }

    private void UpdateUI()
    {
        _ammoText.text = _currentAmmo.ToString() + "/" + _maxAmmo.ToString() + " Ammo";
    }
}
