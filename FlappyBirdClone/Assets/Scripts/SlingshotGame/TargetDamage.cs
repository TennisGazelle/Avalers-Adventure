using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetDamage : MonoBehaviour {

    // target attributes
    private Rigidbody2D rb;
    private Collider2D collider;
    private GameObject gb;
    private Transform targetTransform;

    private Vector3 originalTargetPosition;
    private Quaternion originalTargetRotation;

    // sprites
    public Sprite damagedSprite;
    public SpriteRenderer spriteRenderer;

    // score text
    public Text scoreText;
    int score;

    // misc
    public int hitPoints = 1;
    private int currentHitPoints;
    public float damageImpactSpeed;
    private float damageImpactSpeedSqr;

    private float rotationResetSpeed = 1.0f;



    void Start () {
        // get target attributes
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        gb = GetComponent<GameObject>();
        targetTransform = GetComponent<Transform>();

        originalTargetPosition = new Vector3(targetTransform.position.x, targetTransform.position.y, targetTransform.position.z);
        originalTargetRotation = targetTransform.rotation;

        currentHitPoints = hitPoints;
        damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
        

        score = 0;
    }

    void Update()
    {
        if (GameObject.Find("AsteroidEmpty").GetComponent<GameResetter>().targetReset)
        {
            resetTarget();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameObject.Find("AsteroidEmpty").GetComponent<ProjectileDragging>().isShot)
        {
            if (collision.collider.tag != "Damager")
            {
                return;
            }
            if (collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr)
            {
                return;
            }

            //spriteRenderer.sprite = damagedSprite;

            currentHitPoints--;

            if (currentHitPoints <= 0)
            {
                Kill();
            }
        }
    }

    void Kill()
    {
        spriteRenderer.enabled = false;
        collider.enabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;

        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    void resetTarget()
    {
        rb.velocity = new Vector3(0,0,0);
        targetTransform.position = originalTargetPosition;
        targetTransform.rotation = Quaternion.Slerp(targetTransform.transform.rotation, originalTargetRotation, Time.time * rotationResetSpeed);

        spriteRenderer.enabled = true;
        collider.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;

        GameObject.Find("AsteroidEmpty").GetComponent<GameResetter>().targetReset = false;
    }
}
