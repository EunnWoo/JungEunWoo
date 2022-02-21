using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private ParticleSystem ps;
    private float red = 0f;
    private float green = 0f;
    private float blue = 0f;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        red = Random.Range(0, 255);
        green = Random.Range(0, 255);
        blue = Random.Range(0, 255);

        var main = ps.main;
        main.startColor = new Color(red, green,blue);

    }
    void Update()
    {
        
    }
}
