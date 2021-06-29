using System;
using UnityEngine;

namespace KeyBindings
{
    [Serializable]
    public class Binding
    {
        public string Name
        {
            get { return name; }
        }

        public KeyCode Value
        {
            get { return value; }
        }
        
        public string ValueDisplay
        {
            get { return BindingUiltis.TranslateKeycode(value); }
        }

        [SerializeField] private string name;
        [SerializeField] private KeyCode value;

        public Binding(string _name, KeyCode _defaultValue)
        {
            name = _name;
            value = _defaultValue;
        }
            
        // Saves any value that get inputted inside the PlayerPrefs so it gets remembered and is persistent between sessions
        public void Save()
        {
            PlayerPrefs.SetInt(name, (int) value);
            PlayerPrefs.Save();
        }

        // Loads the stored value of this keybinding, if it is not set, then use default values. 
        public void Load()
        {
            // We make value into the Key thats saved inside the PPrefs
            value = (KeyCode)PlayerPrefs.GetInt(name, (int)value);
        }
        
        // Rebinds the binding to the new keybinding the then saves to the player prefs.
        public void Rebind(KeyCode _new)
        {
            value = _new;
            Save();
        }
        
        // Returns weather or not the key this binding is mapped to was pressed this frame.
        public bool Pressed()
        {
            return Input.GetKeyDown(value);
        }

        // Returns weather or not the key this binding is mapped is being pressed this frame.
        public bool Held()
        {
            return Input.GetKey(value);
        }
        
        // Returns weather or not the key binding is mapped was released this frame.
        public bool Released()
        {
            return Input.GetKeyUp(value);
        }
    }
}