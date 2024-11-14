using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIToggleVisibility : MonoBehaviour
    {
        [Header("UI Element")]
        [Tooltip("The UI GameObject to toggle visibility")]
        public GameObject uiElement;

        [Header("Settings")]
        [Tooltip("Set to true to make the UI visible at start")]
        public bool startVisible = true;

        private void Start()
        {
            // Set initial visibility
            SetVisibility(startVisible);
        }

        /// <summary>
        /// Toggles the visibility of the UI element.
        /// </summary>
        public void ToggleVisibility()
        {
            if (uiElement != null)
            {
                bool isActive = uiElement.activeSelf;
                SetVisibility(!isActive);
            }
        }

        /// <summary>
        /// Sets the visibility of the UI element.
        /// </summary>
        /// <param name="visible">True to make visible, false to hide</param>
        public void SetVisibility(bool visible)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(visible);
            }
        }
    }
}
