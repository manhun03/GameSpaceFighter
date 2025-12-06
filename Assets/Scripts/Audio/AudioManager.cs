using UnityEngine;

public class AudioManager : MonoBehaviour
{
   

    [SerializeField] private AudioSource BackgroundAudio;
    [SerializeField] private AudioSource EffectAudio;

    [SerializeField] private AudioClip BackgroundClip;
    [SerializeField] private AudioClip FightBossClip;
    [SerializeField] private AudioClip PlayerGunCLip;
    [SerializeField] private AudioClip BossGunClip;
    [SerializeField] private AudioClip BossLaserClip;
    [SerializeField] private AudioClip WinClip;
    [SerializeField] private AudioClip LoseClip;

   
    void Start()
    {
        PlayBackground();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBackground()
    {
        BackgroundAudio.clip = BackgroundClip;
        BackgroundAudio.Play();
    }
    public void PlayPlayerGun()
    {
        EffectAudio.PlayOneShot(PlayerGunCLip);
    }
    public void PlayBossGun()
    {
        EffectAudio.PlayOneShot(BossGunClip);
    }
    public void PlayBossLaser()
    {
        EffectAudio.PlayOneShot(BossLaserClip);
    }
    public void PlayFightBoss()
    {
        if (BackgroundAudio.isPlaying)
            BackgroundAudio.Stop();

        BackgroundAudio.clip = FightBossClip;
        BackgroundAudio.loop = true;
        BackgroundAudio.Play();
    }
    public void PlayLose()
    {
        if (BackgroundAudio.isPlaying)
            BackgroundAudio.Stop();
        BackgroundAudio.clip = LoseClip;
        BackgroundAudio.loop = false;
        BackgroundAudio.Play();
    }
    public void PlayWin()
    {
        if (BackgroundAudio.isPlaying)
            BackgroundAudio.Stop();
        BackgroundAudio.clip = WinClip;
        BackgroundAudio.loop = false;
        BackgroundAudio.Play();
    }
}
