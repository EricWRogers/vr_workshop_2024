using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StereoPortalsVR
{
    public class VRPortal : MonoBehaviour
    {
        [SerializeField] HMD _playerHMD;
        [SerializeField] Transform _targetPortal;
        [SerializeField] int _maxRecursion;
        [SerializeField] Vector2Int _renderTextureSize;

        readonly Camera[] _cameras = new Camera[2];

        Transform _portalRenderer;
        Renderer _targetPortalRendComp;


        void Awake()
        {
            _cameras[0] = transform.Find("LeftCamera").GetComponent<Camera>();
            _cameras[1] = transform.Find("RightCamera").GetComponent<Camera>();

            _portalRenderer = transform.Find("PortalRenderer");
            _targetPortalRendComp = _targetPortal.Find("PortalRenderer").GetComponent<MeshRenderer>();
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
            _targetPortalRendComp.material.SetTexture("_LeftEyeTex", leftEyeTexture);
            _targetPortalRendComp.material.SetTexture("_RightEyeTex", rightEyeTexture);
        }

        void Update()
        {
            for (int i = 0; i < 2; i++)
            {
                Matrix4x4 playerMatrix = _playerHMD.Eyes[i].localToWorldMatrix;
                Matrix4x4[] matrices = new Matrix4x4[_maxRecursion];
                for (int j = 0; j < _maxRecursion; j++)
                {
                    playerMatrix = transform.localToWorldMatrix * _targetPortal.worldToLocalMatrix * playerMatrix;
                    matrices[j] = playerMatrix;
                }
                RenderCamera(matrices, i);
            }
        }

        public bool IsInView()
        {
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(_playerHMD.Cam);
            return GeometryUtility.TestPlanesAABB(frustumPlanes, _targetPortalRendComp.bounds);
        }

        void RenderCamera(Matrix4x4[] matrices, int camIndex)
        {
            if (IsInView())
            {
                _targetPortalRendComp.material.SetInt("_RightOverwrite", camIndex);
                _portalRenderer.gameObject.SetActive(false);

                for (int i = _maxRecursion - 1; i >= 0; i--)
                {
                    _cameras[camIndex].transform.SetPositionAndRotation(matrices[i].GetColumn(3), matrices[i].rotation);
                    _cameras[camIndex].projectionMatrix = _playerHMD.Cam.GetStereoProjectionMatrix
                    (camIndex == 0? Camera.StereoscopicEye.Left : Camera.StereoscopicEye.Right);
                    SetProjectionMatrix(_cameras[camIndex]);
                    _cameras[camIndex].Render();
                }

                _targetPortalRendComp.material.SetInt("_RightOverwrite", 0);
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