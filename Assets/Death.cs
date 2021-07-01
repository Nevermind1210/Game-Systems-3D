using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    void Respawn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
