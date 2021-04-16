public class OptionalUIElement : UIElement
{
    protected override void Awake()
    {
        base.Awake();
        _isValid = true;
    }

    protected override void OnDeselectCheck(string value)
    {
        _isValid = true;
    }

    public override void ClearUIValue()
    {
        _inputField.text = "";
    }
}
