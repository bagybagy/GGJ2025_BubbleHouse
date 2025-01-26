using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




public class StatusManager : MonoBehaviour
{
    [SerializeField] GameObject MainObject; //このスクリプトをアタッチするオブジェクト
    [SerializeField] int hp = 1;             //hp現在値
    [SerializeField] int maxHp = 1;          //いずれmaxHp利用する際に使用

    [SerializeField] int attackDamage = 10; // このオブジェクトの攻撃力

    [SerializeField] GameObject destroyEffect;  //撃破エフェクト
    [SerializeField] GameObject damageEffect;   //被弾エフェクト

    float expRate = 1f;     // 初期EXP = Lv
    float healRate = 0.05f; // アイテム取得時の回復率5％

    // 読み取り専用のプロパティ
    public float ExpRate
    {
        get { return expRate; }
    }

    // Update is called once per frame
    void Update()
    {
        //hpが0以下なら、撃破エフェクトを生成してMainを破壊
        if (hp <= 0)
        {
            DestoryMainObject();
        }
    }

    // 被ダメージ時の処理
    public void Damage(int baseDamage, Vector3 attackPoint)
    {
        Debug.Log("damage");
        float finalDamage;  // 計算後の最終ダメージ

        // ダメージ計算を専用関数化
        DamageCalc(baseDamage, out finalDamage);

        // HPを減少
        hp -= Mathf.RoundToInt(finalDamage);

        var effect = Instantiate(damageEffect);     // ダメージエフェクトの生成
        effect.transform.position = attackPoint; // ダメージエフェクトの生成場所の指定

        HPGageUpdateUI();

    }

    private void HPGageUpdateUI()
    {
        // UIManager にHP更新を通知
        if (this.gameObject.CompareTag("Player")) // プレイヤーの場合にのみUI更新
        {
            UIManager.Instance.UpdateHPBar(hp, maxHp);
        }
    }

    private void DamageCalc(int baseDamage,out float finalDamage)
    {
        // ランダム補正を計算（-20%～+20%の範囲）
        float randomFactor = Random.Range(0.8f, 1.2f);
        finalDamage = baseDamage * randomFactor;
    }

    // アタッチされたオブジェクトが破壊される際の処理
    private void DestoryMainObject()
    {
        hp = 0;
        var effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
        Destroy(effect, 5);

        // DropTableを取得してアイテムドロップ
        DropTable dropTable = GetComponentInChildren<DropTable>();
        if (dropTable != null)
        {
            dropTable.DropItems();
        }

        Destroy(MainObject);
    }

    // 攻撃力を返す関数
    public int GetDamageAmount()
    {
        return attackDamage;
    }

    public void IncreaseExp(float amount)
    {
        expRate += amount;
        HPHeal();
        expGageUpdateUI();

        // ログ (デバッグ用)
        Debug.Log($"Expが {amount} 増加しました。現在のExp: {expRate}");
    }

    private void HPHeal()
    { 
        // HP回復用関数
        if (hp < maxHp)
        {
            hp += (int)(maxHp * healRate);
            HPGageUpdateUI();
        }
    }

    private void expGageUpdateUI()
    {
        // UIManager にexp更新を通知
        if (this.gameObject.CompareTag("Player")) // プレイヤーの場合にのみUI更新
        {
            UIManager.Instance.UpdateExpBar(expRate, 10f);  // 今は仮にマジックナンバー10f、今後Critボーダー超える度に更新したい
        }
    }

}
