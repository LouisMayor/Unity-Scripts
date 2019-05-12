using UnityEngine;

[AddComponentMenu("User/MainCamera"),
 RequireComponent(typeof(CameraController)),
 RequireComponent(typeof(Camera)),
 ExecuteInEditMode]
public class MainCamera : MonoBehaviour, ISerialisedDataValidate
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private CameraController m_controller;

    private void Awake()
    {
        if(m_camera == null)
        {
            m_camera = GetComponent<Camera>();
        }

        if(m_controller == null)
        {
            m_controller = GetComponent<CameraController>();
        }
    }

    private void Start()
    {
        if (!Validate())
        {
            Debug.LogError("Check prefab serialised fields");
        }
    }

    [ContextMenu("Reset Camera")]
    public void ResetToOrigin()
    {
        m_camera.transform.position = Vector3.zero;
        m_camera.transform.rotation = Quaternion.identity;
    }

    public bool Validate()
    {
        if (m_camera == null)
        {
            return false;
        }

        if (m_controller == null)
        {
            return false;
        }

        return true;
    }

    private void Update()
    {
        m_controller.Input();
    }
}