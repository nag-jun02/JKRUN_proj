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

    //ゲームの一時停止
    public void Pause()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
    }

    //ゲームを再開
    public void Reunion()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
    }

    //リザルト画面のリトライボタン
    public void PlayGameBtn()
    {
        index = CharacterSelection.CharacterIndex();
        selectedScene = CharacterSelection.SelectedScene();
        SceneManager.LoadScene(selectedScene);
    }

    //ゲームオーバー時のリトライ
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //操作説明パネルを表示
    public void ShowExplain()
    {
        explain.SetActive(true);
    }

    //操作説明パネルを非表示
    public void HideExplain()
    {
        explain.SetActive(false);
    }

    //スコアキーに保存されている値の削除、UIの表示非表示
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