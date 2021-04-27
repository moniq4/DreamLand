using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float healthMax = 100;
    public float invulnerableTime = 1;
    public bool playerCharacter = false;
    public Transform healthBar;
    public AudioController audioController;

    private float healthCurrent;    
    private bool invulnerable;
    private float invulnerableTimer;

    // Start is called before the first frame update
    void Start()
    {
        healthCurrent = healthMax;
        invulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (invulnerable)
        {
            invulnerableTimer -= Time.deltaTime;

            if (invulnerableTimer <= 0f)
            {
                invulnerable = false;
            }
        }
    }

    public void Damage(float damageAmount)
    {
        if (!invulnerable)
        {
            healthCurrent -= damageAmount;

            if (healthCurrent <= 0)
            {
                healthCurrent = 0;
                if (playerCharacter)
                {
                    SceneManager.LoadScene("Start");
                }
                else
                {
                    audioController.PlaySound("HeroDeathSound");
                    Destroy(gameObject);
                }
            }
            else
            {
                if (!playerCharacter) audioController.PlaySound("NPCDeathSound");
                audioController.PlaySound("HitSound");
            }

            if (healthBar)
            {
                Vector3 newScale = healthBar.transform.localScale;
                newScale.x = healthCurrent / healthMax;
                healthBar.localScale = newScale;
            }

            invulnerable = true;
            invulnerableTimer = invulnerableTime;
        }
    }

    public void Heal(float healAmount)
    {
        healthCurrent += healAmount;
        if (healthCurrent > healthMax) healthCurrent = healthMax;

        if (healthBar)
        {
            Vector3 newScale = healthBar.transform.localScale;
            newScale.x = healthCurrent / healthMax;
            healthBar.localScale = newScale;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var dm = collision.gameObject.GetComponent<Damage>();
        if (dm != null && name != collision.transform.parent.name)
        {
            Damage(dm.damage);
        }
    }
}
