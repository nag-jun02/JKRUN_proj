using UnityEngine;
 using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject explain;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject resetScore;
    private int index;

    private Coroutine _Coroutine = new Coroutine();
    
    private string selectedScene;
    public void SelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void TitleScene()
    {
        Time.timeScale = 1;
        PlayerPrefs.DeleteKey("RandomGameScene");
        PlayerPrefs.DeleteKey("CharacterSelected");
        SceneManager.LoadScene("Title");
    }

    //�Q�[���̈ꎞ��~
    public void Pause()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
    }

    //�Q�[�����ĊJ
    public void Reunion()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
    }

    //���U���g��ʂ̃��g���C�{�^��
    public void PlayGameBtn()
    {
        index = CharacterSelection.CharacterIndex();
        selectedScene = CharacterSelection.SelectedScene();
        SceneManager.LoadScene(selectedScene);
    }

    //�Q�[���I�[�o�[���̃��g���C
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //��������p�l����\��
    public void ShowExplain()
    {
        explain.SetActive(true);
    }

    //��������p�l�����\��
    public void HideExplain()
    {
        explain.SetActive(false);
    }

    //�X�R�A�L�[�ɕۑ�����Ă���l�̍폜�AUI�̕\����\��
    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("SCORE");

        resetScore.SetActive(true);
        StartCoroutine(_Coroutine.DelayCoroutine(0.6f, () =>
        {
            resetScore.SetActive(false);
        }));
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
		    Application.Quit();

        #endif
    }
}