using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private bool isPaused = false;
    public void OnPause(InputAction.CallbackContext context)
    {
        // Started �̂Ƃ����� PauseManager ���Ăяo��
        if (context.started)
        {
            isPaused = !isPaused;
            //boss�̑��֌W���o�O�邽�߁ATimeScale�͘M��̂��ꎞ���~
            //Time.timeScale = isPaused ? 0 : 1;

            UIManager.Instance.SetPauseUI(isPaused);
        }
    }
}
