using TMPro;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    [SerializeField] protected TMP_InputField _inputField;
    protected bool _isValid;
    public bool IsValid => _isValid;
    public string Value => _inputField.text;

    protected virtual void Awake()
    {
        UIElementsManager.RegisterElement(this);
        _inputField.onDeselect.AddListener(OnDeselectCheck);
        _inputField.onValueChanged.AddListener(OnValueChangeCheck);
    }

    private void OnDestroy()
    {
        UIElementsManager.UnregisterElement(this);
        _inputField.onDeselect.RemoveListener(OnDeselectCheck);
        _inputField.onValueChanged.RemoveListener(OnValueChangeCheck);
    }

    protected virtual void OnValueChangeCheck(string value)
    {
        _isValid = true;
    }
    
    protected virtual void OnDeselectCheck(string value)
    {
        if (!_inputField.interactable) 
            return;
        _isValid = !string.IsNullOrEmpty(value);
    }
    
    public virtual void ClearUIValue()
    {
        _inputField.text = "";
        _isValid = false;
    }
}
