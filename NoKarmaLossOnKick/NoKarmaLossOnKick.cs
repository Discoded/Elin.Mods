using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;

[BepInPlugin("discode.no_karma_loss_on_kick", "noKarmaLossOnKick", "1.0.0.0")]
public class Mod_NoKarmaLossOnKick : BaseUnityPlugin
{
    private void Start()
    {
        var harmony = new Harmony("CharaPatch");
        harmony.PatchAll();
    }
}

[HarmonyPatch]
class CharaPatch
{
    [HarmonyPrefix, HarmonyPatch(typeof(Chara), nameof(Chara.Kick), new Type[] { typeof(Chara), typeof(bool), typeof(bool) })]
    public static void Kick(Chara __instance, Chara t, bool ignoreSelf, ref bool karmaLoss)
    {
        //System.Console.WriteLine("Kick()------");
        //System.Console.WriteLine(karmaLoss);
        karmaLoss = false;
        return;
    }
}

