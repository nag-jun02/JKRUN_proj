using System.Collections;
using UnityEngine;

public class OBJsScript : MonoBehaviour
{
    public GameObject buildingOBJ;// 生成するPrefab
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

    //DestroyOBJタグのオブジェクトを削除
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("DestroyOBJ"))

        {
            Destroy(collision.gameObject);
        }
    }

    //zPosを+24した位置に生成する
    IEnumerator Generate()
    {
        Instantiate(buildingOBJ, new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 24;
        yield return new WaitForSeconds(3);
        creatingOBJ = false;
    }
}