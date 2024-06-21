public class ScoreManager
{
    public static int totalScore;
    public static int life;
    public static int hitObstacle;// 障害物に当たった数
    public static int score;
    public static int getCoins;// コインの獲得枚数

    private const int DamageScore = 70;
    private const int addCoinScore = 50;

    private static int totalDamageScore;
    private static int totalCoinScore;

    public ScoreManager()
    {
        totalScore = 0;
        life = 5;
        score = 0;
        hitObstacle = 0;
        getCoins = 0;
        totalDamageScore = 0;
        totalCoinScore = 0;
    }

    // ------------------------
    //
    //  スコアの加算・減算・結果
    //
    // ------------------------

    public void HitObstacle()
    {
        life--;
        hitObstacle++;
        totalDamageScore -= DamageScore;
    }

    public void GetCoin()
    {
        getCoins++;
        totalCoinScore += addCoinScore;
    }

    public void CalculateScore()
    {
        score = totalCoinScore + totalDamageScore;
        totalScore = score * life;
    }


    // ----------------------------------------
    //
    // 　値をリザルト画面のスクリプトに渡す
    //
    // ----------------------------------------

    public static int Life()
    {
        return life;
    }

    public static int HitAnObstacle()
    {
        return hitObstacle;
    }

    public static int GetTotalScore()
    {
        return totalScore;
    }

    public static int GetCoins()
    {
        return getCoins;
    }
}
