using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position+ playerOffsetFromPortal;

        float angularDiffernceBetweenPortalRotation = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        
        Quaternion portalRotationalDiffernce =Quaternion.AngleAxis(angularDiffernceBetweenPortalRotation, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDiffernce* playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

    }
}
