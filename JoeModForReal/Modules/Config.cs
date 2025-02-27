﻿using System;
using System.Runtime.CompilerServices;
using BepInEx.Configuration;
using JoeModForReal;
using RiskOfOptions;
using RiskOfOptions.Options;
using UnityEngine;

namespace Modules
{
    internal static class Config
    {
        public static bool Debug;

        public static bool Cursed;

        public static ConfigEntry<bool> jerry;

        //man this is just too much work
        ////primary
        //public static ConfigEntry<float> primaryDamage;
        //public static ConfigEntry<float> primaryDuration;

        //public static ConfigEntry<float> primaryJumpSwingDamage;

        ////secondary
        //public static ConfigEntry<float> secondaryDamage;
        //public static ConfigEntry<int> secondaryStonks;
        //public static ConfigEntry<float> secondaryCooldown;

        public static void ReadConfig()
        {
            Debug = FacelessJoePlugin.instance.Config.Bind(
                "Debug",
                "Debug Logs",
                false,
                "in case I forget to delete them when I upload").Value;

            string sectionGeneral = "General";

            Cursed = FacelessJoePlugin.instance.Config.Bind(
                sectionGeneral,
                "Cursed",
                false,
                "Enable wip/unused content").Value;

            //jerry =
            //    BindAndOptions(
            //        sectionGeneral,
            //        "Everyone is Jerry",
            //        true,
            //        "When anyone dies, the Jerry scream is heard.");

            #region fuck
            /*
            string sectionBETA = "Z_BETA";

            #region primary
            primaryDamage =
                BindAndOptions(
                    sectionBETA,
                    "Primary Swing Damage",
                    1.8f
                );

            primaryDuration =
                BindAndOptions(
                    sectionBETA,
                    "Primary Duration",
                    0.96ff
                );

            primaryJumpSwingDamage =
                BindAndOptions(
                    sectionBETA,
                    "Primary Jump Swing Damage",
                    3.2f
                );
            #endregion primary

            #region secondary

            #endregion secondary
            */
            #endregion fuck

        }

        public static ConfigEntry<T> BindAndOptions<T>(string section, string name, T defaultValue, string description = "") {

            if (string.IsNullOrEmpty(description))
                description = name;

            ConfigEntry<T> entry = FacelessJoePlugin.instance.Config.Bind(section, name, defaultValue, description);

            if (Compat.RiskOfOptionsInstalled) {
                RegisterOption(entry);
            }

            return entry;
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void RegisterOption<T>(ConfigEntry<T> entry) {

            if (entry is ConfigEntry<float>) {
                ModSettingsManager.AddOption(new SliderOption(entry as ConfigEntry<float>));
            }
            if (entry is ConfigEntry<int>) {
                ModSettingsManager.AddOption(new IntSliderOption(entry as ConfigEntry<int>));
            }
            if (entry is ConfigEntry<bool>) {
                ModSettingsManager.AddOption(new CheckBoxOption(entry as ConfigEntry<bool>));
            }
        }

        //Taken from https://github.com/ToastedOven/CustomEmotesAPI/blob/main/CustomEmotesAPI/CustomEmotesAPI/CustomEmotesAPI.cs
        public static bool GetKeyPressed(ConfigEntry<KeyboardShortcut> entry) {
            foreach (var item in entry.Value.Modifiers) {
                if (!Input.GetKey(item)) {
                    return false;
                }
            }
            return Input.GetKeyDown(entry.Value.MainKey);
        }
    }
}