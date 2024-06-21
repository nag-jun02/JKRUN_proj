using System.Collections;
using UnityEngine;

// �v���C���[�I�u�W�F�N�g�ɃA�^�b�`

public class BlinkMaterial : MonoBehaviour
{
    [Header("�_�ł�����}�e���A��")]
    [SerializeField]
    private Material[] blinkMaterials;
    private float blinkDuration = .3f;
    private int blinkCount = 3;

    public IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            // �A���t�@�l��0�ɂ��ē_�ŊJ�n
            SetAllMaterialsAlpha(0);
            yield return new WaitForSeconds(blinkDuration / 2f);
            // �A���t�@�l��1�ɂ��ē_�ŏI��
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
