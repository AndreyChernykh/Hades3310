using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapons {
    SWORD,
    BOW
}

public static class Stats {
    public static int attempts = 0;

    public static int permanentMaxHealth = 50;
    public static int attemptMaxHealth = 50;
    public static int currentHealth = 50;

    public static int permanentPower = 1;
    public static int currentPower = 1;

    public static int money = 500;

    public static bool isZeusBoonEnabled = false;

    public static int currentLevel = 0;

    public static Weapons selectedWeapon = Weapons.SWORD;
}
