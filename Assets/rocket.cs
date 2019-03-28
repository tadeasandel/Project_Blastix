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

    [SerializeField] AudioClip thrusteraudio;
    [SerializeField] AudioClip deathaudio;
    [SerializeField] AudioClip winaudio;

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
         if (state != State.alive) { return; }
         if (collision.gameObject.tag == "Friendly")
         {
         }
         else if (collision.gameObject.tag == "Finish")
        {
            WinTrigger();
        }
        else
        {
            DeathTrigger();
        }
    }

    private void WinTrigger()
    {
        state = State.transcending;
        audiosource.Stop();
        audiosource.PlayOneShot(winaudio);
        Invoke("LoadNextLevel", 1F);
    }

    private void DeathTrigger()
    {
        state = State.dying;
        audiosource.Stop();
        audiosource.PlayOneShot(deathaudio);
        Invoke("LoadFirstLevel", 1F);
    }

    private void LoadFirstLevel()
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
            ApplyThrust();
        }
        else
        {
            audiosource.Stop();
        }
    }

    private void ApplyThrust()
    {
        float thrustaratator = Time.deltaTime * mainthruster;
        rigidbody.AddRelativeForce(Vector3.up * thrustaratator);
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(thrusteraudio);
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
