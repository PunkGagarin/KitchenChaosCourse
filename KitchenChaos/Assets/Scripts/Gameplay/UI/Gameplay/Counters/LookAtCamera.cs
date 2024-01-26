using System;
using UnityEngine;

namespace DefaultNamespace
{

    public class LookAtCamera : MonoBehaviour
    {

        private Camera _mainCamera;

        [SerializeField]
        private CameraLookAtMode _mode;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            switch (_mode)
            {
                case CameraLookAtMode.ForwardInverted:
                    transform.forward = -_mainCamera.transform.forward;
                    break;
                case CameraLookAtMode.Forward:
                    transform.forward = _mainCamera.transform.forward;
                    break;
                case CameraLookAtMode.LookAtInverted:
                {
                    Vector3 direction = transform.position - _mainCamera.transform.position;
                    transform.LookAt(transform.position + direction);
                    break;
                }
                case CameraLookAtMode.LookAt:
                {
                    transform.LookAt(_mainCamera.transform.position);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }


}