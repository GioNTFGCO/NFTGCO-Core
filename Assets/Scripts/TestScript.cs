using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    public Vector2 position;
    public Quaternion rotation;

    private GameObject _objectCreated;
    [Space]
    [SerializeField] private NFTGCO.Helpers.InspectorButton childInstantiateButton = new NFTGCO.Helpers.InspectorButton("childInstantiate");
    [Space]
    [SerializeField] private NFTGCO.Helpers.InspectorButton childInstantiateAsChildButton = new NFTGCO.Helpers.InspectorButton("childInstantiateAsChild");
    [Space]
    [SerializeField] private NFTGCO.Helpers.InspectorButton childInstantiateAsChildWorldSpaceButton = new NFTGCO.Helpers.InspectorButton("childInstantiateAsChildWorldSpace");
    [Space]
    [SerializeField] private NFTGCO.Helpers.InspectorButton childInstantiateWithPositionRotationButton = new NFTGCO.Helpers.InspectorButton("childInstantiateWithPositionRotation");
    [Space]
    [SerializeField] private NFTGCO.Helpers.InspectorButton childInstantiateAsChildWithPositionRotationButton = new NFTGCO.Helpers.InspectorButton("childInstantiateAsChildWithPositionRotation");

    public Vector3 localPos;
    public Quaternion localRot;
    public Vector3 localScale;
    [Space]
    public Vector3 worldPos;
    public Quaternion worldRot;
    public Vector3 worldScale;
    [Space]
    public Vector3 gp;
    public Quaternion gr;
    public Vector3 gs;
    [Space]
    public Vector3 localPosition;
    public Quaternion localRotation;

    public void childInstantiate()
    {
        if (_objectCreated != null)
        {
            Destroy(_objectCreated);
        }

        _objectCreated = Instantiate(prefab);
        _objectCreated.name = "Instantiate";

        localPos = _objectCreated.transform.localPosition;
        localRot = _objectCreated.transform.localRotation;
        localScale = _objectCreated.transform.localScale;

        worldPos = _objectCreated.transform.position;
        worldRot = _objectCreated.transform.rotation;
        worldScale = _objectCreated.transform.lossyScale;

        gp = parent.TransformPoint(localPos);
        gr = parent.rotation * localRot;
        gs = Vector3.Scale(parent.lossyScale, localScale);

        localPosition = parent.InverseTransformPoint(worldPos);
        localRotation = Quaternion.Inverse(parent.rotation) * worldRot;

        _objectCreated.transform.position = gp;
        _objectCreated.transform.rotation = gr;
        //_objectCreated.transform.localScale = gs;

        _objectCreated.transform.SetParent(parent, true);

        //_objectCreated.transform.localPosition = localPosition;
        //_objectCreated.transform.localRotation = localRotation;

    }

    public void childInstantiateAsChild()
    {
        if (_objectCreated != null)
        {
            Destroy(_objectCreated);
        }
        _objectCreated = Instantiate(prefab, parent);
        _objectCreated.name = "InstantiateChild";
    }

    public void childInstantiateAsChildWorldSpace()
    {
        if (_objectCreated != null)
        {
            Destroy(_objectCreated);
        }
        _objectCreated = Instantiate(prefab, parent, true);
        _objectCreated.name = "InstantiateWorldSpace";

        Vector3 worldPos = _objectCreated.transform.position;
        Quaternion worldRot = _objectCreated.transform.rotation;
        Vector3 worldScale = _objectCreated.transform.lossyScale;
    }

    public void childInstantiateWithPositionRotation()
    {
        if (_objectCreated != null)
        {
            Destroy(_objectCreated);
        }
        _objectCreated = Instantiate(prefab, position, rotation);
        _objectCreated.name = "InstantiatePosAndRot";
    }
    public void childInstantiateAsChildWithPositionRotation()
    {
        if (_objectCreated != null)
        {
            Destroy(_objectCreated);
        }
        _objectCreated = Instantiate(prefab, position, rotation, parent);
        _objectCreated.name = "InstantiateChildPosAndRot";
    }

}