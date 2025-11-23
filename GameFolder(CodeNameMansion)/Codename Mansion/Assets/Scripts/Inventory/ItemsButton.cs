using UnityEngine;
using System.Collections;

public class ItemsButton : MonoBehaviour
{
    public Transform OuterRing;
    public float animationDuration = 0.5f;
    private Vector3 targetScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OuterRing.localScale = Vector3.zero;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AnimateScale();
        }
    }

    public void OnClickItem()
    {
        AnimateScale();
    }

    

    public void AnimateScale()
    {
        targetScale = (OuterRing.localScale == Vector3.zero) ? Vector3.one : Vector3.zero;
        GameManager.instance.isItemMenuOn = (targetScale == Vector3.one);
        StartCoroutine(ScaleCoroutine(OuterRing.localScale, targetScale, animationDuration));
    }

    private IEnumerator ScaleCoroutine(Vector3 from, Vector3 target, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            OuterRing.localScale = Vector3.Lerp(from, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        OuterRing.localScale = target; 
    }

}
