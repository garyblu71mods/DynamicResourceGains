using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Dynamic_Resource_Gains
{
    public static class DRGUtil
    {
        private const float MIN_MULTIPLIER = 0.01f;
        private const float MAX_MULTIPLIER = 100f;

        /// <summary>
        /// Gets the current tech tier (number of unlocked technologies)
        /// </summary>
        public static int GetTechTier()
        {
            int fromRnD = GetTechTierFromResearchAndDevelopment();
            if (fromRnD > 0)
                return fromRnD;

            int fromTree = GetTechTierFromTechTree();
            if (fromTree > 0)
                return fromTree;

            return GetTechTierFromGameDatabase();
        }

        private static int GetTechTierFromResearchAndDevelopment()
        {
            if (ResearchAndDevelopment.Instance == null)
                return 0;

            FieldInfo field = typeof(ResearchAndDevelopment).GetField("protoTechNodes", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field == null)
                return 0;

            IDictionary nodes = field.GetValue(ResearchAndDevelopment.Instance) as IDictionary;
            if (nodes == null)
                return 0;

            int count = 0;
            foreach (DictionaryEntry entry in nodes)
            {
                ProtoTechNode tech = entry.Value as ProtoTechNode;
                if (tech != null && tech.state == RDTech.State.Available)
                    count++;
            }

            return count;
        }

        private static int GetTechTierFromTechTree()
        {
            if (AssetBase.RnDTechTree == null)
                return 0;

            FieldInfo field = AssetBase.RnDTechTree.GetType().GetField("TechTreeTechs", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field == null)
                return 0;

            ProtoTechNode[] techs = field.GetValue(AssetBase.RnDTechTree) as ProtoTechNode[];
            if (techs == null)
                return 0;

            int count = 0;
            foreach (ProtoTechNode tech in techs)
            {
                if (tech != null && tech.state == RDTech.State.Available)
                    count++;
            }

            return count;
        }

        private static int GetTechTierFromGameDatabase()
        {
            if (GameDatabase.Instance == null)
                return 0;

            ConfigNode[] techNodes = GameDatabase.Instance.GetConfigNodes("RDNode");
            if (techNodes == null)
                return 0;

            int count = 0;
            foreach (ConfigNode node in techNodes)
            {
                string techId = node.GetValue("id");
                if (string.IsNullOrEmpty(techId))
                    continue;

                if (ResearchAndDevelopment.GetTechnologyState(techId) == RDTech.State.Available)
                    count++;
            }

            return count;
        }

        /// <summary>
        /// Gets lifetime science points
        /// </summary>
        public static float GetLifetimeScience()
        {
            if (ResearchAndDevelopment.Instance == null)
                return 0f;

            return ResearchAndDevelopment.Instance.Science;
        }

        /// <summary>
        /// Clamps multiplier between MIN and MAX
        /// </summary>
        public static float ClampMultiplier(float multiplier)
        {
            return Mathf.Clamp(multiplier, MIN_MULTIPLIER, MAX_MULTIPLIER);
        }
    }
}
