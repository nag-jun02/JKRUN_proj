using System.Diagnostics;
using UnityEngine;

public class AppearObstacles : MonoBehaviour
{

    Animator animator;
    int hash = Animator.StringToHash("ApWall");
    int hash2 = Animator.StringToHash("ApFloor");

    public GameObject targetObject; // アニメーションを再生する対象のゲームオブジェクト
    public int num;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // プレイヤーがトリガーに触れた場合（タグが異なる場合は適切に変更）
        {
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        animator = targetObject.GetComponent<Animator>();
        switch (num)
        {
            case 1:
                // アニメーションを再生
                animator.SetTrigger(hash); // アニメーションの名前に適切なものを指定
                break;
            case 2:
                animator.SetTrigger(hash2);
                break;
        }




    }
}
