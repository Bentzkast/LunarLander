using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    Rigidbody _rigidbody;
    AudioSource _audioSource;
    [SerializeField] float _rcsPower = 100f;
    [SerializeField] float _trustPower = 100f;
    [SerializeField] AudioClip _clipThrust;
    [SerializeField] AudioClip _clipDie;
    [SerializeField] AudioClip _clipFinish;

    [SerializeField] ParticleSystem _particleThrust;
    [SerializeField] ParticleSystem _particleDie;
    [SerializeField] ParticleSystem _particleFinish;


    enum State
    {
        Alive, Dying, Transcending
    }

    State state = State.Alive;
	// Use this for initialization
	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        state = State.Alive;
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update () {

        if (state == State.Alive)
        {
            RespondToThrusterInput();

            RespondToRotationInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state == State.Dying) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK"); // TODO 
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartDeadSequence();
                break;
        }

    }

    private void StartDeadSequence()
    {
        state = State.Dying;
        _particleDie.Play();
        _audioSource.Stop();
        _audioSource.PlayOneShot(_clipDie);
        Invoke("LoadFirstScene", 1f);
    }

    private void StartFinishSequence()
    {
        print("Next Level!");
        state = State.Transcending;
        _particleFinish.Play();
        _audioSource.Stop();

        _audioSource.PlayOneShot(_clipFinish);
        Invoke("LoadNextScene", 1.5f); // parameterize time
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); // todo allow more than 2 lvl 
    }


    private void RespondToRotationInput()
    {
        _rigidbody.freezeRotation = true;// better manual rotation handling     
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * _rcsPower);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * Time.deltaTime * _rcsPower);
        }

        _rigidbody.freezeRotation = false; // let the physics engine take control again
    }

    private void RespondToThrusterInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            _particleThrust.Stop();
            _audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        _rigidbody.AddRelativeForce(Vector3.up * _trustPower); // relative to the local coordinate system
        _particleThrust.Play();
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_clipThrust);
        }
    }
}
