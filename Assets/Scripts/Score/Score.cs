using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("UI����")]
    public Text TotalScoreText;
    public Text lifeText;
    public Text getCoinsText;
    public Text hitObstacleText;
    public Text highScoreText;
    public GameObject highScoreObj;

    //�X�R�A���
    private int totalScore;
    private int life;
    private int highScore = 0;
    private int getCoins;
    private int hitObstacle;

    private void Start()
    {
        SoundManager.instance.Sound(SoundManager.BGM.Result);

        InitializeUI();//�O���ϐ��̎擾�A�t�H�[�}�b�g

        HandleHighScore();//�n�C�X�R�A�̔���ƃe�L�X�g�̕\��

        SaveHighScore();
    }

    //�O���ϐ��̎擾�A�t�H�[�}�b�g
    private void InitializeUI()
    {
        //�ϐ��̎擾
        totalScore = ScoreManager.GetTotalScore();
        hitObstacle = ScoreManager.HitAnObstacle();
        life = ScoreManager.Life();
        getCoins = ScoreManager.GetCoins();

        //�n�C�X�R�A�����[�h
        highScore = PlayerPrefs.GetInt("SCORE", 0);

        //�X�R�A�̃t�H�[�}�b�g
        TotalScoreText.text = $"{totalScore}";
        lifeText.text = $"{life}";
        getCoinsText.text = $"{getCoins}";
        hitObstacleText.text = $"{hitObstacle}";
    }

    //�n�C�X�R�A�̔���ƃe�L�X�g�̕\��
    private void HandleHighScore()
    {
        //�n�C�X�R�A�̏ꍇ
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