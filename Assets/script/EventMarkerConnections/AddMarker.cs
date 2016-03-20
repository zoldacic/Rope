using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddMarker : MonoBehaviour {

    public GameObject markerInstance;

    public void Add()
    {
        var text = (GameObject.Find("AddMarkerInputField").GetComponent("InputField") as InputField).text;
        CreateMarker(text);
    }

    private void CreateMarker(string text)
    {
        var newInstance = Instantiate(markerInstance, transform.position, Quaternion.identity) as GameObject;

        newInstance.GetComponentsInChildren<Text>()[1].text = text;

        var allMarkers = GameObject.Find("AllMarkers");
        newInstance.transform.SetParent(allMarkers.transform);

        var draggable = newInstance.GetComponent<Draggable>();
        draggable.parentToReturnTo = allMarkers.transform;
        draggable.placeholderParent = allMarkers.transform;
        draggable.parentWhenDragging = GameObject.Find("MainCanvas").transform;
    }
}
