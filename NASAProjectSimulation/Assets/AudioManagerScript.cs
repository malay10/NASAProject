using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    private int index = -1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            if (audioClips[index] && !audioSource.isPlaying)
                StartCoroutine(AudioInstruction(index++));
            //index++;
        }
    }

    IEnumerator AudioInstruction(int index)
    {
        if (audioClips[index])
        {
            audioSource.PlayOneShot(audioClips[index], 0.9f);//can scale audio
            yield return new WaitForSeconds(audioClips[index].length);
        }
        else
        {
            Debug.Log("No more Clips");
            yield return new WaitForEndOfFrame();

        }
    }
}
