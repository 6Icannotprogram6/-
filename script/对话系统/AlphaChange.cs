using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChange : MonoBehaviour
{
    public CanvasGroup Group;
    [SerializeField]private AnimationCurve Curve=AnimationCurve.EaseInOut(0,0,1,1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Coroutine AlpahCoroutine;
    //                           目标alpha  显隐持续时间     是否自调用
    public void ChangeAlpha(float copacity, float duration, Action act)
    {
        if (duration <= 0)
        {
            Group.alpha = copacity;
            act?.Invoke();
        }
        else
        {
            if (AlpahCoroutine != null)
            {
                StopCoroutine(AlpahCoroutine);
            }
            AlpahCoroutine = StartCoroutine(alphaChange(copacity, duration, act));
        }
    }
    public IEnumerator alphaChange(float copacity, float duration, Action act)
    {
        float timer = 0;
        float start = Group.alpha;
        while (timer < duration)
        {
            timer = Mathf.Min(duration, timer + Time.unscaledDeltaTime);
            Group.alpha = Mathf.Lerp(start, copacity, Curve.Evaluate(timer / duration));
            yield return null;
        }

        act?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
