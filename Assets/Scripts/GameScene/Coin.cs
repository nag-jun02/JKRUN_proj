using UnityEngine;
public class Coin : MonoBehaviour
{
    // �A�j���[�V�����̑�����
    private readonly float speed = 90f;
    // �l���ς݂��ǂ���
    bool isGet;
    // �l����̑؍ݎ���
    float lifeTime = 0.3f;

    //Player�^�O�ɓ���������
    private void OnTriggerEnter(Collider other)
    {
        if (!isGet && other.CompareTag("Player"))
        {
            isGet = true;
            transform.position += Vector3.up * 1.0f;
            GetComponent<AudioSource>().Play();
        }
    }

    //�R�C���̃A�j���[�V����
    void Update()
    {
        // �R�C�����l��������
        if (isGet)
        {
            // �R�C����f������]
            RotateCoin(speed*10f);

            lifeTime -= Time.deltaTime;// �؍ݎ��Ԃ����炷
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // �R�C������������]
            RotateCoin(speed);
        }
    }
    //�R�C���̉�]
    private void RotateCoin(float rotationSpeed)
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up, Space.World);
    }
}