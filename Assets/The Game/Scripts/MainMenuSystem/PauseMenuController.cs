using System;
using UnityEngine;

namespace The_Game.Scripts.MainMenuSystem
{
    public class PauseMenuController : MonoBehaviour
    {
        [Header("Pause Panel")] 
        [SerializeField] private GameObject pausePanel;
        private bool amIPaused;

        private void Start()
        {
            pausePanel.SetActive(false);
            amIPaused = false;
            Time.timeScale = 1;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !amIPaused)
            {
                PauseGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && amIPaused)
            {
                UnPauseGame();
            }
        }

        private void PauseGame()
        {
            amIPaused = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void UnPauseGame()
        {
            amIPaused = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
}