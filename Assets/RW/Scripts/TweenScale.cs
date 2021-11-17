using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale : MonoBehaviour
{
    public float targetScale;
    public float timeToReachTarget;
    private float startScale;
    private float percentScaled;
    public float timeToDestroy;


    void Start()
    {
        startScale = transform.localScale.x;
        Destroy(gameObject, timeToDestroy);

    }

    void Update()
    {
        if (percentScaled < 1f)
        {
            percentScaled += Time.deltaTime / timeToReachTarget;
            float scale = Mathf.Lerp(startScale, targetScale, percentScaled);
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
