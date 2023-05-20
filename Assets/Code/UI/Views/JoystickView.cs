using System.Collections;
using System.Collections.Generic;
using Code.UI.Presenter;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IJoystickView
{
    private Vector3 initialClickPosition;
    private ISubject<Vector3> _moved;

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialClickPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPosition = eventData.position;
        Vector3 deltaPosition = currentPosition - initialClickPosition;
        deltaPosition.z = deltaPosition.y; 
        deltaPosition.y = 0;
        deltaPosition = deltaPosition.normalized *-1;
        _moved.OnNext(deltaPosition*Time.deltaTime);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _moved.OnNext(Vector3.zero);
    }

    public void Initialize(ISubject<Vector3> moved)
    {
        _moved = moved;
    }
}