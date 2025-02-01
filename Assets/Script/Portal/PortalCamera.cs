using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position+ playerOffsetFromPortal;
       // transform.position = new Vector3(Mathf.Clamp(transform.localPosition.x, 0 , 8), transform.position.y, transform.position.z);

        float angularDiffernceBetweenPortalRotation = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        
        Quaternion portalRotationalDiffernce =Quaternion.AngleAxis(angularDiffernceBetweenPortalRotation, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDiffernce* playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

    }
}
