using UnityEngine;

public class CameraController : Controller
{
    public bool FlightMode { get; set; } = false;

    [SerializeField] protected KeyCode m_up = KeyCode.Space;
    [SerializeField] protected KeyCode m_down = KeyCode.LeftControl;

    protected override void OnStart()
    {
        if(!Validate())
        {
            Debug.LogError("");
        }

        base.OnStart();
    }

    public override void Input()
    {
        if (!UnityEngine.Input.GetKey(KeyCode.Mouse1))
        {
            return;
        }

        base.Input();

        if(UnityEngine.Input.GetKey(m_up))
        {
            m_controllee.localPosition += m_controllee.up * (TranslationSpeed * Time.unscaledDeltaTime);
        }

        if(UnityEngine.Input.GetKey(m_down))
        {
            m_controllee.localPosition += -m_controllee.up * (TranslationSpeed * Time.unscaledDeltaTime);
        }

        float xRotAxis = UnityEngine.Input.GetAxis("Mouse X");
        float yRotAxis = UnityEngine.Input.GetAxis("Mouse Y");

        if (!Mathf.Approximately(xRotAxis, 0.0f))
        {
            float x = m_controllee.localRotation.eulerAngles.x;
            float z = m_controllee.localRotation.eulerAngles.z;
            float y = m_controllee.localRotation.eulerAngles.y + xRotAxis * RotationSpeed * Time.unscaledDeltaTime;
            m_controllee.rotation = Quaternion.Euler(x, y, z);
        }

        if (!Mathf.Approximately(yRotAxis, 0.0f))
        {
            if (!FlightMode)
            {
                yRotAxis = -yRotAxis;
            }

            float x = m_controllee.localRotation.eulerAngles.x + yRotAxis * RotationSpeed * Time.unscaledDeltaTime;
            float z = m_controllee.localRotation.eulerAngles.z;
            float y = m_controllee.localRotation.eulerAngles.y;
            m_controllee.rotation = Quaternion.Euler(x, y, z);
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
