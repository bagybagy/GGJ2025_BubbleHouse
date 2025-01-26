using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNotifier : MonoBehaviour
{
    public void Defeat()
    {
        Debug.Log(name + " has been defeated!");
        GameManager.Instance.NotifyBossDefeat(); // ゲームマネージャーに通知

    }
}