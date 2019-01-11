using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

public class ARTapToPlace : MonoBehaviour{

    public GameObject focus;

    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid;

    // Start is called before the first frame update
    void Start(){
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    // Update is called once per frame
    void Update() {
        UpdatePlacementPose();
        UpdatePlacementPoseIndicator();
    }

    private void UpdatePlacementPose(){
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter,hits,TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    private void UpdatePlacementPoseIndicator(){
        if (placementPoseIsValid)
        {
            focus.SetActive(true);
            focus.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            focus.SetActive(false);
        }
    }
}
