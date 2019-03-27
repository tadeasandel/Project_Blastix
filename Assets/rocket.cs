using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audiosource;
    [SerializeField] float thruster = 100f;
    [SerializeField] float mainthruster = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Friendly")
        {
            print("cool");
        }
        else
        {
            print("not cool scrub");
        }
        /*switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("ok");
                break;
            default:
                print("not ok");
                break;
        }*/
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustaratator = Time.deltaTime * mainthruster;
            rigidbody.AddRelativeForce(Vector3.up * thrustaratator);
            if (!audiosource.isPlaying)
            {
                audiosource.Play();
            }
        }
        else
        {
            audiosource.Stop();
        }
    }

    void Rotate()
    {
        rigidbody.freezeRotation = true;
        float rotatator = Time.deltaTime * thruster;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotatator);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotatator);
        }
        rigidbody.freezeRotation = false;
    }

}
