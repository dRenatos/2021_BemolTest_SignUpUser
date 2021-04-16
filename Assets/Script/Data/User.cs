using System;

[Serializable]
public class User
{
    //In order to use these attributes in the class JsonUtility from unity engine all the properties have to be public and the class set to be serializable
    public string Name;
    public string Cpf;
    public UserAddress Address;

    public User(string name, string cpf, string cep, string city, string state, string street, string number, string neighborhood, string complement)
    {
        Name = name;
        Cpf = cpf;
        Address = new UserAddress(cep, city, state, street, number, neighborhood, complement);
    }

    [Serializable]
    public class UserAddress
    {
        public string CEP;
        public string City;
        public string State;
        public string Street;
        public string Number;
        public string Neighborhood;
        public string Complement;

        public UserAddress(string cep, string city, string state, string street, string number, string neighborhood, string complement)
        {
            CEP = cep;
            City = city;
            State = state;
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            Complement = complement;
        }
    }
}
