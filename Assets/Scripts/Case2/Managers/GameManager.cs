using Helper;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Case2.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Player player;
        public UnityEvent startEvent, winEvent, loseEvent;
        private bool isEnded;

        public void StartEventHandle()
        {
            isEnded=false;
            startEvent.Invoke();
        }
        public void WinEventHandle()
        {
            if(isEnded)return;
            isEnded=true;
            winEvent.Invoke();
        }

        public void LoseEventHandle()
        {
            if(isEnded)return;
            isEnded=true;
            loseEvent.Invoke();
        }

        public void RestartScene(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
