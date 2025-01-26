using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Xml.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    GameObject clearUI;
    [SerializeField]
    GameObject gameOverUI;
    [SerializeField]
    Image hpGage;
    [SerializeField]
    Image expGage;
    [SerializeField]
    private TMP_Text expText;

    private void Awake()
    {
        // Awakeはオブジェクトが生成されるときに呼ばれるメソッドです
        // ここでシングルトンパターンを実現しています

        // すでにGameManagerのインスタンスが存在している場合、現在のオブジェクトは破棄します
        if (Instance != null && Instance != this)
        {
            // 他のインスタンスが存在する場合、重複して作らないようにオブジェクトを破棄
            Destroy(gameObject);
        }
        else
        {
            // GameManagerのインスタンスを管理するため、最初に作成されたインスタンスを保持します
            Instance = this;
        }
    }

    public void ActiveClearUI()
    {
        //ckearUIが存在すれば有効化、無ければエラー表示
        if (clearUI != null)
        {
            clearUI.SetActive(true);
        }
        else
        {
            Debug.Log("クリアUIが見つかりません");
        }
    }

    public void LoadGameScene()
    {
        SceneLoader.Instance.LoadSceneWithFade("StageScene");
    }

    // HPバーの更新メソッド
    public void UpdateHPBar(float currentHP, float maxHP)
    {
        // HPバーのFillAmountを更新
        hpGage.fillAmount = currentHP / maxHP;
        Debug.Log(hpGage.fillAmount);
    }

    public void UpdateExpBar(float currentExp, float borderExp)
    {
        // ExpバーのFillAmountを更新
        expGage.fillAmount = currentExp / borderExp;

        // 現在のレベル
        expText.text = $"{(int)currentExp}";
    }

    public void GameOver()
    {
        // しんぐるとん化している影響でカメラリセットが必要
        CameraManager.Instance.CameraReset();

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.Log("gameOverUIが見つかりません");
        }
        
    }

}
