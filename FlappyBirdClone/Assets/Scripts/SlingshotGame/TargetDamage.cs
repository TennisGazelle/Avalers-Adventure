using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetDamage : MonoBehaviour {

    public int hitPoints = 1;
    public Sprite damagedSprite;
    public float damageImpactSpeed;
    public SpriteRenderer spriteRenderer;

    public Text scoreText;
    int score;

    private int currentHitPoints;
    private float damageImpactSpeedSqr;

    private Rigidbody2D rb;
    private Collider2D collider;
    private GameObject gb;

    void Start () {
        currentHitPoints = hitPoints;
        damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        gb = GetComponent<GameObject>();

        score = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Damager")
        {
            return;
        }
        if (collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr)
        {
            return;
        }

        spriteRenderer.sprite = damagedSprite;

        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        spriteRenderer.enabled = false;
        collider.enabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;

        score++;
        scoreText.text = "Score: " + score.ToString();
        // gb.SetActive(false);
    }
}
