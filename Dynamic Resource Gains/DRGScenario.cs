using System;
using UnityEngine;

namespace Dynamic_Resource_Gains
{
    [KSPScenario(ScenarioCreationOptions.AddToAllGames, GameScenes.SPACECENTER)]
    public class DRGScenario : ScenarioModule
    {
        [KSPField(isPersistant = true)]
        public float BaseScienceMultiplier = 1.0f;

        [KSPField(isPersistant = true)]
        public float BaseFundsMultiplier = 1.0f;

        [KSPField(isPersistant = true)]
        public float BaseReputationMultiplier = 1.0f;

        [KSPField(isPersistant = true)]
        public bool DynamicScience = true;

        [KSPField(isPersistant = true)]
        public bool DynamicFunds = true;

        [KSPField(isPersistant = true)]
        public bool DynamicReputation = true;

        [KSPField(isPersistant = true)]
        public float SciencePerTierPenalty = 0.02f;

        [KSPField(isPersistant = true)]
        public float ScienceStep = 3000f;

        [KSPField(isPersistant = true)]
        public float ScienceStepPenalty = 0.001f;

        [KSPField(isPersistant = true)]
        public float FundsScaleFactor = 0.00002f;

        [KSPField(isPersistant = true)]
        public float FundsStep = 100000f;

        [KSPField(isPersistant = true)]
        public float RepScaleFactor = 0.001f;

        [KSPField(isPersistant = true)]
        public float RepStep = 100f;

        public override void OnAwake()
        {
            base.OnAwake();
            Debug.Log("[DRG] DRGScenario OnAwake");
        }

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);
            Debug.Log("[DRG] DRGScenario OnLoad");
            LoadMultipliers();
        }

        public override void OnSave(ConfigNode node)
        {
            base.OnSave(node);
            Debug.Log("[DRG] DRGScenario OnSave");
        }

        public void SaveMultipliers()
        {
            if (HighLogic.CurrentGame != null)
            {
                GamePersistence.SaveGame("persistent", HighLogic.SaveFolder, SaveMode.OVERWRITE);
            }
            Debug.Log("[DRG] Configuration saved to game file");
        }

        public void LoadMultipliers()
        {
            Debug.Log("[DRG] Loaded - Science: " + (BaseScienceMultiplier * 100) + "%, Funds: " + (BaseFundsMultiplier * 100) + "%, Reputation: " + (BaseReputationMultiplier * 100) + "%");
        }

        public float GetCurrentScienceMultiplier()
        {
            if (!DynamicScience)
                return BaseScienceMultiplier;

            int techTier = DRGUtil.GetTechTier();
            float lifetimeScience = DRGUtil.GetLifetimeScience();
            float multiplier = BaseScienceMultiplier;
            multiplier -= (techTier * SciencePerTierPenalty / 100f);
            multiplier -= ((lifetimeScience / ScienceStep) * ScienceStepPenalty / 100f);
            return DRGUtil.ClampMultiplier(multiplier);
        }

        public float GetCurrentFundsMultiplier()
        {
            if (!DynamicFunds)
                return BaseFundsMultiplier;

            float currentFunds = Funding.Instance != null ? (float)Funding.Instance.Funds : 0f;
            float effectiveStep = FundsStep > 0f ? FundsStep : 100000f;
            float multiplier = BaseFundsMultiplier - ((currentFunds / effectiveStep) * FundsScaleFactor * 100f);
            return DRGUtil.ClampMultiplier(multiplier);
        }

        public float GetCurrentReputationMultiplier()
        {
            if (!DynamicReputation)
                return BaseReputationMultiplier;

            float currentRep = Reputation.Instance != null ? Reputation.Instance.reputation : 0f;
            float effectiveStep = RepStep > 0f ? RepStep : 100f;
            float multiplier = BaseReputationMultiplier - ((currentRep / effectiveStep) * RepScaleFactor * 100f);
            return DRGUtil.ClampMultiplier(multiplier);
        }
    }
}
