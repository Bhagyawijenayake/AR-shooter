using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[HelpURL("https://youtu.be/HkNVp04GOEI")]
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceMultipleObjectsOnPlane : PressInputBase
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject placedPrefab;

    GameObject spawnedObject;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Variables to limit the number of objects
    int maxObjectsToPlace = 10;
    int placedObjectCount = 0;

    protected override void Awake()
    {
        base.Awake();
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    protected override void OnPress(Vector3 position)
    {
        if (placedObjectCount >= maxObjectsToPlace)
            return; // Stop placing objects if the limit is reached

        if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);

            Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            lookPos.y = 0;
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);

            placedObjectCount++;
        }
    }
}
