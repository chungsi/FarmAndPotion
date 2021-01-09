using UnityEngine;
using UnityEngine.Events;

// attach to any object that wants to listen to some event
public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public  UnityEvent Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised() {
        Response.Invoke();
    }
}
