using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapons {
    SWORD,
    BOW
}

public static class Stats {
    public static int attempts = 0;

    public static int permanentMaxHealth = 10;
    public static int attemptMaxHealth = 10;

    public static event Action OnCurrentHealthChange = delegate { };
    private static int currentHealth = 10;
    public static int CurrentHealth {
        get { return currentHealth; }
        set {
            currentHealth = value;
            OnCurrentHealthChange.Invoke();
        }
    }

    public static int permanentDamagePerHit = 1;
    public static int currentPower = 1;

    public static event Action OnMoneyChange = delegate { };
    private static int money = 0;
    public static int Money {
        get { return money; }
        set {
            money = value;
            OnMoneyChange.Invoke();
        }
    }

    public static int currentRoom = 0;
    public static int bossRoom = 13;

    public static Weapons selectedWeapon = Weapons.SWORD;

    public static void ResetStats() {
        attempts += 1;
        attemptMaxHealth = permanentMaxHealth;
        currentHealth = permanentMaxHealth;
        currentPower = permanentDamagePerHit;
        currentRoom = 0;
    }
}
