using UnityEngine;

[ExecuteInEditMode]
public class Controller : MonoBehaviour, ISerialisedDataValidate
{
    public bool DisableInput { get; set; } = false;
    public float TranslationSpeed { get; set; } = 1.0f;
    public float RotationSpeed { get; set; } = 50.0f;

    [SerializeField] protected Transform _controllee;

    [SerializeField] protected KeyCode _forward;
    [SerializeField] protected KeyCode _right;
    [SerializeField] protected KeyCode _back;
    [SerializeField] protected KeyCode _left;

    private void Awake()
    {
        if(_controllee == null)
        {
            _controllee = GetComponent<Transform>();
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

        if (UnityEngine.Input.GetKey(_forward))
        {
            _controllee.localPosition += _controllee.forward * (TranslationSpeed * Time.unscaledDeltaTime);
        }

        if (UnityEngine.Input.GetKey(_right))
        {
            _controllee.localPosition += _controllee.right * (TranslationSpeed * Time.unscaledDeltaTime);
        }

        if (UnityEngine.Input.GetKey(_back))
        {
            _controllee.localPosition += -_controllee.forward * (TranslationSpeed * Time.unscaledDeltaTime);
        }

        if (UnityEngine.Input.GetKey(_left))
        {
            _controllee.localPosition += -_controllee.right * (TranslationSpeed * Time.unscaledDeltaTime);
        }
    }

    public virtual bool Validate()
    {
        if (_controllee == null)
        {
            return false;
        }

        return true;
    }
}