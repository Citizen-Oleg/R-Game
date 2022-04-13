using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Events;
using UnityEngine;

public class ManagerCup : MonoBehaviour
{
    private const int RAYCAST_RELEASE_HEIGHT = 50;

    public event Action<Cup> OnChangeLastCup;
    
    [SerializeField]
    private float _jumpLength;
    [SerializeField]
    private float _delayBeforeJump;
    [SerializeField]
    private List<Cup> _startCups = new List<Cup>();
    
    private IDisposable _subscription;
    
    private Cup _newCup;
    private Cup _removeCup;

    private void Awake()
    {
        _subscription = EventStreams.UserInterface.Subscribe<TapEvent>(Jump);
    }

    private void Jump(TapEvent tapEvent)
    {
        if (_startCups.Any(cup => cup.MovementController.IsJump))
        {
            return;
        }

        CheckGameObject();
        StartCoroutine(StartJumpingCup());
        
        OnChangeLastCup?.Invoke(_startCups[_startCups.Count - 1]);
    }

    private IEnumerator StartJumpingCup()
    {
        var endPoint = GetJumpEndPoint();
        
        for (var i = 0; i < _startCups.Count; i++)
        {
            if (i == 0)
            {
                _startCups[i].MovementController.Jump(endPoint, CheckNumberCups);
            }
            else
            {
                _startCups[i].MovementController.Jump(endPoint + GetMergePosition(i), CheckNumberCups);
            }

            yield return new WaitForSeconds(_delayBeforeJump);
        }
        
        _startCups.Reverse();
        
        RemoveCup();
        AddNewCup();
    }

    private void CheckNumberCups()
    {
        if (_startCups.Count == 0)
        {
            EventStreams.UserInterface.Publish(new EndGameEvent());
        }
    }

    private Vector3 GetMergePosition(int index)
    {
        var mergePosition = Vector3.zero;
        for (int i = index; i > 0;)
        {
            mergePosition += _startCups[--i].MergePosition;
        }

        return mergePosition;
    }
    
    private Vector3 GetJumpEndPoint()
    {
        if (Physics.Raycast(GetReleasePosition(), Vector3.down, out var hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IMergeObject mergeObject))
            {
                return mergeObject.InitialMergeTransform.position;
            }
            
            return hitInfo.point;
        }
        
        return GetGlassPosition() + new Vector3(0, transform.position.y, _jumpLength);
    }

    private void CheckGameObject()
    {
        if (Physics.Raycast(GetReleasePosition(), Vector3.down, out var hitInfo))
        {
            CheckForCup(hitInfo);
            CheckForPin(hitInfo);
        }
    }

    private void CheckForCup(RaycastHit hitInfo)
    {
        if (hitInfo.collider.TryGetComponent(out Cup cup))
        {
            _newCup = cup;
        }
    }

    private void AddNewCup()
    {
        if (_newCup != null)
        {
            _startCups.Add(_newCup);
            _newCup = null;
        }
    }

    private void RemoveCup()
    {
        if (_removeCup == null)
        {
            return;
        }
        
        _startCups.Remove(_removeCup);
        _removeCup = null;
    }

    private void CheckForPin(RaycastHit hitInfo)
    {
        if (hitInfo.collider.TryGetComponent(out Pin pin))
        {
            _removeCup = _startCups[0];
        }
    }
    
    private Vector3 GetReleasePosition()
    {
        return GetGlassPosition() + new Vector3(0, RAYCAST_RELEASE_HEIGHT, _jumpLength);
    }

    private Vector3 GetGlassPosition()
    {
        return _startCups[_startCups.Count - 1].transform.position;
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}