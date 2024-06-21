using System;
using System.Collections;
using UnityEngine;

public class Coroutine
{
    // ��莞�Ԍ�ɏ������Ăяo���R���[�`�� 
    public IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
