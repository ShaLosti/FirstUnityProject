﻿using UnityEngine;
using System;

[Serializable]
public class HpController : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float currentHP;
    [SerializeField] private float maxHP = 10;
    [SerializeField] private float baseHP;

    [SerializeField] private GameObject UIBarFill;
    private IUIHealthBar healthBar;

    private void Start()
    {
        CurrentHP = BaseHP = this.MaxHP = maxHP;
        healthBar = UIBarFill.GetComponent<IUIHealthBar>();
        DefaultValues();
    }

    public float CurrentHP { get => currentHP; set => currentHP = value; }
    public float MaxHP { get => maxHP; set => maxHP = value; }
    public float BaseHP { get => baseHP; set => baseHP = value; }

    public void TakeDamage(int damage = 0)
    {
        CurrentHP -= damage;
        UpdateHealthBar();
    }
    public void AddHP(int hp)
    {
        this.CurrentHP += hp;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        CheckValues();
        if (CurrentHP <= 0)
        {
            GameManager.GameOver();
        }
        healthBar.UpdateMaxHPFill(maxHP);
        healthBar.UpdateCurrentHPFill(CurrentHP);       
    }

    private void CheckValues()
    {
        if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        if (CurrentHP < 0) CurrentHP = 0;
    }

    public void AddMaxHP(int hp)
    {
        this.MaxHP += hp;
        UpdateHealthBar();
    }

    public void UpdateBaseMaxHP(int value)
    {
        BaseHP = value;
        UpdateHealthBar();
    }

    public void DefaultValues()
    {
        MaxHP = BaseHP;
        CurrentHP = MaxHP;
        CheckValues();
        UpdateHealthBar();
    }

}
