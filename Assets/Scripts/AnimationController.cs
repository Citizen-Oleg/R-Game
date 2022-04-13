using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Transform _cup;
    [SerializeField]
    private AnimationCurve _rotationCurve;

    public void Flip(float time)
    {
        StartCoroutine(RotationCup(time));
    }

    private IEnumerator RotationCup(float time)
    {
        var currentTime = 0f;
        
        while (currentTime < time)
        {
            _cup.rotation = Quaternion.AngleAxis(_rotationCurve.Evaluate(currentTime / time), Vector3.right);
            currentTime += Time.deltaTime;
            
            yield return null;
        }
        
        _cup.rotation = Quaternion.AngleAxis(_rotationCurve[_rotationCurve.keys.Length - 1].value, Vector3.right);
    }
}