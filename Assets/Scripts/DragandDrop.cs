using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragandDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler {
	private RectTransform rectTransform;
	public GameObject image;
	public void Start () {
		image.SetActive(false);
	}
	
	public void OnPointerEnter(PointerEventData eventData){
		image.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData){
		image.SetActive(false);
	}
	private void Awake(){
		rectTransform = GetComponent<RectTransform>();
	}
	public void OnPointerDown(PointerEventData eventData){
		Debug.Log("OnPointerDown");
		image.SetActive(true);
	}
	public void OnDrag(PointerEventData eventData){
		Debug.Log("OnDrag");
		image.SetActive(true);
		rectTransform.anchoredPosition += eventData.delta;
	}
	public void OnBeginDrag(PointerEventData eventData){
		Debug.Log("BeginDrag");
		image.SetActive(true);
	}

	public void OnEndDrag(PointerEventData eventData){
		Debug.Log("EndDrag");
	}

}
