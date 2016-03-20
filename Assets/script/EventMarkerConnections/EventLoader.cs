using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Threading;
using UnityEngine.UI;

public class EventLoader : MonoBehaviour {

    public GameObject eventMarkerConnectionInstance;
    private GameObject _eventMarkerConnections;
    private Events _eventDataService;
    private bool _hasCreatedEventMarkerConnections;

	void Start ()
	{
	    _hasCreatedEventMarkerConnections = false;
	}

    void Update()
    {
        if (!_hasCreatedEventMarkerConnections && GameObject.Find("DBConnectionManager") != null && GameObject.Find("EventMarkerConnections") != null)
        {
            var dataServiceManager = GameObject.Find("DBConnectionManager").GetComponent("DataServiceManager") as DataServiceManager;
            _eventDataService = dataServiceManager.GetDataService("Event") as Events;

            _eventMarkerConnections = GameObject.Find("EventMarkerConnections");

            CreateEventMarkerConnections();
            _hasCreatedEventMarkerConnections = true;
        }
        else if (!_hasCreatedEventMarkerConnections)
        {
            Thread.Sleep(500);
        }
    }

    private void CreateEventMarkerConnections()
    {
        var events = _eventDataService.List().Cast<Event>();
        var result = events.Select(e => CreateEventMarkerConnection(e) ).ToList();
    }

    private GameObject CreateEventMarkerConnection(Event evnt)
    {
        var newInstance = Instantiate(eventMarkerConnectionInstance, transform.position, Quaternion.identity) as GameObject;

        newInstance.GetComponentInChildren<Text>().text = evnt.Text;
        newInstance.transform.SetParent(_eventMarkerConnections.transform);
        newInstance.AddComponent<DropZone>();

        return newInstance;
    }
}
