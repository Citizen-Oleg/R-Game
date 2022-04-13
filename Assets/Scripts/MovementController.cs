using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    public bool IsJump => _isJump;

    [SerializeField]
    private AnimationCurve _jumpCurve;
    [SerializeField]
    private float _jumpTime;
    [SerializeField]
    private float _jumpHeight;
    [Space(25f)]
    [SerializeField]
    private Transform _transformCheckGround;
    [SerializeField]
    private float _testDistance;
    [SerializeField]
    private LayerMask _layerMaskGround;

    private bool _isJump;
    private Rigidbody _rigidbody;
    private AnimationController _animationController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animationController = GetComponent<AnimationController>();
    }

    public void Jump(Vector3 endPosition, Action callBack)
    {
        StartCoroutine(JumpToPosition(endPosition, callBack));
    }
    
    private IEnumerator JumpToPosition(Vector3 endPosition, Action callBack)
    {
        _isJump = true;
        _rigidbody.isKinematic = false;
        
        var currentTime = 0f;
        var startPosition = transform.position;
        _animationController.Flip(_jumpTime);

        while (currentTime < _jumpTime)
        {
            var time = currentTime / _jumpTime;
            var positionYcurve = _jumpCurve.Evaluate(time) * Vector3.up * _jumpHeight;
            transform.position = Vector3.Lerp(startPosition, endPosition, time) + positionYcurve;
            currentTime += Time.deltaTime;
            
            yield return null;
        }   

        transform.position = endPosition;
        
        _isJump = !IsGround();
        _rigidbody.isKinematic = IsGround();
        callBack?.Invoke();
    }

    private bool IsGround()
    {
        return Physics.Raycast(_transformCheckGround.position, Vector3.down, _testDistance, _layerMaskGround);
    }
}
