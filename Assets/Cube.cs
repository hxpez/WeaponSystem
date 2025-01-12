using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private MeshRenderer _mesh;

    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _mesh.material.color = Color.white;
    }

    public void ChangeColor()
    {
        if (_mesh.material.color == Color.white)
        {
            _mesh.material.color = Color.red;
        }
        else _mesh.material.color = Color.white;
    }
}
