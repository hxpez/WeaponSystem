using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _damageCooldown = 2;
    private bool _damagable = true;

    private MeshRenderer _renderer;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    [SerializeField] private AudioSource _audioSource;
    private void OnEnable()
    {
        _renderer = GetComponent<MeshRenderer>();
        _audioSource = GetComponent<AudioSource>();

        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void TakeDamage()
    {
        if (_damagable)
        {
            _damagable = false;
            _audioSource.Play();
            StartCoroutine(DamageCooldown());
        }
    }

    private IEnumerator DamageCooldown()
    {
        //_renderer.material.color = Color.red;
        _skinnedMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(_damageCooldown);
        _skinnedMeshRenderer.material.color = Color.white;
        //_renderer.material.color = Color.white;
        _damagable = true;
    }


}
