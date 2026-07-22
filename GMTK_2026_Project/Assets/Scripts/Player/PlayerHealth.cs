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

    public bool IsInvulnerable { get; set; }

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
        SetAlpha(0.25f);

        yield return new WaitForSeconds(refractoryPeriod);
        
        //re enable in case there is an enemy on top of the player as invulnerability ends
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;
        
        IsInvulnerable = false;
        SetAlpha(1f);
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
        Debug.Log($"<color=cyan>{name} has died.</color>");
        Destroy(gameObject);
    }
}