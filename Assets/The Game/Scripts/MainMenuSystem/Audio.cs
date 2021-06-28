using UnityEngine;

namespace The_Game.Scripts.MainMenuSystem
{
    public class Audio : MonoBehaviour
    {
        [SerializeField] private AudioSource click;

        public void PlayOnClick()
        {
            click.Play();
        }
    }
}