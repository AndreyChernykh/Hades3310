﻿using System.Collections;
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
    public static int currentHealth = 10;

    public static int permanentPower = 1;
    public static int currentPower = 1;

    public static int money = 0;

    public static int currentRoom = 0;
    public static int bossRoom = 13;

    public static Weapons selectedWeapon = Weapons.SWORD;

    public static void ResetStats() {
        attempts += 1;
        attemptMaxHealth = permanentMaxHealth;
        currentHealth = permanentMaxHealth;
        currentPower = permanentPower;
        currentRoom = 0;
    }
}