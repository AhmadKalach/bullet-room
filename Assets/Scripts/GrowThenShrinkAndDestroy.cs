using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrowThenShrinkAndDestroy : MonoBehaviour
{
    public float growTime;
    public float shrinkStart;
    public float shrinkTime;

    Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(initialScale, growTime);
        StartCoroutine(ShrinkThenDestroy());
    }

    IEnumerator ShrinkThenDestroy()
    {
        yield return new WaitForSeconds(shrinkStart);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0, shrinkTime));
        sequence.AppendCallback(() => Destroy(this.gameObject));
    }
}
