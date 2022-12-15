using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventEditorScript : MonoBehaviour
{   
    public InputField titleInputField;

    // public TimePicker timePicker;
    public ScrollTimePicker scrollTimePicker;
    public Text timeStartText;
    public Text timeEndText;

    public RepeatPicker repeatPicker;
    public Text repeatingText;

    public ReminderPicker reminderPicker;
    public Transform remindersContainer;
    public GameObject reminderPrefab;

    public GameObject colorPicker;
    public Image colorImage;

    public InputField notesInputField;

    public GameObject peoplePicker;
    public GameObject peopleList;
    public GameObject personPrefab;
    /*
    public enum RepeatingType 
    {
        None, Daily, Weekly, Monthly, Yearly
    }*/

    /*public class Repeating
    {
        public RepeatingType type = RepeatingType.None;
        public int amount = 1;
    }*/

    /*
    class CallendarEvent 
    {
        public string name;
        public Color color = new Color(0.0f, 0.32f, 1.0f, 1.0f); // #0052FF blue

        public System.DateTime startTime;
        public System.DateTime endTime;
        public bool isAllDay = false;

        public Repeating repeating;

        public List<System.TimeSpan> reminders;

        public string notes; //todo: handle checkboxes?

        // public List<invited people> invitedPeople;
    }*/

    Event currentEvent;
    bool isEditingStartTime = true;

    void Start()
    {
        // Load event if edit, create new if new
        if(SavedEvents.lastScene == "MainView")
        {
            currentEvent = new Event();
            currentEvent.startDate = System.DateTime.Now;
            System.TimeSpan oneHour = System.TimeSpan.FromHours(1);
            currentEvent.endDate = currentEvent.startDate.Add(oneHour);
            SetTimeTextFields();

            Repeating repeat;
            repeat.amount = 1;
            repeat.type = RepeatingType.None;

            currentEvent.repeating = repeat;
            SetRepeatingTextField();

            System.TimeSpan tenMinutes = System.TimeSpan.FromMinutes(10);
            currentEvent.reminders = new List<System.TimeSpan>();
            currentEvent.reminders.Add(tenMinutes);
            SetRemindersTextFields();

            currentEvent.eventColor = OurColors.blue;
            colorImage.color = OurColors.GetColor(currentEvent.eventColor);
        }
        else if(SavedEvents.lastScene == "DayView2")
        {
            currentEvent = SavedEvents.events[SavedEvents.currentlyEditedEvent];
            SetTimeTextFields();
            SetRepeatingTextField();
            SetRemindersTextFields();
            colorImage.color = OurColors.GetColor(currentEvent.eventColor);
        }

    }

    void SetTimeTextFields()
    {
        if (currentEvent.isAllDay)
        {
            timeStartText.text = currentEvent.startDate.ToString("dd.MM.yy  ");
            timeEndText.text = currentEvent.endDate.ToString("dd.MM.yy  ");
        }
        else
        {
            timeStartText.text = currentEvent.startDate.ToString("dd.MM.yy      HH:mm  ");
            timeEndText.text = currentEvent.endDate.ToString("dd.MM.yy      HH:mm  ");
        }
    }

    public void AllDayTicked(bool value)
    {
        currentEvent.isAllDay = !currentEvent.isAllDay;
        SetTimeTextFields();
    }

    void SetRepeatingTextField()
    {
        if (currentEvent.repeating.type == RepeatingType.None)
        {
            repeatingText.text = "Wydarzenie jednorazowe  ";
        }
        else
        {
            string suffix = "";
            switch (currentEvent.repeating.type)
            {
                case RepeatingType.Daily:
                {
                    suffix = " dni  ";
                    break;
                }
                case RepeatingType.Weekly:
                {
                    suffix = " tygodni  ";
                    break;
                }
                case RepeatingType.Monthly:
                {
                    suffix = " miesięcy  ";
                    break;
                }
                case RepeatingType.Yearly:
                {
                    suffix = " lat  ";
                    break;
                }
            }
            repeatingText.text = "Co " + currentEvent.repeating.amount + suffix;
        }
    }

    void SetRemindersTextFields()
    {
        foreach (Transform child in remindersContainer) 
        {
            Destroy(child.gameObject);
        }
        
        foreach (System.TimeSpan reminder in currentEvent.reminders)
        {
            var reminderGO = Instantiate(reminderPrefab, remindersContainer);
            var reminderText = reminderGO.GetComponentInChildren<Text>();
            reminderText.text = "" + reminder.TotalMinutes + " minut przed  ";

            var reminderScript = reminderGO.GetComponentInChildren<ReminderRemoveButton>();
            reminderScript.timespan = reminder;
        }

        Canvas.ForceUpdateCanvases();
    }
    
    public void HandleAddReminderButton()
    {

        reminderPicker.gameObject.SetActive(true);
        reminderPicker.SetupFields();

        // currentEvent.reminders.Add(System.TimeSpan.FromMinutes(temporaryMinuteSelectLol));
        // temporaryMinuteSelectLol += 10;
        // SetRemindersTextFields();
    }

    public void HandleConfirmAddReminderButton()
    {
        var timespan = reminderPicker.GetValues();
        currentEvent.reminders.Add(timespan);
        reminderPicker.gameObject.SetActive(false);
        SetRemindersTextFields();
    }

    public void HandleRemoveReminder(System.TimeSpan removed)
    {
        foreach (System.TimeSpan reminder in currentEvent.reminders)
        {
            if (System.TimeSpan.Compare(reminder, removed) == 0)
            {
                currentEvent.reminders.Remove(reminder);
                break;
            }
        }

        SetRemindersTextFields();
    }

    public void HandleBackButton()
    {
        // Check for any changes
        // if changes -> confirm popup

        // Exit
        SceneManager.LoadScene(SavedEvents.lastScene); //FIXME: not always back to main view?
    }

    public void HandleConfirmButton()
    {
        // Validate event

        if (titleInputField.text == "")
        {
            print("empty name");
            return;
        }

        if (currentEvent.endDate < currentEvent.startDate)
        {
            print("invalid date");
            return;
        }

        // Output an event

        currentEvent.eventName = titleInputField.text;
        currentEvent.notes = notesInputField.text;
        print(SavedEvents.lastScene);
        if (SavedEvents.lastScene == "MainView")
        {
            SavedEvents.events.Add(currentEvent);
        }
        else if(SavedEvents.lastScene == "DayView2")
        {
            SavedEvents.events[SavedEvents.currentlyEditedEvent] = currentEvent;
        }
        SceneManager.LoadScene("MainView");
        return;
    }

    public void HandleStartTimeButton()
    {
        isEditingStartTime = true;
        // timePicker.gameObject.SetActive(true);
        // timePicker.SetupFields("Ustaw datę rozpoczęcia", currentEvent.startTime);
        scrollTimePicker.gameObject.SetActive(true);
        scrollTimePicker.SetupContent(currentEvent.startDate);
    }

    public void HandleEndTimeButton()
    {
        isEditingStartTime = false;
        // timePicker.gameObject.SetActive(true);
        // timePicker.SetupFields("Ustaw datę zakończenia", currentEvent.endTime);
        scrollTimePicker.gameObject.SetActive(true);
        scrollTimePicker.SetupContent(currentEvent.endDate);
    }

    public void HandleTimePickerConfirmButton()
    {
        if (isEditingStartTime)
        {
            // currentEvent.startTime = timePicker.GetValues();
            currentEvent.startDate = scrollTimePicker.GetValues();
        }
        else
        {
            // currentEvent.endTime = timePicker.GetValues();
            currentEvent.endDate = scrollTimePicker.GetValues();
        }

        SetTimeTextFields();
        // timePicker.gameObject.SetActive(false);
        scrollTimePicker.gameObject.SetActive(false);
    }

    public void HandleRepeatButton()
    {
        repeatPicker.gameObject.SetActive(true);
        repeatPicker.SetupFields(currentEvent.repeating);
    }

    public void HandleRepeatPickerConfirmButton()
    {
        currentEvent.repeating = repeatPicker.GetValues();
        repeatPicker.gameObject.SetActive(false);
        SetRepeatingTextField();
    }

    public void HandleColorPickButton()
    {
        colorPicker.SetActive(true);
    }

    public void HandleColorSelectButton(OurColors selectedColor)
    {
        colorPicker.SetActive(false);
        currentEvent.eventColor = selectedColor;
        colorImage.color = OurColors.GetColor(currentEvent.eventColor);
    }

    public void HandleInviteButton()
    {
        peoplePicker.SetActive(true);
    }

    int previous = 0;
    public void HandleInviteConfirmButton()
    {
        int counter = 0;
        foreach (Transform child in peoplePicker.transform)
        {
            Toggle toggle = child.gameObject.GetComponent<Toggle>();
            if (toggle && toggle.isOn)
            {
                counter += 1;
            }
        }

        foreach (Transform child in peopleList.transform)
        {
            Destroy(child.gameObject);
        }
        
        previous = counter;
        
        for (int i = 0; i < counter; i++)
        {
            var personGO = Instantiate(personPrefab, peopleList.transform);
            var text = personGO.GetComponentInChildren<Text>();
            if (text)
            {
                text.text += " " + (i+1) + "   ";
            }
        }

        Canvas.ForceUpdateCanvases();

        peoplePicker.SetActive(false);
    }

    public void debuggg()
    {
        
    }
}
