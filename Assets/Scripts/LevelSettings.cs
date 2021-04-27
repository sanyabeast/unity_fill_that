using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SFXInfo {
    public AudioClip audioClip;
    public float volume;
    public float pitchMax;
    public float pitchMin;
} 

[ExecuteInEditMode]
public class LevelSettings : MonoBehaviour

{

    public string nextLevel = "";
    public Color backgroundColor = Color.white;
    public Color cellColor = Color.gray;
    public Color fillColor = Color.red;
    public Vector3 fillScale = Vector3.one;
    public float fillSpeed = 0.5f;
    public SFXInfo fillSound;
    public SFXInfo clearSound;
    public SFXInfo dropSound;
    public SFXInfo levelCompletedSound;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        // Camera.main.backgroundColor = backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayFillSound()
    {
        PlaySFX(fillSound);
    }
    public void PlayClearSound()
    {
        PlaySFX(clearSound);
    }
    public void PlayLevelCompletedSound()
    {
        PlaySFX(levelCompletedSound);

    }

    public void PlayDropSound()
    {
        PlaySFX(dropSound);

    }

    public void GoToNextLevel(){
        Debug.Log($"loading next level: {nextLevel}");
        SceneManager.LoadScene(nextLevel);
    }

    private void PlaySFX(SFXInfo sfx){
        _audioSource.pitch = Random.Range(sfx.pitchMin, sfx.pitchMax);
        _audioSource.volume = sfx.volume;
        _audioSource.PlayOneShot(sfx.audioClip);
    }
    
}
