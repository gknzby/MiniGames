using Gknzby.UI;
using Gknzby.Components;
using Gknzby.Managers;
using UnityEngine;

public class RingStackHUD : UIMenu, IEventListener<ScoreData>
{
    public void HandleEvent(ScoreData eventArg)
    {
        if(eventArg == null)
        {
            Debug.Log("Null invoked");
        }
        else
        {
            Debug.Log("ScoreData invoked");
            Debug.Log(eventArg.subGame);
        }
    }

    public void HandleEvent()
    {
        Debug.Log("No data invoked");
    }

    private void Start()
    {
        base.RegisterToUIManager();
        ManagerProvider.GetManager<IEventManager>().AddEventListener(Gknzby.EventName.ScoreChange, this);
    }

    private void OnDestroy()
    {
        ManagerProvider.GetManager<IEventManager>()?.RemoveEventListener(Gknzby.EventName.ScoreChange, this);
    }
}
