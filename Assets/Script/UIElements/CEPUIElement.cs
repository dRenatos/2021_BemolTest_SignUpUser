using System;
using UnityEngine;

public class CEPUIElement : UiWarningElement
{
    private static int _cepLength = 9;
    public WebServiceResponseAddress WebServiceData { get; private set; }
    public new string Value => _inputField.text.Replace("-", "");
    public Action OnValidateValue;
    public Action OnChangeValue;
    
#region Unity Events
    
    protected override void Awake()
    {
        base.Awake();
        _inputField.characterLimit = _cepLength;
    }
    
#endregion
    
#region Check UI Values
    
    protected override void OnValueChangeCheck(string value)
    {
        if (value.Length >= 6 && !value.Contains("-"))
        {
            _inputField.text = value.Substring(0, 5) + "-" + value.Substring(5);
            _inputField.caretPosition++;
        }

        if (value.EndsWith("-"))
        {
            _inputField.text = value.Remove(value.IndexOf("-"));
        }

        if (value.Length < _cepLength)
        {
            _isValid = false;
            OnChangeValue?.Invoke();
        }
        else
        {
            _warningMessage.SetActive(false);
            GetAddressInformation();
        }
    }

    protected override void OnDeselectCheck(string value)
    {
        if (value.Length < _cepLength)
        {
            _isValid = false;
            _warningMessage.SetActive(true);
            return;
        }
        _warningMessage.SetActive(false);
        GetAddressInformation();
    }
    
#endregion

#region Get Web Service Data

    private void GetAddressInformation()
    {
        var check = new CEPWebServiceThreadJob(Value, RequestSuccess, OnRequestFail);
        check.Start();
        StartCoroutine(check.WaitFor());
    }

    private void OnRequestFail()
    {
        _isValid = false;
        _warningMessage.SetActive(true);
    }

    private void RequestSuccess(string obj)
    {
        _isValid = true;
        WebServiceData = JsonUtility.FromJson<WebServiceResponseAddress>(obj);
        if (!CheckDataIntegrity())
        {
            OnRequestFail();
            return;
        }
        _warningMessage.SetActive(false);
        OnValidateValue?.Invoke();
    }

    private bool CheckDataIntegrity()
    {
        return !string.IsNullOrEmpty(WebServiceData.bairro) || !string.IsNullOrEmpty(WebServiceData.logradouro) 
                                                            || !string.IsNullOrEmpty(WebServiceData.uf) 
                                                            || !string.IsNullOrEmpty(WebServiceData.localidade);
    }

#endregion

}
