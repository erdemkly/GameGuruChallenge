using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class State : IPointerDownHandler,IPointerUpHandler,IDragHandler,IEndDragHandler
{
    public abstract void OnPointerDown(PointerEventData eventData);
    public abstract void OnPointerUp(PointerEventData eventData);
    public abstract void OnDrag(PointerEventData eventData);
    public abstract void OnEndDrag(PointerEventData eventData);
    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
