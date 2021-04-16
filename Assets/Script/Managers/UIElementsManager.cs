using System;
using System.Collections.Generic;
using System.Linq;

public static class UIElementsManager
{
    public static Action FailedUserCreation;
    public static Action UserCreatedSuccessful;
    private static CEPUIElement _cepUiElement;
    private static CPFUIElement _cpfUiElement;
    private static List<SetterUIElement> _setterUiElements;
    private static List<UiClassifiedWarningElement> _warningUiElements;
    private static OptionalUIElement _complementUiElement;

    public static void RegisterElement(UIElement element)
    {
        switch (element)
        {
            case CEPUIElement cepUiElement:
                _cepUiElement = cepUiElement;
                _cepUiElement.OnValidateValue += SetCEPWebServiceValues;
                _cepUiElement.OnChangeValue += ClearCepWebServiceValues;
                break;
            case CPFUIElement cpfUiElement:
                _cpfUiElement = cpfUiElement;
                break;
            case SetterUIElement setterUIElement:
                if(_setterUiElements == null)
                    _setterUiElements = new List<SetterUIElement>();
                _setterUiElements.Add(setterUIElement);
                break;
            case UiClassifiedWarningElement warningElement:
                if(_warningUiElements == null)
                    _warningUiElements = new List<UiClassifiedWarningElement>();
                _warningUiElements.Add(warningElement);
                break;
            case OptionalUIElement optionalUiElement:
                _complementUiElement = optionalUiElement;
                break;
        }
    }

    public static void UnregisterElement(UIElement uiElement)
    {
    }

    private static void SetCEPWebServiceValues()
    {
        foreach (var setterUiElement in _setterUiElements)
        {
            setterUiElement.SetUIValue(_cepUiElement.WebServiceData);
        }
    }

    private static void ClearCepWebServiceValues()
    {
        foreach (var setterUiElement in _setterUiElements)
        {
            setterUiElement.ClearUIValue();
        }
    }

    public static bool CanCreateUser()
    {
        var setterUiElement = _setterUiElements.Where(element => element.IsValid == false);
        var warningElement = _warningUiElements.Where(element => element.IsValid == false);

        SetWarnings();
        
        return _cepUiElement.IsValid && _cpfUiElement.IsValid && _complementUiElement.IsValid &&
               setterUiElement.ElementAtOrDefault(0) == null && warningElement.ElementAtOrDefault(0) == null;
    }

    private static void SetWarnings()
    {
        _cepUiElement.SetWarning();
        _cpfUiElement.SetWarning();
        foreach (var warningUiElement in _warningUiElements)
        {
            warningUiElement.SetWarning();
        }
    }

    public static void CreateUser()
    {
        if (UserManager.UserExits(_cpfUiElement.Value))
        {
            FailedUserCreation?.Invoke();
            return;
        }
            
        UserManager.CreateUser(_cpfUiElement.Value, _cepUiElement.Value, _setterUiElements, _warningUiElements, _complementUiElement.Value);
        ClearUI();
        UserCreatedSuccessful?.Invoke();
    }

    private static void ClearUI()
    {
        _cepUiElement.ClearUIValue();
        _cpfUiElement.ClearUIValue();
        _complementUiElement.ClearUIValue();
        foreach (var warningUiElement in _warningUiElements)
        {
            warningUiElement.ClearUIValue();
        }
        ClearCepWebServiceValues();
    }
}
