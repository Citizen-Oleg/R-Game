using Events;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LoseZone : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cup cup))
        {
            _collider.enabled = false;
            EventStreams.UserInterface.Publish(new EndGameEvent());
        }
    }
}