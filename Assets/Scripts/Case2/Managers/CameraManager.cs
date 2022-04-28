using System;
using System.Collections.Generic;
using Cinemachine;
using Helper;
using UnityEngine;
using UnityEngine.Events;

namespace Case2.Managers
{
    [Serializable]
    public struct MyVirtualCamera
    {
        [HideInInspector]public CinemachineVirtualCamera cam;
        public string camId;
        public UnityEvent onActive;
        public MyVirtualCamera(CinemachineVirtualCamera cam,string camId,UnityEvent onActive)
        {
            this.cam = cam;
            this.camId = camId;
            this.onActive = onActive;
        }
    }
    public class CameraManager : MonoSingleton<CameraManager>
    {
        public Camera mainCamera;
        public HashSet<MyVirtualCamera> allVirtualCameras;
        public void RegisterVirtualCamera(MyVirtualCamera cam)
        {
            if (allVirtualCameras == null) allVirtualCameras = new HashSet<MyVirtualCamera>();
            allVirtualCameras.Add(cam);
        }
        public void UnRegisterVirtualCamera(MyVirtualCamera cam)
        {
            if (allVirtualCameras == null) return;
            if (!allVirtualCameras.Contains(cam)) return;
            allVirtualCameras.Remove(cam);
        }
        public void SetCamera(string id)
        {
            foreach (var cam in allVirtualCameras)
            {
                var b = string.Equals(cam.camId, id, StringComparison.CurrentCultureIgnoreCase);
                cam.cam.enabled = b;
                if (b)
                {
                    cam.onActive?.Invoke();
                }
            }
        }

        private void Start()
        {
            mainCamera = Camera.main;
        }
    } 
}
