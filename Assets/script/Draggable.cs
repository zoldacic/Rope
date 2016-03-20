﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;
    public Transform parentWhenDragging = null;

    GameObject placeholder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var allMarkers = GameObject.Find("AllMarkers");
        var copy = CopyMarker(this.transform.gameObject);
        copy.transform.parent = allMarkers.transform;
        
        //Debug.Log("OnBeginDrag");

        //placeholder = new GameObject();
        //placeholder.transform.SetParent(this.transform.parent);
        //LayoutElement le = placeholder.AddComponent<LayoutElement>();
        //le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        //le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        //le.flexibleWidth = 0;
        //le.flexibleHeight = 0;

        //placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(parentWhenDragging);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log ("OnDrag");

        this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        this.transform.position = eventData.position;

        //if (placeholder.transform.parent != placeholderParent)
        //    placeholder.transform.SetParent(placeholderParent);

        //int newSiblingIndex = placeholderParent.childCount;

        //for (int i = 0; i < placeholderParent.childCount; i++)
        //{
        //    if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
        //    {

        //        newSiblingIndex = i;

        //        if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
        //            newSiblingIndex--;

        //        break;
        //    }
        //}

        //placeholder.transform.SetSiblingIndex(newSiblingIndex);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        this.transform.SetParent(parentToReturnTo);
        //this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        //Destroy(placeholder);
    }

    private GameObject CopyMarker(GameObject sourceMarker)
    {
        return Instantiate(sourceMarker, transform.position, Quaternion.identity) as GameObject;
    }

}
