using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private bool isPaused = false;
    public void OnPause(InputAction.CallbackContext context)
    {
        // Started のときだけ PauseManager を呼び出す
        if (context.started)
        {
            isPaused = !isPaused;
            //bossの足関係がバグるため、TimeScaleは弄るのを一時中止
            //Time.timeScale = isPaused ? 0 : 1;

            UIManager.Instance.SetPauseUI(isPaused);
        }
    }
}
