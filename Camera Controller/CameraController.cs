using UnityEngine;

public class CameraController : Controller
{
    public bool FlightMode { get; set; } = false;

    protected override void OnStart()
    {
        if(!Validate())
        {
            Debug.LogError("");
        }

        base.OnStart();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Input()
    {
        base.Input();

        if (!UnityEngine.Input.GetKey(KeyCode.Mouse1))
        {
            return;
        }

        float xRotAxis = UnityEngine.Input.GetAxis("Mouse X");
        float yRotAxis = UnityEngine.Input.GetAxis("Mouse Y");

        if (!Mathf.Approximately(xRotAxis, 0.0f))
        {
            float x = _controllee.localRotation.eulerAngles.x;
            float z = _controllee.localRotation.eulerAngles.z;
            float y = _controllee.localRotation.eulerAngles.y + xRotAxis * RotationSpeed * Time.unscaledDeltaTime;
            _controllee.rotation = Quaternion.Euler(x, y, z);
        }

        if (!Mathf.Approximately(yRotAxis, 0.0f))
        {
            if (!FlightMode)
            {
                yRotAxis = -yRotAxis;
            }

            float x = _controllee.localRotation.eulerAngles.x + yRotAxis * RotationSpeed * Time.unscaledDeltaTime;
            float z = _controllee.localRotation.eulerAngles.z;
            float y = _controllee.localRotation.eulerAngles.y;
            _controllee.rotation = Quaternion.Euler(x, y, z);
        }

#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorState();
        }
#endif
    }

    private void ToggleCursorState()
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public override bool Validate()
    {
        base.Validate();
        return true;
    }
}
