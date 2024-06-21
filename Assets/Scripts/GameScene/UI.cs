using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private GameObject clearUI;
    [SerializeField]
    private GameObject gameOverUI;

    [Header("ƒ‰ƒCƒtUI")]
    [SerializeField]
    private Image mainColor;
    [SerializeField]
    private Text lifeText;

    private float subFillAmount = .2f;

    public void GameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    public void ClearUI()
    {
        clearUI.SetActive(true);
    }

    public void LifeUI()
    {
        lifeText.text = ScoreManager.life.ToString();

        mainColor.fillAmount -= subFillAmount;
    }
}
