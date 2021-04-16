using System;
using System.IO;
using System.Net;

public class CEPWebServiceThreadJob : ThreadJob
{
    private static string _urlLink = "https://viacep.com.br/ws/";
    private static string _format = "/json/";
    private readonly Action<string> _onSuccess;
    private readonly Action _onFailed;
    private readonly string _cep;
    private bool _failed;
    private string _responseFromServer;
 
    public CEPWebServiceThreadJob(string cep, Action<string> onSuccess, Action onFailed)
    {
        _cep = cep;
        _onSuccess = onSuccess;
        _onFailed = onFailed;
    }
    
    protected override void ThreadFunction()
    {
        WebRequest request = WebRequest.Create($"{_urlLink}{_cep}{_format}");
        try
        {
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream ();
            StreamReader reader = new StreamReader (dataStream);
            _responseFromServer = reader.ReadToEnd ();
            reader.Close ();
            dataStream.Close ();
            response.Close ();
        }
        catch (Exception e)
        {
            _failed = true;
        }
    }

    protected override void OnFinished()
    {
        if (_failed)
        {
            _onFailed.Invoke();
            return;
        }
        
        _onSuccess.Invoke(_responseFromServer); 
    }
}
