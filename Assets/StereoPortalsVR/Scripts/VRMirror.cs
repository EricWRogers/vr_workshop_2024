using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace StereoPortalsVR
{
    public class VRMirror : MonoBehaviour
    {
        [SerializeField] HMD _playerHMD;
        [SerializeField] Vector2Int _renderTextureSize;

        readonly Camera[] _cameras = new Camera[2];

        Transform _portalRenderer;
        Renderer _rendComp;

        [SerializeField] Transform test;

        void Awake()
        {
            _cameras[0] = transform.Find("LeftCamera").GetComponent<Camera>();
            _cameras[1] = transform.Find("RightCamera").GetComponent<Camera>();

            _portalRenderer = transform.Find("PortalRenderer");
            _rendComp = _portalRenderer.GetComponent<MeshRenderer>();
        }

        void Start()
        {
            InitializeCameras();
        }

        void InitializeCameras()
        {
            RenderTexture leftEyeTexture = new(_renderTextureSize.x, _renderTextureSize.y, 0);
            RenderTexture rightEyeTexture = new(_renderTextureSize.x, _renderTextureSize.y, 0);
            _cameras[0].enabled = false;
            _cameras[1].enabled = false;
            _cameras[0].targetTexture = leftEyeTexture;
            _cameras[1].targetTexture = rightEyeTexture;
            _cameras[0].fieldOfView = _playerHMD.Cam.fieldOfView;
            _cameras[1].fieldOfView = _playerHMD.Cam.fieldOfView;
            _rendComp.material.SetTexture("_LeftEyeTex", leftEyeTexture);
            _rendComp.material.SetTexture("_RightEyeTex", rightEyeTexture);
        }

        void Update()
        {
            for (int i = 0; i < 2; i++)
            {
                Transform playerTransform = _playerHMD.Eyes[i].transform;
                Matrix4x4 matrix = new();
                Vector3 relPos = playerTransform.position - transform.position;
                Vector3 pos = playerTransform.position + 2 * Vector3.Dot(-transform.forward, relPos) * transform.forward;
                Vector3 reflectedForward = Vector3.Reflect(playerTransform.forward, transform.forward);
                Vector3 reflectedUp = Vector3.Reflect(playerTransform.up, transform.forward); // Reflect the up vector as well
                Quaternion rot = Quaternion.LookRotation(reflectedForward, reflectedUp);
                matrix.SetTRS(pos, rot, Vector3.one);
                RenderCamera(matrix, i);
            }
        }

        public bool IsInView()
        {
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(_playerHMD.Cam);
            return GeometryUtility.TestPlanesAABB(frustumPlanes, _rendComp.bounds);
        }

        void RenderCamera(Matrix4x4 matrix, int camIndex)
        {
            if (IsInView())
            {
                _rendComp.material.SetInt("_RightOverwrite", camIndex);
                _portalRenderer.gameObject.SetActive(false);

                _cameras[camIndex].transform.SetPositionAndRotation(matrix.GetColumn(3), matrix.rotation);
                _cameras[camIndex].projectionMatrix = _playerHMD.Cam.GetStereoProjectionMatrix
                (camIndex == 0 ? Camera.StereoscopicEye.Left : Camera.StereoscopicEye.Right);
                SetProjectionMatrix(_cameras[camIndex]);
                _cameras[camIndex].Render();

                _rendComp.material.SetInt("_RightOverwrite", 0);
                _portalRenderer.gameObject.SetActive(true);
            }
        }

        void SetProjectionMatrix(Camera cam)
        {
            Transform clipPlane = transform;
            int dot = System.Math.Sign(Vector3.Dot(clipPlane.forward, transform.position - cam.transform.position));
            Vector3 camSpacePos = cam.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
            Vector3 camSpaceNormal = cam.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * dot;
            float camSpaceDistance = -Vector3.Dot(camSpacePos, camSpaceNormal);
            Vector4 clipPlaneMatrix = new(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camSpaceDistance);
            cam.projectionMatrix = cam.CalculateObliqueMatrix(clipPlaneMatrix);
        }
    }
}