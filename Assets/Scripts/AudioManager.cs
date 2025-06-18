using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource deathSound;
    AudioSource defendingSoung;
    AudioSource pickUpHealthSoung;
    AudioSource playerDeathSoung;
    AudioSource swordClashSoung;
    AudioSource winningSoung;
    AudioSource swordHitSoung;
    AudioSource bgSoung;
    AudioSource playerTakingDamageSoung;
    AudioSource enemyDeathSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] allAudioSources = GetComponents<AudioSource>();
        deathSound = allAudioSources[0];
        defendingSoung = allAudioSources[1];
        pickUpHealthSoung = allAudioSources[2];
        playerDeathSoung = allAudioSources[3];
        swordClashSoung = allAudioSources[4];
        winningSoung = allAudioSources[5];
        swordHitSoung = allAudioSources[6];
        bgSoung = allAudioSources[7];
        playerTakingDamageSoung = allAudioSources[8];
        enemyDeathSound = allAudioSources[9];
    }

    public void PlayDeathSound() => deathSound.Play();
    public void PlayDefendingSound() => defendingSoung.Play();
    public void PlayPickUpHealthSound() => pickUpHealthSoung.Play();
    public void PlayPlayerDeath() => playerDeathSoung.Play();
    public void PlaySwordClash() => swordClashSoung.Play();
    public void PlayWinningSound() => winningSoung.Play();
    public void PlaySwordHitSound() => swordHitSoung.Play();
    public void PlayBackgroundMusic() => bgSoung.Play();
    public void PlayPlayerTakingDamage() => playerTakingDamageSoung.Play();
    public void PlayEnemyDeath() => enemyDeathSound.Play();
}
