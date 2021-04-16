using UnityEngine;

public class UiWarningElement : UIElement
{
    [SerializeField] protected GameObject _warningMessage = default;

    protected override void Awake()
    {
        base.Awake();
        _warningMessage.SetActive(false);
    }

    protected override void OnDeselectCheck(string value)
    {
        base.OnDeselectCheck(value);
        _warningMessage.SetActive(!_isValid);
    }

    public virtual void SetWarning()
    {
        _warningMessage.SetActive(!_isValid);
    }
    
}

