using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialougue : MonoBehaviour
{
    public string greeting;
    public string faction;
    public LineOfDialouge goodbye;
    public LineOfDialouge[] dialougeOptions;

    public bool firstDialogue;

    private void Update()
    {
        if (!firstDialogue) return;
        if(Input.GetKeyDown(KeyCode.E))
        {
            DialougeManager.theManager.LoadDialouge(this);
        }
    }
}
