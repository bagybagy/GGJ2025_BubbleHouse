using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private bool isPaused = false;
    public void OnPause(InputAction.CallbackContext context)
    {
        // Started ‚Ì‚Æ‚«‚¾‚¯ PauseManager ‚ğŒÄ‚Ño‚·
        if (context.started)
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;

            UIManager.Instance.SetPauseUI(isPaused);
        }
    }
}
