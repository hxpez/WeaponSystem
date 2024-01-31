using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _damageCooldown = 2;
    private float _speed = 2;
    private float _minDistance = 3;
    private bool _damagable = true;

    private MeshRenderer _renderer;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    [SerializeField] private Transform _target;

    [SerializeField] private AudioSource _audioSource;
    private void OnEnable()
    {
        _renderer = GetComponent<MeshRenderer>();
        _audioSource = GetComponent<AudioSource>();

        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if(_target == null)
        {
            _target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _target.position);
        transform.LookAt(_target);

        if(distance > _minDistance)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }

    public void TakeDamage()
    {
        if (_damagable)
        {
            // health -= damage

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
