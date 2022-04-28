using Helper;
using UnityEngine.Events;

namespace Case2.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Player player;
        public UnityEvent startEvent, winEvent, loseEvent;

        public void StartEventHandle()
        {
            startEvent.Invoke();
        }
        public void WinEventHandle()
        {
            winEvent.Invoke();
        }

        public void LoseEventHandle()
        {
            loseEvent.Invoke();
        }
    }
}
