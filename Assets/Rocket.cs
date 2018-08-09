using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody _rigidbody;
    AudioSource _audioSource;
    int rcsPower = 100;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
    }

    /// <summary>
    /// 
    /// </summary>
    private void ProcessInput()
    {

        if(Input.GetKey(KeyCode.Space))
        {
            // relative to the local coordinate system
            _rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * 100);
            if(!_audioSource.isPlaying)
            {
                _audioSource.Play();

            }
        }
        else
        {
            _audioSource.Stop();
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rcsPower);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * Time.deltaTime * rcsPower);
        }
    }
}
