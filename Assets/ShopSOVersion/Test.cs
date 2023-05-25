using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassA : MonoBehaviour
{
    [SerializeField]
    private int intValue;

    [SerializeField]
    private float floatValue;
}

public class ClassB : MonoBehaviour
{
    [SerializeField]
    private string stringValue;

    [SerializeField]
    private bool boolValue;
}
