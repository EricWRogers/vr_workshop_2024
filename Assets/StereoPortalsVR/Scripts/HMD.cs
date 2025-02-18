using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StereoPortalsVR
{
    public class HMD : MonoBehaviour
    {
        [SerializeField] Camera _cam;
        [SerializeField] Transform _leftEye;
        [SerializeField] Transform _rightEye;

        public Camera Cam { get { return _cam; } }
        public Transform[] Eyes { get { return new Transform[] { _leftEye, _rightEye }; } }
    }
}