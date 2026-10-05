using System;

public class DebugCommandBase
{
    private string _id;
    private string _description;
    private string _format;

    public string commandId
    {
        get { return _id; }
    }
    public string commandDescription
    {
        get { return _description; }
    }
    public string commandFormat
    {
        get { return _format; }
    }

    public DebugCommandBase(string id, string description, string format)
    {
        _id = id;
        _description = description;
        _format = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action _command;

    public DebugCommand(string id, string description, string format, Action action)
        : base(id, description, format)
    {
        _command = action;
    }

    public void Invoke()
    {
        _command.Invoke();
    }
}
