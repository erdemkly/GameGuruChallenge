using Case2.Managers;
using Cinemachine;
using UnityEngine;

namespace Case2.Other
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class VirtualCameraSubscriber : MonoBehaviour
    {
        [SerializeField] private MyVirtualCamera myVirtualCam;
        private void OnEnable()
        {
            myVirtualCam.cam = GetComponent<CinemachineVirtualCamera>();
            CameraManager.Instance.RegisterVirtualCamera(myVirtualCam);
        }
        private void OnDisable()
        {
            if (CameraManager.Instance == null) return;
            CameraManager.Instance.UnRegisterVirtualCamera(myVirtualCam);
        }
    }
}
