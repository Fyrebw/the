﻿using RoR2;
using System;
using Modules.Characters;
using R2API;
using UnityEngine;
using TeslaTrooper;
using ModdedEntityStates.TeslaTrooper.Tower;

namespace Modules.Survivors {
    internal class TeslaTowerScepter : CharacterBase {

        public override string bodyName => "TeslaTowerScepter";

        public const string TOWER_PREFIX = TeslaTrooperPlugin.DEV_PREFIX + "_TESLA_TOWER_BODY_";
        public const string TOWER_SCEPTER_PREFIX = TeslaTrooperPlugin.DEV_PREFIX + "_TESLA_TOWER_SCEPTER_";

        public override BodyInfo bodyInfo { get; set; }

        public override CustomRendererInfo[] customRendererInfos { get; set ; }

        //todo scepter: TowerIdleSearchButForMultipleTargets
        public override Type characterMainState => typeof(TowerIdleSearchScepter);
        public override Type characterSpawnState => typeof(TowerSpawnStateScepter);

        public override ItemDisplaysBase itemDisplays => baseTower.itemDisplays;

        public static GameObject masterPrefab;
        public TeslaTowerNotSurvivor baseTower;

        internal static SkinDef.MinionSkinReplacement MasteryMinionSkinReplacement;
        internal static SkinDef.MinionSkinReplacement MCMinionSkinReplacement;
        internal static SkinDef.MinionSkinReplacement NodMinionSkinReplacement;

        public void Initialize(TeslaTowerNotSurvivor baseTower_) {

            baseTower = baseTower_;
            InitializeCharacter();
        }

        public override void InitializeCharacter() {

            StealCharacterBodyAndModel();
            StealCharacterMaster();

            InitializeEntityStateMachine();
            //todo scepter: skills when base tower gets ai for their skills
            //InitializeSkills();

            InitializeSkins();
            InitializeItemDisplays();
        }

        private void StealCharacterBodyAndModel() {
            bodyPrefab = PrefabAPI.InstantiateClone(baseTower.bodyPrefab, "TeslaTowerScepterBody");
            CharacterBody characterBody = bodyPrefab.GetComponent<CharacterBody>();
            characterBody.baseNameToken = TOWER_SCEPTER_PREFIX + "NAME";
            characterBody.portraitIcon = Modules.Assets.LoadCharacterIcon("texIconTeslaTower");

            Modules.Content.AddCharacterBodyPrefab(bodyPrefab);

            bodyCharacterModel = bodyPrefab.GetComponentInChildren<CharacterModel>();
        }

        private void StealCharacterMaster() {
            masterPrefab = PrefabAPI.InstantiateClone(TeslaTowerNotSurvivor.masterPrefab, "TeslaTowerScepterMaster");
            masterPrefab.GetComponent<CharacterMaster>().bodyPrefab = bodyPrefab;

            Modules.Content.AddMasterPrefab(masterPrefab);
        }

        public override void InitializeSkins() {

            ModelSkinController skinController = bodyCharacterModel.gameObject.GetComponent<ModelSkinController>();

            #region Default
            TowerSkinDef scepterDefault = Skins.DuplicateScepterSkinDef(skinController.skins[0] as TowerSkinDef);

            scepterDefault.meshReplacements = Modules.Skins.getMeshReplacements(scepterDefault.rendererInfos,
                "Tower_Base_Pillars_Color_Scepter",
                null,//"Mastery_Base_Platform",
                null,//"Mastery_Base_Center",
                null,//"Mastery_Base_Tubes",

                "Tower_Circles_Scepter",
                null,//"Mastery_Pole",
                null,//"Mastery_Pole_Tracer",
                null,//"Mastery_Emission",
                null);//"Mastery_Orb");

            skinController.skins[0] = scepterDefault;
            #endregion Default

            #region Mastery
            TowerSkinDef scepterMastery = Skins.DuplicateScepterSkinDef(skinController.skins[1] as TowerSkinDef);

            scepterMastery.meshReplacements = Modules.Skins.getMeshReplacements(scepterMastery.rendererInfos,
                "Mastery_Base_Pillars_Color_Scepter",
                null,//"Mastery_Base_Platform",
                "Mastery_Base_Center_Scepter",
                null,//"Mastery_Base_Tubes",

                "Mastery_Circles_Scepter",
                null,//"Mastery_Pole",
                null,//"Mastery_Pole_Tracer",
                null,//"Mastery_Emission",
                null);//"Mastery_Orb");

            skinController.skins[1] = scepterMastery;

            MasteryMinionSkinReplacement = new SkinDef.MinionSkinReplacement {
                minionBodyPrefab = bodyPrefab,
                minionSkin = scepterMastery,
            };

            #endregion Mastery

            #region Nod
            TowerSkinDef scepterNod = Skins.DuplicateScepterSkinDef(skinController.skins[2] as TowerSkinDef);
            scepterNod.ZapLightningType = (skinController.skins[2] as TowerSkinDef).ZapLightningType;

            scepterNod.meshReplacements = Modules.Skins.getMeshReplacements(scepterNod.rendererInfos,
                null,//"Nod_Base_Pillars_Color_Scepter",
                null,//"Nod_Base_Platform",
                "Nod_Tower_Scepter",//"Nod_Base_Center_Scepter",
                null,//"Nod_Base_Tubes",

                null,//"Nod_Circles_Scepter",
                null,//"Nod_Pole",
                null,//"Nod_Pole_Tracer",
                "Nod_Emission_Scepter",
                null);//"Nod_Orb");


            skinController.skins[2] = scepterNod;

            NodMinionSkinReplacement = new SkinDef.MinionSkinReplacement {
                minionBodyPrefab = bodyPrefab,
                minionSkin = scepterNod,
            };

            #endregion Nod

            #region MC
            TowerSkinDef scepterMC = Skins.DuplicateScepterSkinDef(skinController.skins[3] as TowerSkinDef);

            scepterMC.meshReplacements = Modules.Skins.getMeshReplacements(scepterMC.rendererInfos,
                null,//"MC_Base_Pillars_Color_Scepter",
                null,//"MC_Base_Platform",
                null,//"MC_Tower_Scepter",//"MC_Base_Center_Scepter",
                null,//"MC_Base_Tubes",

                "MC_Circles_Scepter",
                null,//"MC_Pole",
                null,//"MC_Pole_Tracer",
                null,//"MC_Emission_Scepter",
                "MC_Orb_Scepter");


            skinController.skins[3] = scepterMC;

            MCMinionSkinReplacement = new SkinDef.MinionSkinReplacement {
                minionBodyPrefab = bodyPrefab,
                minionSkin = scepterMC,
            };

            #endregion MC
        }
    }
}