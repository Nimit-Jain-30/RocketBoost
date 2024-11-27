using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;
    [SerializeField] ParticleSystem leftThrustParticle;

    AudioSource audioSource;
    Rigidbody rb;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0)
        {
            rightThrustParticle.Play();
            ApplyRotation(rotationStrength);
        }
        else if(rotationInput > 0){
            leftThrustParticle.Play();
            ApplyRotation(-rotationStrength);
        }
    }

    private void ApplyRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

    private void Thrust()
    {
        if (thrust.IsPressed() == true)
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            mainEngineThrustParticle.Play();
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else{
            audioSource.Stop();
        }
        
    }
}
