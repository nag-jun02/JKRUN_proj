using UnityEngine;
public class Coin : MonoBehaviour
{
    // アニメーションの総時間
    private readonly float speed = 90f;
    // 獲得済みかどうか
    bool isGet;
    // 獲得後の滞在時間
    float lifeTime = 0.3f;

    //Playerタグに当たったら
    private void OnTriggerEnter(Collider other)
    {
        if (!isGet && other.CompareTag("Player"))
        {
            isGet = true;
            transform.position += Vector3.up * 1.0f;
            GetComponent<AudioSource>().Play();
        }
    }

    //コインのアニメーション
    void Update()
    {
        // コインを獲得したら
        if (isGet)
        {
            // コインを素早く回転
            RotateCoin(speed*10f);

            lifeTime -= Time.deltaTime;// 滞在時間を減らす
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // コインをゆっくり回転
            RotateCoin(speed);
        }
    }
    //コインの回転
    private void RotateCoin(float rotationSpeed)
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up, Space.World);
    }
}