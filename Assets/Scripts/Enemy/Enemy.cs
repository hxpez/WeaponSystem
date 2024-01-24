using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _damageCooldown = 2;
    private bool _damagable = true;

    private MeshRenderer _renderer;
    private void OnEnable()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void TakeDamage()
    {
        if (_damagable)
        {
            _damagable = false;
            StartCoroutine(DamageCooldown());
        }
    }

    private IEnumerator DamageCooldown()
    {
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(_damageCooldown);
        _renderer.material.color = Color.white;
        _damagable = true;
    }


}
