using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Cup _startCup;
    [SerializeField]
    private ManagerCup _managerCup;
    [SerializeField]
    private Vector3 _offSet;

    private Cup _cup;
    
    private void Awake()
    {
        _cup = _startCup;
        _managerCup.OnChangeLastCup += ChangeTarget;
    }

    private void ChangeTarget(Cup cup)
    {
        _cup = cup;
    }

    private void LateUpdate()
    {
        var positionCup = _cup.transform.position;
        transform.position = new Vector3(positionCup.x ,0, positionCup.z) + _offSet;
    }

    private void OnDestroy()
    {
        _managerCup.OnChangeLastCup -= ChangeTarget;
    }
}