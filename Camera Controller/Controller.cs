using UnityEngine;

[ExecuteInEditMode]
public class Controller : MonoBehaviour, ISerialisedDataValidate
{
    public bool DisableInput { get; set; } = false;
    public float TranslationSpeed { get; set; } = 1.0f;
    public float RotationSpeed { get; set; } = 50.0f;

    [SerializeField] protected Transform m_controllee;

    [SerializeField] protected KeyCode m_forward = KeyCode.W;
    [SerializeField] protected KeyCode m_right = KeyCode.D;
    [SerializeField] protected KeyCode m_back = KeyCode.S;
    [SerializeField] protected KeyCode m_left = KeyCode.A;

    private void Awake()
    {
        if(m_controllee == null)
        {
            m_controllee = GetComponent<Transform>();
        }
    }

    public void Start()
    {
        if (!Validate())
        {
            Debug.LogError("");
        }

        OnStart();
    }

    protected virtual void OnStart()
    { }

    public virtual void Input()
    {
        if (DisableInput)
        {
            return;
        }

        if (UnityEngine.Input.GetKey(m_forward))
        {
            m_controllee.localPosition += m_controllee.forward * (TranslationSpeed * Time.unscaledDeltaTime);
        }

        if (UnityEngine.Input.GetKey(m_right))
        {
            m_controllee.localPosition += m_controllee.right * (TranslationSpeed * Time.unscaledDeltaTime);
        }

        if (UnityEngine.Input.GetKey(m_back))
        {
            m_controllee.localPosition += -m_controllee.forward * (TranslationSpeed * Time.unscaledDeltaTime);
        }

        if (UnityEngine.Input.GetKey(m_left))
        {
            m_controllee.localPosition += -m_controllee.right * (TranslationSpeed * Time.unscaledDeltaTime);
        }
    }

    public virtual bool Validate()
    {
        if (m_controllee == null)
        {
            return false;
        }

        return true;
    }
}