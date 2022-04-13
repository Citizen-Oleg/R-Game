using Events;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapZone : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        EventStreams.UserInterface.Publish(new TapEvent());
    }
}