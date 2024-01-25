using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _damageCooldown = 4;
    private bool _isDamagable;

    private MeshRenderer _renderer;

    private void OnEnable()
    {
        _isDamagable = true;
        _renderer = GetComponent<MeshRenderer>();
    }

    public void TakeDamage()
    {
        if (_isDamagable)
        {
            StartCoroutine(DamageCooldown());
        }
    }

    public IEnumerator DamageCooldown()
    {
        _isDamagable = false;
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(_damageCooldown);
        _renderer.material.color = Color.white;
        _isDamagable = true;
    }
}
