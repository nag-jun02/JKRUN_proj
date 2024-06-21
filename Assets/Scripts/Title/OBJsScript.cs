using System.Collections;
using UnityEngine;

public class OBJsScript : MonoBehaviour
{
    public GameObject buildingOBJ;// ��������Prefab
    public int zPos = 24;
    private bool creatingOBJ = false;

    private void Start()
    {
        SoundManager.instance.Sound(SoundManager.BGM.Title);
    }

    void FixedUpdate()
    {
        if (!creatingOBJ)
        {
            creatingOBJ = true;
            StartCoroutine(Generate());
        }
       
    }

    //DestroyOBJ�^�O�̃I�u�W�F�N�g���폜
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("DestroyOBJ"))

        {
            Destroy(collision.gameObject);
        }
    }

    //zPos��+24�����ʒu�ɐ�������
    IEnumerator Generate()
    {
        Instantiate(buildingOBJ, new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 24;
        yield return new WaitForSeconds(3);
        creatingOBJ = false;
    }
}