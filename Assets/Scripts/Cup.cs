using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(AnimationController))]
public class Cup : MonoBehaviour, IMergeObject
{
    public Transform InitialMergeTransform => _initialMergeTransform;
    public Vector3 MergePosition => _mergePosition;
    public MovementController MovementController => _movementController;

    [SerializeField]
    private Vector3 _mergePosition;
    [SerializeField]
    private Transform _initialMergeTransform;
    [SerializeField]
    private MovementController _movementController;
}
