using UnityEngine;
using UnityEngine.SceneManagement;

public class DaySceneChange : MonoBehaviour
{
    public System.DateTime startTime = new System.DateTime(2137,1,1,0,0,0);
    public System.DateTime endTime = new System.DateTime(2137, 1, 1, 0, 0, 0);
    public void ChangeScene(string SceneName)
    {
        if(SceneManager.GetActiveScene().name == "DayView2")
        {
            Event e = SavedEvents.events[SavedEvents.currentlyEditedEvent];
            e.startDate = e.startDate.AddHours(startTime.Hour-1).AddMinutes(startTime.Minute-1);
            e.endDate = e.endDate.AddHours(endTime.Hour-2).AddMinutes(endTime.Minute-1);
            SavedEvents.events[SavedEvents.currentlyEditedEvent] = e;
            SavedEvents.lastScene = SceneManager.GetActiveScene().name;
        }
        SceneManager.LoadScene(SceneName);
    }

    public void GoBack()
    {
        if (SceneManager.GetActiveScene().name == "ST_TeamEventAdd")
        {
            if(SavedEvents.currentlyEditedEvent >= 0)
            {
                SavedEvents.events.Remove(SavedEvents.events[SavedEvents.currentlyEditedEvent]);
            }
            print("dsa");
            SceneManager.LoadScene("MainView");
        }
        else if(SceneManager.GetActiveScene().name == "DayView2")
        {
            SceneManager.LoadScene("ST_TeamEventAdd");
        }
        else
        {
            SceneManager.LoadScene(SavedEvents.lastScene);
        }
    }
}
