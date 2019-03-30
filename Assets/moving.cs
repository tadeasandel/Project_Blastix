using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moving : MonoBehaviour
{

    [SerializeField] Vector3 movementvector;
    [SerializeField] float period = 2f;

    [Range(0, 1)] [SerializeField] float movementfactor;
    Vector3 startingposition;

    // Start is called before the first frame update
    void Start()
    {
        startingposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycle = Time.time / period;

        const float tau = Mathf.PI * 2f;
        float rawsinewave = Mathf.Sin(cycle*tau);
        movementfactor = rawsinewave / 2f + 0.5f;

        Vector3 offset = movementvector * movementfactor;
        transform.position = offset + startingposition;
    }
}
