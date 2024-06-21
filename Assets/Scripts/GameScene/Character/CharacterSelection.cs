using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    //�v���C���[�̃��X�g(�q�I�u�W�F�N�g���Q��)
    private GameObject[] characterList;
    //�L�����N�^�[�̃C���f�b�N�X�ԍ�
    public static int index;
    //�V�[���̃C���f�b�N�X�ԍ�
    int val;

    public static string selectedScene;

    private void Start()
    {
        //�ʃV�[���Ɉ����p�����߃L�[�ɂ���
        index = PlayerPrefs.GetInt("CharacterSelected");
        characterList = new GameObject[transform.childCount];

        //�q�I�u�W�F�N�g�̃J�E���g
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        //�I������Ȃ��������\��
        foreach(GameObject go in characterList)
        {
            go.SetActive(false);
        }

        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }
    }

    //�L�����؂�ւ�
    public void Toggle(bool isRight)
    {
        //���f���̔�\��
        characterList[index].SetActive(false);

        if (isRight)
        {
            index++;
            //�Ō�̒l�Ɠ�����������0�ɂ���
            if (index == characterList.Length)
            {
                index = 0;
            }
        }
        else
        {
            index--;
            if (index < 0)
            {
                index = characterList.Length - 1;
            }
        }
        //���f���̕\��
        characterList[index].SetActive(true);
    }

    //�L������index��ۑ����āA�����_���ŃQ�[���V�[���ɑJ��
    public void ConfirmButton()
    {
        //�L�����C���f�b�N�X�̒l��ۑ�
        PlayerPrefs.SetInt("CharacterSelected", index);

        //�Q�[���V�[���Ƀ����_���őJ�ڂ���
        string[] sceneNames = { "Game", "Game1", "Game2", "Game3" };
        val = Random.Range(0, sceneNames.Length);
        selectedScene = sceneNames[val];
        //�Q�[���V�[���̒l��ۑ�
        PlayerPrefs.SetString("RandomGameScene", selectedScene);
        SceneManager.LoadScene(selectedScene);
    }

    //�L�����̃C���f�b�N�X��n��
    public static int CharacterIndex()
    {
        return index;
    }
    //�Q�[���V�[���̖��O��n��
    public static string SelectedScene()
    {
        return selectedScene;
    }
}