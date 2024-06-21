using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("UI部分")]
    public Text TotalScoreText;
    public Text lifeText;
    public Text getCoinsText;
    public Text hitObstacleText;
    public Text highScoreText;
    public GameObject highScoreObj;

    //スコア情報
    private int totalScore;
    private int life;
    private int highScore = 0;
    private int getCoins;
    private int hitObstacle;

    private void Start()
    {
        SoundManager.instance.Sound(SoundManager.BGM.Result);

        InitializeUI();//外部変数の取得、フォーマット

        HandleHighScore();//ハイスコアの判定とテキストの表示

        SaveHighScore();
    }

    //外部変数の取得、フォーマット
    private void InitializeUI()
    {
        //変数の取得
        totalScore = ScoreManager.GetTotalScore();
        hitObstacle = ScoreManager.HitAnObstacle();
        life = ScoreManager.Life();
        getCoins = ScoreManager.GetCoins();

        //ハイスコアをロード
        highScore = PlayerPrefs.GetInt("SCORE", 0);

        //スコアのフォーマット
        TotalScoreText.text = $"{totalScore}";
        lifeText.text = $"{life}";
        getCoinsText.text = $"{getCoins}";
        hitObstacleText.text = $"{hitObstacle}";
    }

    //ハイスコアの判定とテキストの表示
    private void HandleHighScore()
    {
        //ハイスコアの場合
        if (highScore < totalScore)
        {
            highScoreObj.SetActive(true);
            highScore = totalScore;
        }
        else
        {
            highScoreObj.SetActive(false);
        }
    }
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("SCORE", highScore);
        PlayerPrefs.Save();
    }
}