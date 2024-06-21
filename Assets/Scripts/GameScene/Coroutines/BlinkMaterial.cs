using System.Collections;
using UnityEngine;

// プレイヤーオブジェクトにアタッチ

public class BlinkMaterial : MonoBehaviour
{
    [Header("点滅させるマテリアル")]
    [SerializeField]
    private Material[] blinkMaterials;
    private float blinkDuration = .3f;
    private int blinkCount = 3;

    public IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            // アルファ値を0にして点滅開始
            SetAllMaterialsAlpha(0);
            yield return new WaitForSeconds(blinkDuration / 2f);
            // アルファ値を1にして点滅終了
            SetAllMaterialsAlpha(1);
            yield return new WaitForSeconds(blinkDuration / 2f);
        }
    }

    private void SetAllMaterialsAlpha(float alpha)
    {
        foreach (Material material in blinkMaterials)
        {
            Color color = material.color;
            color.a = alpha;
            material.color = color;
        }
    }
}
