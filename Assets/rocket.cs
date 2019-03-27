using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audiosource;
    [SerializeField] float thruster = 100f;
    [SerializeField] float mainthruster = 100f;
    enum State {alive, dying, transcending };
    State state = State.alive;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.alive)
        {
            Thrust();
            Rotate();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        /* if (state != State.alive) { return; }
         if (collision.gameObject.tag == "Friendly")
         {
         }
         else if (collision.gameObject.tag == "Finish")
         {
             state = State.transcending;
             Invoke("LoadNextScene",1F);
         }
         else
         {
             state = State.dying;
               Invoke("DyingPhase",1F);
         }*/
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                print("Hit finish"); //todo remove
                state = State.transcending;
                Invoke("LoadNextLevel", 1f); // parameterise time
                break;
            default:
                print("Dead");
                print("Hit something deadly");
                state = State.dying;
                Invoke("LoadFirstLevel", 1f); // parameterise time
                break;
        }
    }

    private void LoadFirtLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
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
