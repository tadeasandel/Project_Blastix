using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audiosource;
    public int rotatator = 10;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up*Time.deltaTime);
            if (!audiosource.isPlaying)
            {
                audiosource.Play();
            }
        }
        else
        {
            audiosource.Stop();
        }


        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward/* * Time.deltaTime*/);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back /* Time.deltaTime*/);
        }
    }
}
