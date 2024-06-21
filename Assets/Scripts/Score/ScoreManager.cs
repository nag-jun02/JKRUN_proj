public class ScoreManager
{
    public static int totalScore;
    public static int life;
    public static int hitObstacle;// ��Q���ɓ���������
    public static int score;
    public static int getCoins;// �R�C���̊l������

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
    //  �X�R�A�̉��Z�E���Z�E����
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
    // �@�l�����U���g��ʂ̃X�N���v�g�ɓn��
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
