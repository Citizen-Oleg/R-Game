using DefaultNamespace;
using UnityEngine;

public class Pin : MonoBehaviour, IMergeObject
{
    public Transform InitialMergeTransform => _initialMergeTransform;
    public Vector3 MergePosition => _mergePosition;

    [SerializeField]
    private Transform _initialMergeTransform;
    [SerializeField]
    private Vector3 _mergePosition;
}