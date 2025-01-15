using UnityEngine.Video;
using UnityEngine;

public class UnityVideoPlayerAdapter : ITriggerable
{
    private readonly VideoPlayer videoPlayer;

    public UnityVideoPlayerAdapter(VideoPlayer videoPlayer)
    {
        this.videoPlayer = videoPlayer;
    }

    public void Trigger()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play(); // Unity'nin VideoPlayer API'sini kullan
            Debug.Log("Jumpscare video is playing!");
        }
        else
        {
            Debug.LogError("VideoPlayer is null. Cannot play the video.");
        }
    }
}
