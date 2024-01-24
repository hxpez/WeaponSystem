using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Camera _playerCam;
    [SerializeField] private Transform _desiredPos;

    void Update()
    {
        _playerCam.transform.position = _desiredPos.position;
    }
}
