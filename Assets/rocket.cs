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
    [SerializeField] AudioClip onelifeaudio;
    [SerializeField] AudioClip twolifesaudio;
    [SerializeField] AudioClip threelifesaudio;

    [SerializeField] ParticleSystem thrusterparticle;
    [SerializeField] ParticleSystem deathparticle;
    [SerializeField] ParticleSystem winparticle;

    [SerializeField] float leveload = 2f;

    bool colisionmode = false;

    [SerializeField] int lifecount = 3;

    enum State {alive, dying, transcending };
    State state = State.alive;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
        LifeCounter nc = new LifeCounter();
        lifecount -= nc.lifeamount;
        nc.lifeamount = lifecount;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.alive)
        {
            Thrust();
            Rotate();
        }
        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
    }
    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            colisionmode = !colisionmode;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
         if (state != State.alive || colisionmode) { return; }
         if (collision.gameObject.tag == "Friendly")
         {

         }
         else if (collision.gameObject.tag == "Finish")
        {
            WinTrigger();
        }
        else
        {
            if (lifecount >= 1)
            {
                LifeCounter nc = new LifeCounter();
                nc.lifeamount--;
                reloadtrigger();
            }
            else
            {
                DeathTrigger();
            }
        }
    }

    private void Lifenumber()
    {
        if (lifecount == 1)
        {
            audiosource.PlayOneShot(onelifeaudio);
        }
        if (lifecount == 2)
        {
            audiosource.PlayOneShot(twolifesaudio);
        }
        if (lifecount == 3)
        {
            audiosource.PlayOneShot(threelifesaudio);
        }
    }

    private void WinTrigger()
    {
        state = State.transcending;
        audiosource.Stop();
        audiosource.PlayOneShot(winaudio);
        winparticle.Play();
        Invoke("LoadNextLevel", leveload);
    }
    private void DeathTrigger()
    {
        state = State.dying;
        audiosource.Stop();
        audiosource.PlayOneShot(deathaudio);
        deathparticle.Play();
        Invoke("LoadFirstLevel", leveload);
    }
    private void reloadtrigger()
    {
        state = State.dying;
        audiosource.Stop();
        audiosource.PlayOneShot(deathaudio);
        deathparticle.Play();
        Invoke("ReloadCurrentLevel", leveload);
    }

    private void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        int nextsceneindex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextsceneindex == SceneManager.sceneCountInBuildSettings)
        {
            nextsceneindex = 0;
        }
        SceneManager.LoadScene(nextsceneindex);
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
            thrusterparticle.Stop();
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
         thrusterparticle.Play();
    }

    void Rotate()
    {
        float rotatator = Time.deltaTime * thruster;
        if (Input.GetKey(KeyCode.A))
        {
            rotatinganodatime(rotatator);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotatinganodatime(-rotatator);
        }
    }

    private void rotatinganodatime(float rotatator)
    {
        rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotatator);
        rigidbody.freezeRotation = false;
    }
}
public class LifeCounter
{
    public LifeCounter()
    {

    }
    public int lifeamount;
}
