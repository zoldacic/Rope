using UnityEngine;
using System.Collections;

public class EventMarkerConnectionInstatiator : MonoBehaviour
{
    public GameObject eventMarkerConnectionInstance;

    public GameObject GetNewInstance()
    {
        var instance = Instantiate(eventMarkerConnectionInstance, transform.position, Quaternion.identity) as GameObject;
        return instance;
    }
}
