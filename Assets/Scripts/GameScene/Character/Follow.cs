using UnityEngine;

//�L�����N�^�[�Ƃ̋������v�Z���āA�L�����ɂ��Ă�������
public class Follow : MonoBehaviour
{
    // �L������z��ɓ����
    public GameObject[] characterListObj;
    int index;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        index = CharacterSelection.CharacterIndex();
        // �Q�[���J�n���_�̃J�����ƃ^�[�Q�b�g�̋����i�I�t�Z�b�g�j���擾
        offset = this.gameObject.transform.position - characterListObj[index].transform.position;
    }

    /// <summary>
    /// �v���C���[���ړ�������ɃJ�������ړ�����悤�ɂ��邽�߂�LateUpdate�ɂ���B
    /// </summary>
    void LateUpdate()
    {
        // �J�����̈ʒu���^�[�Q�b�g�̈ʒu�ɃI�t�Z�b�g�𑫂����ꏊ�ɂ���B
        gameObject.transform.position = characterListObj[index].transform.position + offset;
    }
}