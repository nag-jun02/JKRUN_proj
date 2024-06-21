using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    //プレイヤーのリスト(子オブジェクトを参照)
    private GameObject[] characterList;
    //キャラクターのインデックス番号
    public static int index;
    //シーンのインデックス番号
    int val;

    public static string selectedScene;

    private void Start()
    {
        //別シーンに引き継ぐためキーにする
        index = PlayerPrefs.GetInt("CharacterSelected");
        characterList = new GameObject[transform.childCount];

        //子オブジェクトのカウント
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        //選択されなかったら非表示
        foreach(GameObject go in characterList)
        {
            go.SetActive(false);
        }

        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }
    }

    //キャラ切り替え
    public void Toggle(bool isRight)
    {
        //モデルの非表示
        characterList[index].SetActive(false);

        if (isRight)
        {
            index++;
            //最後の値と同じだったら0にする
            if (index == characterList.Length)
            {
                index = 0;
            }
        }
        else
        {
            index--;
            if (index < 0)
            {
                index = characterList.Length - 1;
            }
        }
        //モデルの表示
        characterList[index].SetActive(true);
    }

    //キャラのindexを保存して、ランダムでゲームシーンに遷移
    public void ConfirmButton()
    {
        //キャラインデックスの値を保存
        PlayerPrefs.SetInt("CharacterSelected", index);

        //ゲームシーンにランダムで遷移する
        string[] sceneNames = { "Game", "Game1", "Game2", "Game3" };
        val = Random.Range(0, sceneNames.Length);
        selectedScene = sceneNames[val];
        //ゲームシーンの値を保存
        PlayerPrefs.SetString("RandomGameScene", selectedScene);
        SceneManager.LoadScene(selectedScene);
    }

    //キャラのインデックスを渡す
    public static int CharacterIndex()
    {
        return index;
    }
    //ゲームシーンの名前を渡す
    public static string SelectedScene()
    {
        return selectedScene;
    }
}