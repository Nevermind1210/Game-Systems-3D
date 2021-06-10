using System.Collections.Generic;
using UnityEngine;
using System;

namespace KeyBindings
{
    [CreateAssetMenu(fileName = "KeyBinds", menuName = "KeyBinds/KeyBinds", order = 0)]
    public class Keybinding : ScriptableObject
    {
        public List<Bind> KeyBinds;

        public bool GetKeyDown(string key)
        {
            Bind bind = KeyBinds.Find((x) => x.name == key);
            return bind != null ? Input.GetKey(bind.keyCode) : false;
        }
    
        public void ChangeKeyBind(string key, KeyCode newKeyCode)
        {
            Bind bind = KeyBinds.Find((x) => x.name == key);
            bind.keyCode = newKeyCode;
        }

        public KeyCode GetKeyPressed() // (for getting the next key that was pressed)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return keyCode;
                }
            }
            return KeyCode.None;
        }
    }
}


[System.Serializable]
public class Bind
{
    public string name;
    public KeyCode keyCode;
}