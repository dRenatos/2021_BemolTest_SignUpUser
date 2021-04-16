using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class UserManager
{
    private static string jsonFile = "User_DataBase.json";
    
    private static List<User> _users;

    static UserManager()
    {
        if(File.Exists(jsonFile))
             LoadUsers();
    }
    
    public static bool UserExits(string cpf)
    {
        var user = _users?.Where(userInList => userInList.Cpf == cpf);
        return user?.ElementAtOrDefault(0) != null;
    }

    public static void CreateUser(string cpf, string cep, List<SetterUIElement> setterUiElements, List<UiClassifiedWarningElement> warningElements, string complement)
    {
        User newUser = new User(GetValue(warningElements, UiWarningType.Name), cpf, cep, 
            GetValue(setterUiElements, UiAddressType.City), 
            GetValue(setterUiElements, UiAddressType.State),
            GetValue(setterUiElements, UiAddressType.Street),
            GetValue(warningElements, UiWarningType.Number), 
            GetValue(setterUiElements, UiAddressType.Neighborhood), complement);
        
        if(_users == null)
            _users = new List<User>();
        
        _users.Add(newUser);
        SaveUsers();
    }

    private static void SaveUsers()
    {
        bool append = false;
        foreach (var user in _users)
        {
            string save = JsonUtility.ToJson(user);
            using (StreamWriter outputFile = new StreamWriter(jsonFile, append))
            {
                outputFile.WriteLineAsync(save);
            }
            append = true;
        }
    }

    private static void LoadUsers()
    {
        using (StreamReader outputFile = new StreamReader(jsonFile))
        {
            string line;
            while ((line = outputFile.ReadLine()) != null)
            {
                var user = JsonUtility.FromJson<User>(line);
                if(_users == null)
                    _users = new List<User>();
                _users.Add(user);
            }
        }
    }
    
    private static string GetValue(List<SetterUIElement> setterUiElements, UiAddressType addressType)
    {
        return setterUiElements.First(setterUiElement => setterUiElement.AddressType == addressType).Value;
    }
    
    private static string GetValue(List<UiClassifiedWarningElement> warningElements, UiWarningType warningType)
    {
        return warningElements.First(warningElement => warningElement.WarningType == warningType).Value;
    }
}
