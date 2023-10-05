using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceMultipleObjectsOnPlane : PressInputBase
{
    [SerializeField]
    [Tooltip("The prefab to instantiate on detected planes.")]
    GameObject placedPrefab;

    GameObject spawnedObject;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Variables to limit the number of objects
    int maxObjectsToPlace = 10;
    int placedObjectCount = 0;
    bool planeDetectionEnabled = true; // Flag to control plane detection

    private TimerUIUpdater timerUIUpdater;

    protected override void Awake()
    {
        base.Awake();
        aRRaycastManager = GetComponent<ARRaycastManager>();
        timerUIUpdater = FindObjectOfType<TimerUIUpdater>();
    }

    protected override void OnPress(Vector3 position)
    {
        if (placedObjectCount >= maxObjectsToPlace)
        {
            // Disable AR plane detection when the object placement limit is reached
            if (planeDetectionEnabled)
            {
                TogglePlaneDetection(false);
                planeDetectionEnabled = false;

                // Start the timer when all objects are placed
                timerUIUpdater.StartTimer();
            }
            return; // Stop placing objects if the limit is reached
        }

        if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            // Place the object on the detected plane
            var hitPose = hits[0].pose;
            spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);

            // Orient the object to face the camera
            Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            lookPos.y = 0;
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);

            placedObjectCount++;
        }
    }

    // Helper method to toggle plane detection on or off
    private void TogglePlaneDetection(bool enable)
    {
        ARPlaneManager planeManager = FindObjectOfType<ARPlaneManager>();
        if (planeManager != null)
        {
            planeManager.enabled = enable;
        }
    }
}
