using UnityEngine;

public class SetterUIElement : UIElement
{
    [SerializeField] private UiAddressType _addressType = default;
    public UiAddressType AddressType => _addressType;
    
    public void SetUIValue(WebServiceResponseAddress webServiceResponseAddress)
    {
        switch (_addressType)
        {
            case UiAddressType.Street:
                _inputField.text = webServiceResponseAddress.logradouro;
                break;
            case UiAddressType.City:
                _inputField.text = webServiceResponseAddress.localidade;
                break;
            case UiAddressType.Neighborhood:
                _inputField.text = webServiceResponseAddress.bairro;
                break;
            case UiAddressType.State:
                _inputField.text = webServiceResponseAddress.uf;
                break;
        }
        _isValid = true;
    }
}

public enum UiAddressType
{
    Street,
    City,
    Neighborhood,
    State
}
