using Events;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private const int SWIPE_MULTIPLIER = 3;
    
    private float _startYposition;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _startYposition = eventData.position.normalized.y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var power = eventData.position.normalized.y - _startYposition;
        if (power > 0)
        {
            EventStreams.UserInterface.Publish(new TapEvent(Mathf.Clamp(power * SWIPE_MULTIPLIER, 0.1f, 1f)));
        }
    }
} 