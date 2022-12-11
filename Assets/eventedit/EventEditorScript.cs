using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventEditorScript : MonoBehaviour
{   
    public InputField titleInputField;

    public TimePicker timePicker;

    public GameObject colorPicker;
    public Image colorImage;

    public InputField notesInputField;

    public GameObject peoplePicker;


    class CallendarEvent 
    {
        public string name;
        public System.DateTime startTime;
        public System.DateTime endTime;
        public Color color = Color.blue;
        public string notes; //todo: handle checkboxes?
        // public List<invited people> invitedPeople;

        // --- meta data ---
        public bool isNewEvent = true;
        public bool hasChanged = true;
    }

    CallendarEvent currentEvent;
    bool isEditingStartTime = true;

    void Start()
    {
        // Load event if edit, create new if new
        currentEvent = new CallendarEvent();
        currentEvent.startTime = System.DateTime.Now;
        System.TimeSpan oneHour = new System.TimeSpan(1, 0, 0);
        currentEvent.endTime = currentEvent.startTime.Add(oneHour);

        colorImage.color = currentEvent.color;

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

        timePicker.gameObject.SetActive(false);
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
}
