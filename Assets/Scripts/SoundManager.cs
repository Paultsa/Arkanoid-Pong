using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource hit;
    public AudioSource loseLife;
    public AudioSource shot;
    public AudioSource shield;
    public AudioSource heal;
    public AudioSource powerUp;
    public AudioSource expand;
    public AudioSource gameOver;
    public AudioSource win;
    public AudioSource music;

    public void Hit() { hit.Play(); }
    public void LoseLife() { loseLife.Play(); }
    public void Shot() { shot.Play(); }
    public void Shield() { shield.Play(); }
    public void Heal() { heal.Play(); }
    public void PowerUp() { powerUp.Play(); }
    public void Expand() { expand.Play(); }
    public void GameOver() { gameOver.Play(); }
    public void Win() { win.Play(); }
    public void StopMusic() { music.Stop(); }
}
