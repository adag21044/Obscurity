using UnityEngine;
using DentedPixel;

public class EnemyBar : MonoBehaviour
{
    public GameObject bar;
    public int time; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AnimateBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, time);
    }
}
