using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SignUpUserButton : MonoBehaviour
{
    [SerializeField] private Button _button = default;
    [SerializeField] private GameObject _warningMessage = default;
    [SerializeField] private GameObject _userAlreadyAddedMessage = default;
    [SerializeField] private GameObject _userCreatedSuccessfulMessage = default;
    private Coroutine _userCreatedSuccessful;
    
    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
        UIElementsManager.FailedUserCreation += UserAlreadyAdded;
        UIElementsManager.UserCreatedSuccessful += UserCreatedSuccessful;
        _warningMessage.SetActive(false);
        _userAlreadyAddedMessage.SetActive(false);
        _userCreatedSuccessfulMessage.SetActive(false);
    }

    private void UserCreatedSuccessful()
    {
        _userCreatedSuccessful = StartCoroutine(ShowSuccessfulMessage());
    }

    private void OnDestroy()
    {
        UIElementsManager.FailedUserCreation -= UserAlreadyAdded;
        _button.onClick.RemoveListener(OnClick);
    }

    private void FailedUserCreation()
    {
        _warningMessage.SetActive(true);
    }

    private void UserAlreadyAdded()
    {
        _userAlreadyAddedMessage.SetActive(true);
    }
    
    private void OnClick()
    {
        if (_userCreatedSuccessful != null)
        {
            StopCoroutine(_userCreatedSuccessful);
        }
        _warningMessage.SetActive(false);
        _userAlreadyAddedMessage.SetActive(false);
        _userCreatedSuccessfulMessage.SetActive(false);
        if (!UIElementsManager.CanCreateUser())
        {
            FailedUserCreation();
            return;
        }
        
        UIElementsManager.CreateUser();
    }

    private IEnumerator ShowSuccessfulMessage()
    {
        _userCreatedSuccessfulMessage.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        _userCreatedSuccessfulMessage.SetActive(false);
        _userCreatedSuccessful = null;
    }
}
