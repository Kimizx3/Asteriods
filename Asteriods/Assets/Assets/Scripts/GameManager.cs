using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public int score = 0;
    public Text scoreText;
    public Text liveText;

    private void Start()
    {
        UpdateScoreText();
        UpdateLiveText();
    }

    public void AsteroridDestroyed(Asteriod asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        explosion.Play();
        
        // TODO: Add score
        if (asteroid.size < 0.75f)
        {
            score += 100;
            UpdateScoreText();
        }
        else if (asteroid.size < 1.2f)
        {
            score += 50;
            UpdateScoreText();
        }
        else
        {
            score += 25;
            UpdateScoreText();
        }
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = player.transform.position;
        this.explosion.Play();
        this.lives--;
        UpdateLiveText();

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
        
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        lives = 3;
        
        score = 0;
        
        Invoke(nameof(Respawn), respawnTime);
        UpdateLiveText();
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "" + score;
    }
    private void UpdateLiveText()
    {
        liveText.text = "x" + lives;
    }
}
