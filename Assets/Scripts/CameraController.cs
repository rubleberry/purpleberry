using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
/* test comments */
    private float _rotationSpeed = 30f;
    private float _motionSpeed = 3f;
    private float _verticalSpeed = 0.1f;
/*    private float _fov = 80;
    private float _mapDragSpeed = 0.3f;
    private float _zoomSpeed = 0.5f;

    private float _fovMax = 100;
    private float _fovMin = 30;
*/
    private float _cameraHeightMax = 10;
    private float _cameraHeightMin = 1;

    private float _rotationDownLimit = 85f;
    private float _rotationUpLimit = 1f;

    private InputAction _moveAction;
    private InputAction _pointerPositionAction;

    public GameObject unitToControl;
    public GameObject unitToTarget;
    public Vector3 coordToMove;
    public Vector3 coordOfOrigin;
    private TextMeshProUGUI _text0;
    private PanelController _controlPanelController;

    [SerializeField] 
    public GameObject Text0;

    [SerializeField]
    public GameObject ControlPanel;

    void Start()
    {
        Application.targetFrameRate = 30;
        _moveAction = InputSystem.actions.FindAction("Move");
        _pointerPositionAction = new InputAction("Pointer/Position", binding: "<Mouse>/position");
        _pointerPositionAction.Enable();
        _text0 = Text0.GetComponent<TextMeshProUGUI>();
        _controlPanelController = ControlPanel.GetComponent<PanelController>();
        ControlPanel.SetActive(false);
    }

    void Update()
    {
        Vector3 _screenPoint = _pointerPositionAction.ReadValue<Vector2>();
        CameraMotion();
        CameraRotation();
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                unitToControl = null;
                PrintText0("");
                coordToMove = coordOfOrigin;
                ControlPanel.SetActive(false);
            }
            else if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(_screenPoint);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
//                    Debug.Log(hit.point);
//                    Debug.Log(hit.transform.tag);
                    if (hit.transform.tag == null)
                    {
                        Debug.LogError("Tag is Null.");
                    }
                    else if (hit.transform.tag == "UI")
                    {

                    }
                    else if (hit.transform.tag == "Unit")
                    {
                        //                    hit.transform.GetComponent<UnitController>().OnClick();
                        if (unitToControl == null)
                        {
                            unitToControl = hit.transform.gameObject;
                            _controlPanelController.unitToControl = unitToControl;
                            PrintText0(unitToControl.name);
                            ControlPanel.SetActive(true);
                            ControlPanel.GetComponent<PanelController>().InitControlPanel(unitToControl);
                        }
                        else
                        {
                            unitToTarget = hit.transform.gameObject;
                            _controlPanelController.unitToTarget = unitToTarget;
                            UnitTactic unitTactic = unitToControl.GetComponent<UnitTactic>();
                            unitTactic.SetTarget(unitToTarget);
                        }
                    }
                    else if (hit.transform.tag == "Terrain")
                    {
                        //                    ControlPanel.SetActive(false);
                        coordToMove = hit.point;
                    }
                }
            }
            if (unitToControl != null && coordToMove != coordOfOrigin)
            {
                coordToMove = coordToMove + new Vector3(0, unitToControl.transform.position.y, 0);
                unitToControl.GetComponent<UnitController>().SetDestination(coordToMove);

                unitToControl = null;
                coordToMove = coordOfOrigin;
            }

        }

    }
    private void CameraMotion()
    {
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        Vector3 _motion = new Vector3(moveValue.x, 0, moveValue.y);
        float _formerPositionY = transform.position.y;
        transform.Translate(_motion * _motionSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, _formerPositionY, transform.position.z);

        if (transform.position.x < 0)
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        if (transform.position.z < 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (Keyboard.current.cKey.isPressed)
        {
            if (transform.position.y < _cameraHeightMax)
                transform.position = new Vector3(transform.position.x, transform.position.y + _verticalSpeed, transform.position.z);
        }
        if (Keyboard.current.vKey.isPressed)
        {
            if (transform.position.y > _cameraHeightMin)
                transform.position = new Vector3(transform.position.x, transform.position.y - _verticalSpeed, transform.position.z);
        }
    }

    private void CameraRotation()
    {
        if (Keyboard.current.qKey.isPressed)
        {
            transform.Rotate(0f, -_rotationSpeed * Time.deltaTime, 0f, Space.World);
        }
        if (Keyboard.current.eKey.isPressed)
        {
            transform.Rotate(0f, _rotationSpeed * Time.deltaTime, 0f, Space.World);
        }
        if (Keyboard.current.zKey.isPressed)
        {
            if (Camera.main.transform.rotation.eulerAngles.x < _rotationDownLimit)
                transform.Rotate(_rotationSpeed * Time.deltaTime, 0f, 0f);
        }
        if (Keyboard.current.xKey.isPressed)
        {
            if (Camera.main.transform.rotation.eulerAngles.x > _rotationUpLimit)
                transform.Rotate(-_rotationSpeed * Time.deltaTime, 0f, 0f);
        }
    }

    private void PrintText0(string text)
    {
        _text0.text = "Unit to control: " + text;
    }

}
