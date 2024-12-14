using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    [Header("UI Reference")]
    [Tooltip("Drag the UI GameObject (Image or Panel) here")]
    public GameObject uiElement;

    // Method to toggle the UI element on and off
    public void ToggleUIElement()
    {
        if (uiElement == null)
        {
            Debug.LogWarning("UI element reference is not assigned!");
            return;
        }

        // Toggle the active state of the UI element
        uiElement.SetActive(!uiElement.activeSelf);
    }
}
