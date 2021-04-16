using UnityEngine;

public class UiClassifiedWarningElement : UiWarningElement
{
    [SerializeField] private UiWarningType _warningType = default;
    public UiWarningType WarningType => _warningType;
}

public enum UiWarningType
{
    Number,
    Name
}