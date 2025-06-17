
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : GateInteraction
{
    public VideoPlayer videoPlayer;
    public VideoClip videoClip;
    // Check if video is currently playing
    bool isPlaying;

    void Start()
    {
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
        if (!isPlaying && gateOpened)
        {
            if (videoPlayer != null)
            {
                videoPlayer.Play();
                Debug.Log("Video playing!");
            }
        }
    }

    void OnDestroy()
    {
        // Always unsubscribe to prevent memory leaks
        videoPlayer.loopPointReached -= OnVideoFinished;
        Debug.Log("Unsubscribed!");
    }

}