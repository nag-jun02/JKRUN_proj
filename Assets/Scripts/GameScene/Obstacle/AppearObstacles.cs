using System.Diagnostics;
using UnityEngine;

public class AppearObstacles : MonoBehaviour
{

    Animator animator;
    int hash = Animator.StringToHash("ApWall");
    int hash2 = Animator.StringToHash("ApFloor");

    public GameObject targetObject; // �A�j���[�V�������Đ�����Ώۂ̃Q�[���I�u�W�F�N�g
    public int num;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �v���C���[���g���K�[�ɐG�ꂽ�ꍇ�i�^�O���قȂ�ꍇ�͓K�؂ɕύX�j
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
                // �A�j���[�V�������Đ�
                animator.SetTrigger(hash); // �A�j���[�V�����̖��O�ɓK�؂Ȃ��̂��w��
                break;
            case 2:
                animator.SetTrigger(hash2);
                break;
        }




    }
}
