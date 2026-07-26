using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHearts;
    [SerializeField] private GameObject heartImgPrefab;
    [SerializeField] private Transform heartsLayoutGroup;
    [SerializeField] private float refractoryPeriod;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private List<GameObject> curHearts;

    private bool isInvulnerable;
    public bool IsInvulnerable
    {
        get => isInvulnerable;
        set
        {
            isInvulnerable = value;
            SetAlpha(isInvulnerable ? 0.25f : 1f);
        }
    }

    private void Start()
    {
        curHearts = new List<GameObject>();

        for (int i = 0; i < maxHearts; i++)
        {
            GameObject h = Instantiate(heartImgPrefab, heartsLayoutGroup);
            curHearts.Add(h);
        }
    }

    public void TakeDamage()
    {
        if (IsInvulnerable || curHearts.Count == 0)
            return;
        
        int lastIndex = curHearts.Count - 1;
        GameObject heartToRemove = curHearts[lastIndex];
        curHearts.RemoveAt(lastIndex);
        Destroy(heartToRemove);
        GetComponentInChildren<PlayerAudio>().PlayPlayerHurtAudio(curHearts.Count);

        if (curHearts.Count == 0)
        {
            Die();
        }
        else if (refractoryPeriod > 0f)
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        IsInvulnerable = true;

        yield return new WaitForSeconds(refractoryPeriod);
        
        //re enable in case there is an enemy on top of the player as invulnerability ends
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;
        
        IsInvulnerable = false;
    }

    private void SetAlpha(float alpha)
    {
        if (spriteRenderer == null)
            return;

        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }

    private void Die()
    {
        AudioManager.Instance.OnLoseGame();
        Debug.Log($"<color=cyan>{name} has died.</color>");
        Destroy(gameObject);
    }
}