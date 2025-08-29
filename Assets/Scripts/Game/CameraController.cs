using Cinemachine;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachinePOV povComponent;
    private Vector2 movementCamera;
    private Transform cameraMain;

    [SerializeField] private float sensibility;

    [Header("Raycast")]
    [SerializeField] LayerMask objectLayer;
    private RaycastHit raycastObject;

    private bool confirmsInput = true;
    InteractiveObject interactive;

    [Header("do")]
    [SerializeField] private DoUI doUI;
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        cameraMain = Camera.main.transform;
    }
    private void Update()
    {
        povComponent.m_VerticalAxis.Value = povComponent.m_VerticalAxis.Value  -movementCamera.y * sensibility * Time.deltaTime;
        povComponent.m_HorizontalAxis.Value = povComponent.m_HorizontalAxis.Value + movementCamera.x * sensibility * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(cameraMain.transform.position, cameraMain.forward * 2f, Color.green);
        if (Physics.Raycast(cameraMain.transform.position, cameraMain.transform.forward, out raycastObject, 2f, objectLayer))
        {
            if (confirmsInput)
            {
                interactive = raycastObject.collider.gameObject.GetComponent<InteractiveObject>();
                interactive.Input(true);
                confirmsInput = false;
                doUI.Open();
            }
        }
        else
        {
            if (!confirmsInput)
            {
                interactive.Input(false);
                interactive = null;
                confirmsInput = true;
                doUI.Close();
            }
        }
    }
    public void MovementCamera(Vector2 value)
    {
        movementCamera = value;
    }
    private void NewSensibility(float sensibility)
    {
        this.sensibility = sensibility;
    }
    private void OnEnable()
    {
        SettingManager.eventSensibility += NewSensibility;
        InputReader.movementCamera += MovementCamera;
    }
    private void OnDisable()
    {
        SettingManager.eventSensibility -= NewSensibility;
        InputReader.movementCamera -= MovementCamera;
    }
}
