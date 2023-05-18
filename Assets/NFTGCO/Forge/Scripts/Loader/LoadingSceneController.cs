using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 90f;
    [SerializeField] private Transform _iconTransform;
    void Update()
    {
        _iconTransform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
    }
}