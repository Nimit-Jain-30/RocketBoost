using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    
    [SerializeField] AudioClip crashSfx;
    [SerializeField] AudioClip successSfx;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;
    bool isControllable = true;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter(Collision other) {
        if (!isControllable) {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Launch Pad"); 
                break;
            case "Finish":
                Debug.Log("Finish!!!");
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence(){
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSfx);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("Reload",delay);
    }
    void StartSuccessSequence(){
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSfx);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",delay);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings){
            nextScene = 1;
        }
            SceneManager.LoadScene(nextScene);
    }

    void Reload(){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
