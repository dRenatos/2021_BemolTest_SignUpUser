using System.Collections;
using System.Threading;

public class ThreadJob
{
    private bool _IsDone = false;
    private object _Handle = new object();
    private Thread _Thread = null;
    
    public bool IsDone
    {
        get
        {
            bool tmp;
            lock (_Handle)
            {
                tmp = _IsDone;
            }
            return tmp;
        }
        set
        {
            lock (_Handle)
            {
                _IsDone = value;
            }
        }
    }
 
    public virtual void Start()
    {
        _Thread = new Thread(Run);
        _Thread.Start();
    }
 
    protected virtual void ThreadFunction() { }
 
    protected virtual void OnFinished() { }

    public IEnumerator WaitFor()
    {
        while (!Update())
        {
            yield return null;
        }
    }
    
    public virtual bool Update()
    {
        if (!IsDone)
        {
            return false;
        }
        OnFinished();
        return true;
    }
   
    private void Run()
    {
        ThreadFunction();
        IsDone = true;
    }
}