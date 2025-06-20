
using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class PlayVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip videoClip;
    // Check if video is currently playing
    bool isPlaying;
    public GameObject screen;

    void Start()
    {
        screen.SetActive(false);
        // Get VideoPlayer component if not assigned
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        // Setup video
        if (videoPlayer != null && videoClip != null)
        {
            videoPlayer.clip = videoClip;
            videoPlayer.playOnAwake = false;
        }
        // Subscribe to the event that fires when video reaches the end
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video finished playing!");
        // Do whatever you want when video ends
        HideAllParents(); //hides the gameObject the video is attached to
        OnDestroy();
        GameManager.Instance.LoadScene();
        GameManager.Instance.FadeOutSceneTransition();
    }

    void HideAllParents()
    {
        //gets current gameObject
        Transform current = transform;

        // Start from this gameObject and go up the hierarchy
        while (current != null)
        {
            Debug.Log($"Hiding: {current.name}");
            current.gameObject.SetActive(false);
            current = current.parent;
        }
    }

    void Update()
    {
        isPlaying = videoPlayer.isPlaying;
        // Play video when Enter is pressed
        if (!isPlaying && GateInteraction.gateOpened)
        {
            if (videoPlayer != null)
            {
                playVideo();
            }
        }
    }

    void playVideo()
    {
        screen.SetActive(true);
        videoPlayer.Play();
        Debug.Log("Video playing!");
    }
    

    void OnDestroy()
    {
        // Always unsubscribe to prevent memory leaks
        videoPlayer.loopPointReached -= OnVideoFinished;
        screen.SetActive(false);
        Debug.Log("Unsubscribed!");
    }

}