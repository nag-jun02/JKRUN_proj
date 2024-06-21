using UnityEngine;

//キャラクターとの距離を計算して、キャラについていく処理
public class Follow : MonoBehaviour
{
    // キャラを配列に入れる
    public GameObject[] characterListObj;
    int index;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        index = CharacterSelection.CharacterIndex();
        // ゲーム開始時点のカメラとターゲットの距離（オフセット）を取得
        offset = this.gameObject.transform.position - characterListObj[index].transform.position;
    }

    /// <summary>
    /// プレイヤーが移動した後にカメラが移動するようにするためにLateUpdateにする。
    /// </summary>
    void LateUpdate()
    {
        // カメラの位置をターゲットの位置にオフセットを足した場所にする。
        gameObject.transform.position = characterListObj[index].transform.position + offset;
    }
}