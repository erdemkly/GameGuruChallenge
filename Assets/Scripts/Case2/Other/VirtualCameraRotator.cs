using System;
using Cinemachine;
using UnityEngine;
namespace Case2.Other
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class VirtualCameraRotator : MonoBehaviour
    {
        private CinemachineVirtualCamera _vCam;
        private CinemachineOrbitalTransposer _orbitalTransposer;

        private void Awake()
        {
            _vCam = GetComponent<CinemachineVirtualCamera>();
            _orbitalTransposer = _vCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        }

        public void ResetOrbital()
        {
            _orbitalTransposer.m_XAxis.Value = 0;
        }
        private void Update()
        {
            if (!_vCam.enabled) return;
            _orbitalTransposer.m_XAxis.Value += Time.deltaTime*_orbitalTransposer.m_XAxis.m_MaxSpeed;
        }
    }
}
