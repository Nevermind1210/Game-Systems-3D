using System;
using System.Collections.Generic;
using UnityEngine;

namespace KeyBindings
{
    public class BindingManager : MonoBehaviour
    {
        public static bool BindingsPressed(string _key)
        {
            // Attempt to retrieve the binding
            Binding binding = GetBinding(_key);

            if(binding != null)
            {
                // We got the binding so get its pressed state
                return binding.Pressed();
            }

            // No binding matches the passed key so log a message and return false
            Debug.LogWarning("No binding matches the passed key: " + _key);
            return false;
        }

        public static bool BindingHeld(string _key)
        {
            // Attempt to retrieve the binding
            Binding binding = GetBinding(_key);

            if(binding != null)
            {
                // We got the binding so get its pressed state
                return binding.Held();
            }

            // No binding matches the passed key so log a message and return false
            Debug.LogWarning("No binding matches the passed key: " + _key);
            return false;
        }

        public static bool BindingReleased(string _key)
        {
            // Attempt to retrieve the binding
            Binding binding = GetBinding(_key);

            if(binding != null)
            {
                // We got the binding so get its pressed state
                return binding.Released();
            }

            // No binding matches the passed key so log a message and return false
            Debug.LogWarning("No binding matches the passed key: " + _key);
            return false;
        }
        
        public static void Rebind(string _name, KeyCode _value)
        {
            // Attempt to get the corresponding binding
            Binding binding = GetBinding(_name);

            if(binding != null)
            {
                // We retrieved it so rebind the key.
                binding.Rebind(_value);
            }
        }

        public static Binding GetBinding(string _key)
        {
            // First we see if the binding exists in the system.
            if(instance.bindingsMap.ContainsKey(_key))
            {
                // It does so return it.
                return instance.bindingsMap[_key];
            }

            // No binding matched this key so return null.
            return null;
        }

        private static BindingManager instance = null;

        private Dictionary<string, Binding> bindingsMap = new Dictionary<string, Binding>();

        private List<Binding> bindngsList = new List<Binding>();

        [SerializeField] private List<Binding> defaultBindings = new List<Binding>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            PopulateBindingDictionaries();
            LoadBindings();
        }
        
        private void PopulateBindingDictionaries()
        {
            // Loop through all the bindings set in the inspector
            foreach(Binding binding in defaultBindings)
            {
                // If the bindingsMap already contains a binding with this name
                // ignore this binding
                if(bindingsMap.ContainsKey(binding.Name))
                {
                    continue;
                }

                // This binding is new, so we will add it to the system
                bindingsMap.Add(binding.Name, binding);
                bindngsList.Add(binding);
            }
        }

        
        private void LoadBindings()
        {
            //self explanatory basically looking at the list and then populating it inside the inspector and game
            foreach(Binding binding in bindngsList)
            {
                binding.Load();
            }
        }
        
        public void SaveBindings()
        {
            // Same thing as above
            foreach(Binding binding in bindngsList)
            {
                binding.Save();
            }
        }
    }
}