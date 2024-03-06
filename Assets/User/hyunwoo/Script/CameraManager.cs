using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float panSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomInMax;
    [SerializeField] private float zoomOutMax;

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;
    private UnityEngine.Transform cameraTransform;
    
    private void Awake() {
        inputProvider = GetComponent<CinemachineInputProvider>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = virtualCamera.VirtualCameraGameObject.transform;
    }

    private void Update() {
        float x = inputProvider.GetAxisValue(0);
        float y = inputProvider.GetAxisValue(1);
        float z = inputProvider.GetAxisValue(2);

        if(x != 0 || y != 0) {
            PanScreen(x, y);
        }
        if( z != 0) {
            ZoomScreen(-z);
        }
    }

    public void ZoomScreen(float increment) {
        float fov = virtualCamera.m_Lens.FieldOfView;
        float target = Mathf.Clamp(fov + increment, zoomInMax, zoomOutMax);
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(fov, target, zoomSpeed * Time.deltaTime);
    }

    public Vector2 PanDirection(float x, float y) {
        Vector2 direction = Vector2.zero;
        if(y >= Screen.height * 0.97f) {
            direction.y += 1;
        } else if(y <= Screen.height * 0.03f) {
            direction.y -= 1;
        }
        if(x >= Screen.width * 0.97f) {
            direction.x += 1;
        } else if(x <= Screen.width * 0.03f) {
            direction.x -= 1;
        }

        return direction;
    }

    public void PanScreen(float x, float y) {
        Vector2 direction = PanDirection(x, y);
        Vector3 cam_Dir = new Vector3(direction.x, 0, direction.y);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraTransform.position + cam_Dir * panSpeed, Time.deltaTime );
    }
}
