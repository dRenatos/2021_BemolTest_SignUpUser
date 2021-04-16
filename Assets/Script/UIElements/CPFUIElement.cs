public class CPFUIElement : UiWarningElement
{
    private static int _cpfLength = 14;
    public new string Value => _inputField.text.Replace(".", "").Replace("-", "");

    protected override void Awake()
    {
        base.Awake();
        _inputField.characterLimit = _cpfLength;
    }

    protected override void OnValueChangeCheck(string value)
    {
        if (Value.Length > 1 && Value.Length % 3 == 1 && value.LastIndexOf(".") != value.Length - 2 && Value.Length % 10 != 0)
        {
            _inputField.text = value.Substring(0, value.Length - 1) + "." + value.Substring(value.Length - 1);
            _inputField.caretPosition++;
        }
        
        if (Value.Length > 1 && Value.Length % 10 == 0 && value.LastIndexOf("-") != value.Length - 2)
        {
            _inputField.text = value.Substring(0, value.Length - 1) + "-" + value.Substring(value.Length - 1);
            _inputField.caretPosition++;
        } 
        
        if (value.EndsWith("-"))
        {
            _inputField.text = value.Remove(value.IndexOf("-"));
        }
        
        if (value.EndsWith("."))
        {
             _inputField.text = value.Remove(value.IndexOf("."));
        }

        _isValid = value.Length >= _cpfLength;
        _warningMessage.SetActive(false);
    }

    protected override void OnDeselectCheck(string value)
    {
        if (value.Length < _cpfLength)
        {
            _isValid = false;
            _warningMessage.SetActive(true);
            return;
        }
        _warningMessage.SetActive(false);
    }
    
}
