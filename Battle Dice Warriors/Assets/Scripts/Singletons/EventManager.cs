using UnityEngine.Events;

public class EventManager
{
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new EventManager();

            return _instance;
        }
    }

    private EventManager() 
    {

    }

    public UnityAction OnResetAction { get; set; }
    public UnityAction OnUndoClicked { get; set; }
}