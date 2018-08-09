using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody _rigidbody;
    AudioSource _audioSource;
    [SerializeField] float _rcsPower = 100f;
    [SerializeField] float _trustPower = 100f;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
        ThrusterInput();
        RotationInput();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK"); // TODO 
                break;
            case "Fuel":
                print("Fuel"); // TODO
                break;
            default:
                print("DEAD");
                break;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    private void RotationInput()
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

    private void ThrusterInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // relative to the local coordinate system
            _rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * _trustPower);
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();

            }
        }
        else
        {
            _audioSource.Stop();
        }
    }
}
