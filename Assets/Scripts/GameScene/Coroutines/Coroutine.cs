using System;
using System.Collections;
using UnityEngine;

public class Coroutine
{
    // 一定時間後に処理を呼び出すコルーチン 
    public IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
