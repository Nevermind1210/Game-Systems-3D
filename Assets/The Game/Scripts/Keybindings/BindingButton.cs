using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KeyBindings
{
    public class BindingButton : MonoBehaviour
    {
        [SerializeField] private string bindingToMap;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TextMeshProUGUI bindingName;

        private bool isRebinding = false;
        
        // We're setting up the rebinding button with the passed value whatever that is.
        public void Setup(string _toMap)
        {
            bindingToMap = _toMap;
            
            button.onClick.AddListener(OnClick);
            bindingName.text = _toMap;
            
            BindingUiltis.UpdateTextWithBinding(bindingToMap, buttonText);
            gameObject.SetActive(true);
        }

        private void Start()
        {
            // Have we set the bindingToMap variable
            if(string.IsNullOrEmpty(bindingToMap))
            {
                // We haven't so turn this gameObject off
                gameObject.SetActive(false);
                return;
            }
            Setup(bindingToMap);
        }

        private void Update()
        {
            if(isRebinding)
            {
                // Try to get any key in the input and check if it was successful
                KeyCode pressed = BindingUiltis.GetAnyPressedKey();
                if(pressed != KeyCode.None)
                {
                    // Rebind the key and update the button text
                    BindingManager.Rebind(bindingToMap, pressed);
                    BindingUiltis.UpdateTextWithBinding(bindingToMap, buttonText);

                    // Reset the isRebinding flag as we have now rebound the key
                    isRebinding = false;
                }
            }
        }
        
        private void OnClick()
        {
            isRebinding = true;
        }
    }
}