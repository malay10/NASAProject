using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManagerScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //can Detect
        if(Input.GetKeyDown(KeyCode.N))
        {

            if (audioClips.Length > index && !audioSource.isPlaying)
            {
                Debug.Log("Playing Clip " + index);
                StartCoroutine(AudioInstruction(index++));
                //index++;
            }
            else
            {
                Debug.Log("Can't Do that, audio playing or no more clips");
            }
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
