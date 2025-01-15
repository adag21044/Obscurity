using UnityEngine;
using UnityEngine.Video;

public class CustomVideoPlayer : MonoBehaviour, ITriggerable
{
    public string videoPath;

    public void Initialize(string videoPath)
    {
        this.videoPath = videoPath;
    }

    public void Trigger() => PlayVideo();

    public void PlayVideo()
    {
        Debug.Log("Attempting to play jumpscare video");

        var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.url = videoPath;
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = false;

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (vp) => vp.Play();

        videoPlayer.errorReceived += (vp, message) =>
        {
            Debug.LogError($"VideoPlayer Error: {message}");
        };
    }
}
