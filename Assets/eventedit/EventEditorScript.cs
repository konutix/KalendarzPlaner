using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventEditorScript : MonoBehaviour
{   
    public InputField titleInputField;

    public TimePicker timePicker;
    public Text timeStartText;
    public Text timeEndText;

    public RepeatPicker repeatPicker;
    public Text repeatingText;

    public Transform remindersContainer;
    public GameObject reminderPrefab;

    public GameObject colorPicker;
    public Image colorImage;

    public InputField notesInputField;

    public GameObject peoplePicker;


    public enum RepeatingType 
    {
        None, Daily, Weekly, Monthly, Yearly
    }

    public class Repeating
    {
        public RepeatingType type = RepeatingType.None;
        public int amount = 1;
    }

    class CallendarEvent 
    {
        public string name;
        public Color color = Color.blue;

        public System.DateTime startTime;
        public System.DateTime endTime;
        public bool isAllDay = false;

        public Repeating repeating;

        public List<System.TimeSpan> reminders;

        public string notes; //todo: handle checkboxes?

        // public List<invited people> invitedPeople;
    }

    CallendarEvent currentEvent;
    bool isEditingStartTime = true;

    void Start()
    {
        // Load event if edit, create new if new
        currentEvent = new CallendarEvent();
        currentEvent.startTime = System.DateTime.Now;
        System.TimeSpan oneHour = System.TimeSpan.FromHours(1);
        currentEvent.endTime = currentEvent.startTime.Add(oneHour);
        SetTimeTextFields();

        currentEvent.repeating = new Repeating();
        SetRepeatingTextField();

        System.TimeSpan tenMinutes = System.TimeSpan.FromMinutes(10);
        currentEvent.reminders = new List<System.TimeSpan>();
        currentEvent.reminders.Add(tenMinutes);
        SetRemindersTextFields();

        colorImage.color = currentEvent.color;

    }

    void SetTimeTextFields()
    {
        timeStartText.text = currentEvent.startTime.ToString("dd.MM.yy      HH:mm  ");
        timeEndText.text = currentEvent.endTime.ToString("dd.MM.yy      HH:mm  ");
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
    }
    int temporaryMinuteSelectLol = 20;
    public void HandleAddReminderButton()
    {
        currentEvent.reminders.Add(System.TimeSpan.FromMinutes(temporaryMinuteSelectLol));
        temporaryMinuteSelectLol += 10;
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
    }

    public void HandleConfirmButton()
    {
        // Validate event

        if (titleInputField.text == "")
        {
            print("empty name");
            return;
        }

        if (currentEvent.endTime < currentEvent.startTime)
        {
            print("invalid date");
            return;
        }

        // Output an event

        currentEvent.name = titleInputField.text;

        currentEvent.notes = notesInputField.text;


        print(currentEvent.name);
        print(currentEvent.startTime);
        print(currentEvent.endTime);
        print(currentEvent.color);
        print(currentEvent.notes);
    }

    public void HandleStartTimeButton()
    {
        isEditingStartTime = true;
        timePicker.gameObject.SetActive(true);
        timePicker.SetupFields("Ustaw datę rozpoczęcia", currentEvent.startTime);
    }

    public void HandleEndTimeButton()
    {
        isEditingStartTime = false;
        timePicker.gameObject.SetActive(true);
        timePicker.SetupFields("Ustaw datę zakończenia", currentEvent.endTime);
    }

    public void HandleTimePickerConfirmButton()
    {
        if (isEditingStartTime)
        {
            currentEvent.startTime = timePicker.GetValues();
        }
        else
        {
            currentEvent.endTime = timePicker.GetValues();
        }

        SetTimeTextFields();
        timePicker.gameObject.SetActive(false);
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

    public void HandleColorSelectButton(Color selectedColor)
    {
        colorPicker.SetActive(false);
        currentEvent.color = selectedColor;
        colorImage.color = currentEvent.color;
    }

    public void HandleInviteButton()
    {
        peoplePicker.SetActive(!peoplePicker.activeSelf);
    }

    public GameObject betterTimePicker;
    public void debuggg()
    {
        betterTimePicker.SetActive(!betterTimePicker.activeSelf);
    }
}
