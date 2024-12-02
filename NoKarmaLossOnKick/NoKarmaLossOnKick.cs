using System;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

[BepInPlugin("discode.no_karma_loss_on_kick", "noKarmaLossOnKick", "1.0.0.0")]
public class Mod_NoKarmaLossOnKick : BaseUnityPlugin
{
    public static ConfigEntry<string> configKick;
    public static ConfigEntry<bool> configDisplayKick;
    private static Harmony harmony;
    private void Start()
    {
        configDisplayKick = Config.Bind("General.Toggles",
                                        "DisplayKick",
                                        true,
                                        "true: enable kick option on action menu, false: hide kick option on action menu");
        harmony = new Harmony("CharaPatch");
        harmony.PatchAll();
    }
    private void OnDestroy()
    {
        harmony.UnpatchSelf();
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

    [HarmonyPrefix, HarmonyPatch(typeof(ActKick), nameof(ActKick.CanPerform), new Type[] { })]
    public static bool CanPerform(ActKick __instance)
    {
        //System.Console.WriteLine("Kick()------");
        //System.Console.WriteLine(karmaLoss);
        bool DisplayKick = Mod_NoKarmaLossOnKick.configDisplayKick.Value;
        if (!DisplayKick) {
            return false;
        }
        return true;
    }
}

