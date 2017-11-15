using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventTriggerListener:UnityEngine.EventSystems.EventTrigger{
    public delegate void VoidDelegate(GameObject obj);
    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;
    public VoidDelegate onDeselect;
	public VoidDelegate onInitializePotentialDrag;
	public VoidDelegate onBeginDrag;
	public VoidDelegate onDrag;
	public VoidDelegate onScroll;
	public VoidDelegate onMove;
	public VoidDelegate onSubmit;
	public VoidDelegate onCancel;

    static public EventTriggerListener Get(GameObject obj){
        EventTriggerListener listener = obj.GetComponent<EventTriggerListener>();
        if (listener == null) listener = obj.AddComponent<EventTriggerListener>();
        return listener; 
    }
    /*
    * IPointerEnterHandler - OnPointerEnter - Called when a pointer enters the object
    * IPointerExitHandler - OnPointerExit - Called when a pointer exits the object
    * IPointerDownHandler - OnPointerDown - Called when a pointer is pressed on the object
    * IPointerUpHandler - OnPointerUp - 当鼠标在对象所在区域释放时进行调用;(调用的是按下的是调用的对象)
    * IPointerClickHandler - OnPointerClick - Called when a pointer is released (called on the original the pressed object)
    * IInitializePotentialDragHandler - OnInitializePotentialDrag - Called when a drag target is found, can be used to initialise values
    * IBeginDragHandler - OnBeginDrag - Called on the drag object when dragging is about to begin
    * IDragHandler - OnDrag - Called on the drag object when a drag is happening
    * IEndDragHandler - OnEndDrag - Called on the drag object when a drag finishes
    * IDropHandler - OnDrop - Called on the object where a drag finishes
    * IScrollHandler - OnScroll - Called when a mouse wheel scrolls
    * IUpdateSelectedHandler - OnUpdateSelected - Called on the selected object each tick
    * ISelectHandler - OnSelect - Called when the object becomes the selected object
    * IDeselectHandler - OnDeselect - Called on the selected object becomes deselected
    * IMoveHandler - OnMove - Called when a move event occurs (left, right, up, down, ect)
    * ISubmitHandler - OnSubmit - Called when the submit button is pressed
    * ICancelHandler - OnCancel - Called when the cancel button is pressed

    OnPointerEnter -> OnSelect -> OnPointerDown ->OnInitializePotentialDrag ->
                                                                    
    OnUpdateSelected(多次调用) ->OnBeginDrag-> OnDrag(多次调用) - >OnPointerUp - >OnPointerClick - > OnPointerExit(失去焦点就会调用)
    */
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }
    public override void OnPointerDown (PointerEventData eventData){
		if(onDown != null) onDown(gameObject);
	}
	public override void OnPointerEnter (PointerEventData eventData){
		if(onEnter != null) onEnter(gameObject);
	}
	public override void OnPointerExit (PointerEventData eventData){
		if(onExit != null) onExit(gameObject);
	}
	public override void OnPointerUp (PointerEventData eventData){
		if(onUp != null) onUp(gameObject);
	}
	public override void OnDeselect (BaseEventData eventData){
		if(onDeselect != null) onDeselect(gameObject);
	}
    public override void OnSelect (BaseEventData eventData){
		if(onSelect != null) onSelect(gameObject);
	}

	public override void OnUpdateSelected (BaseEventData eventData){
		if(onUpdateSelect != null) onUpdateSelect(gameObject);
	}

    public override void OnInitializePotentialDrag (PointerEventData eventData){
		if(onInitializePotentialDrag != null) onInitializePotentialDrag(gameObject);
	}

    public override void OnBeginDrag (PointerEventData eventData){
		if(onBeginDrag != null) onBeginDrag(gameObject);
	}

    public override void OnDrag (PointerEventData eventData){
		if(onDrag != null) onDrag(gameObject);
	}

    public override void OnScroll (PointerEventData eventData){
		if(onScroll != null) onScroll(gameObject);
	}

    public override void OnMove (AxisEventData  eventData){
		if(onMove != null) onMove(gameObject);
	}

    public override void OnSubmit (BaseEventData  eventData){
		if(onSubmit != null) onSubmit(gameObject);
	}

    public override void OnCancel (BaseEventData  eventData){
		if(onCancel != null) onCancel(gameObject);
	}
}