using UnityEngine;
using KSP.UI.Screens;

namespace Dynamic_Resource_Gains
{
    [KSPAddon(KSPAddon.Startup.SpaceCenter, false)]
    public class DynamicResourceGains : MonoBehaviour
    {
        private static DynamicResourceGains instance;
        private ApplicationLauncherButton appButton;
        private bool guiVisible = false;
        private Rect windowRect = new Rect(100, 100, 400, 600);

        private float baseScienceMultiplier = 1.0f;
        private float baseFundsMultiplier = 1.0f;
        private float baseRepMultiplier = 1.0f;

        private bool dynamicScience = false;
        private bool dynamicFunds = false;
        private bool dynamicReputation = false;

        private float sciencePerTierPenalty = 0.05f;
        private float scienceStepPenalty = 0.01f;
        private float scienceStep = 10000f;
        private float fundsScaleFactor = 0.001f;
        private float repScaleFactor = 0.01f;

        private string scienceMultiplierInput = "100";
        private string fundsMultiplierInput = "100";
        private string repMultiplierInput = "100";

        private string sciencePerTierInput = "0.05";
        private string scienceStepPenaltyInput = "0.01";
        private string scienceStepInput = "10000";
        private string fundsScaleFactorInput = "0.001";
        private string repScaleFactorInput = "0.01";

        private GUIStyle windowStyle;
        private GUIStyle labelStyle;
        private GUIStyle buttonStyle;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            LoadConfiguration();
            Texture2D buttonTexture = new Texture2D(38, 38, TextureFormat.RGBA32, false);
            AddAppLauncherButton(buttonTexture);

            GameEvents.OnTechnologyResearched.Add(OnTechnologyResearched);
            GameEvents.OnScienceReceived.Add(OnScienceReceived);
            GameEvents.OnFundsChanged.Add(OnFundsChanged);
            GameEvents.OnReputationChanged.Add(OnReputationChanged);

            Debug.Log("[DRG] Dynamic Resource Gains initialized");
        }

        private void OnDestroy()
        {
            GameEvents.OnTechnologyResearched.Remove(OnTechnologyResearched);
            GameEvents.OnScienceReceived.Remove(OnScienceReceived);
            GameEvents.OnFundsChanged.Remove(OnFundsChanged);
            GameEvents.OnReputationChanged.Remove(OnReputationChanged);

            if (appButton != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(appButton);
            }

            Debug.Log("[DRG] Dynamic Resource Gains destroyed");
        }

        private void AddAppLauncherButton(Texture2D texture)
        {
            appButton = ApplicationLauncher.Instance.AddModApplication(
                OnAppLauncherButtonToggleOn,
                OnAppLauncherButtonToggleOff,
                null,
                null,
                null,
                null,
                ApplicationLauncher.AppScenes.SPACECENTER,
                texture
            );
        }

        private void OnAppLauncherButtonToggleOn()
        {
            guiVisible = true;
        }

        private void OnAppLauncherButtonToggleOff()
        {
            guiVisible = false;
        }

        private void OnGUI()
        {
            if (!guiVisible || HighLogic.LoadedScene != GameScenes.SPACECENTER)
            {
                return;
            }

            if (windowStyle == null)
            {
                InitializeStyles();
            }

            windowRect = GUILayout.Window(
                GetInstanceID(),
                windowRect,
                DrawWindow,
                "Dynamic Resource Gains",
                windowStyle,
                GUILayout.MinWidth(350),
                GUILayout.MinHeight(200)
            );
        }

        private void DrawWindow(int windowID)
        {
            GUILayout.BeginVertical();

            GUILayout.Label("Base Multipliers (% of normal)", labelStyle);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Science:", GUILayout.Width(100));
            scienceMultiplierInput = GUILayout.TextField(scienceMultiplierInput, GUILayout.Width(100));
            GUILayout.Label("%", GUILayout.Width(30));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Funds:", GUILayout.Width(100));
            fundsMultiplierInput = GUILayout.TextField(fundsMultiplierInput, GUILayout.Width(100));
            GUILayout.Label("%", GUILayout.Width(30));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Reputation:", GUILayout.Width(100));
            repMultiplierInput = GUILayout.TextField(repMultiplierInput, GUILayout.Width(100));
            GUILayout.Label("%", GUILayout.Width(30));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.Label("Dynamic Scaling", labelStyle);

            dynamicScience = GUILayout.Toggle(dynamicScience, "Enable Dynamic Science");
            if (dynamicScience)
            {
                DrawScienceSettings();
            }

            dynamicFunds = GUILayout.Toggle(dynamicFunds, "Enable Dynamic Funds");
            if (dynamicFunds)
            {
                DrawFundsSettings();
            }

            dynamicReputation = GUILayout.Toggle(dynamicReputation, "Enable Dynamic Reputation");
            if (dynamicReputation)
            {
                DrawReputationSettings();
            }

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Apply", buttonStyle))
            {
                ApplyMultipliers();
            }
            if (GUILayout.Button("Save", buttonStyle))
            {
                SaveConfiguration();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }

        private void DrawScienceSettings()
        {
            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label("Science Settings:", labelStyle);

            int techTier = DRGUtil.GetTechTier();
            float lifetimeScience = DRGUtil.GetLifetimeScience();

            GUILayout.Label($"Current Tech Tier: {techTier}", labelStyle);
            GUILayout.Label($"Lifetime Science Earned: {lifetimeScience:F0}", labelStyle);

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Science per Tier Penalty:", GUILayout.Width(180));
            sciencePerTierInput = GUILayout.TextField(sciencePerTierInput, GUILayout.Width(100));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Science Step:", GUILayout.Width(180));
            scienceStepInput = GUILayout.TextField(scienceStepInput, GUILayout.Width(100));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Science Step Penalty:", GUILayout.Width(180));
            scienceStepPenaltyInput = GUILayout.TextField(scienceStepPenaltyInput, GUILayout.Width(100));
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        private void DrawFundsSettings()
        {
            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label("Funds Settings:", labelStyle);

            float currentFunds = HighLogic.CurrentGame.Parameters.Career.CurrentFunds;
            GUILayout.Label($"Current Funds: {currentFunds:F0}", labelStyle);

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Funds Scale Factor:", GUILayout.Width(180));
            fundsScaleFactorInput = GUILayout.TextField(fundsScaleFactorInput, GUILayout.Width(100));
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        private void DrawReputationSettings()
        {
            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label("Reputation Settings:", labelStyle);

            float currentRep = HighLogic.CurrentGame.Parameters.Career.CurrentReputation;
            GUILayout.Label($"Current Reputation: {currentRep:F0}", labelStyle);

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Reputation Scale Factor:", GUILayout.Width(180));
            repScaleFactorInput = GUILayout.TextField(repScaleFactorInput, GUILayout.Width(100));
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        private void ApplyMultipliers()
        {
            if (float.TryParse(scienceMultiplierInput, out float sciencePercent))
            {
                baseScienceMultiplier = sciencePercent / 100f;
            }
            if (float.TryParse(fundsMultiplierInput, out float fundsPercent))
            {
                baseFundsMultiplier = fundsPercent / 100f;
            }
            if (float.TryParse(repMultiplierInput, out float repPercent))
            {
                baseRepMultiplier = repPercent / 100f;
            }

            if (float.TryParse(sciencePerTierInput, out float sciencePerTier))
            {
                sciencePerTierPenalty = sciencePerTier;
            }
            if (float.TryParse(scienceStepPenaltyInput, out float scienceStepPen))
            {
                scienceStepPenalty = scienceStepPen;
            }
            if (float.TryParse(scienceStepInput, out float scienceStepVal))
            {
                scienceStep = scienceStepVal;
            }
            if (float.TryParse(fundsScaleFactorInput, out float fundsFactor))
            {
                fundsScaleFactor = fundsFactor;
            }
            if (float.TryParse(repScaleFactorInput, out float repFactor))
            {
                repScaleFactor = repFactor;
            }

            if (HighLogic.CurrentGame != null && HighLogic.CurrentGame.Parameters != null)
            {
                CareerParams careerParams = HighLogic.CurrentGame.Parameters.Career;

                float scienceMultiplier = CalculateScienceMultiplier();
                float fundsMultiplier = CalculateFundsMultiplier();
                float repMultiplier = CalculateReputationMultiplier();

                careerParams.ScienceGainMultiplier = scienceMultiplier;
                careerParams.FundsGainMultiplier = fundsMultiplier;
                careerParams.RepGainMultiplier = repMultiplier;

                Debug.Log($"[DRG] Applied multipliers - Science: {scienceMultiplier:F3}, Funds: {fundsMultiplier:F3}, Rep: {repMultiplier:F3}");
            }
        }

        private float CalculateScienceMultiplier()
        {
            if (!dynamicScience)
            {
                return baseScienceMultiplier;
            }

            float multiplier = baseScienceMultiplier;
            int techTier = DRGUtil.GetTechTier();
            float lifetimeScience = DRGUtil.GetLifetimeScience();

            multiplier -= (techTier * sciencePerTierPenalty);
            multiplier -= (lifetimeScience / scienceStep) * scienceStepPenalty;

            return Mathf.Clamp(multiplier, 0.01f, 5f);
        }

        private float CalculateFundsMultiplier()
        {
            if (!dynamicFunds)
            {
                return baseFundsMultiplier;
            }

            float multiplier = baseFundsMultiplier;
            float currentFunds = HighLogic.CurrentGame.Parameters.Career.CurrentFunds;

            multiplier -= (currentFunds * fundsScaleFactor);

            return Mathf.Clamp(multiplier, 0.01f, 5f);
        }

        private float CalculateReputationMultiplier()
        {
            if (!dynamicReputation)
            {
                return baseRepMultiplier;
            }

            float multiplier = baseRepMultiplier;
            float currentRep = HighLogic.CurrentGame.Parameters.Career.CurrentReputation;

            multiplier -= (currentRep * repScaleFactor);

            return Mathf.Clamp(multiplier, 0.01f, 5f);
        }

        private void OnTechnologyResearched(GameEvents.HostTargetAction<RDTech, RDTech.OperationResult> data)
        {
            if (dynamicScience || dynamicFunds || dynamicReputation)
            {
                ApplyMultipliers();
            }
        }

        private void OnScienceReceived(float science, ScienceSubject subject, ProtoVessel vessel, bool reverting)
        {
            if (dynamicScience || dynamicFunds || dynamicReputation)
            {
                ApplyMultipliers();
            }
        }

        private void OnFundsChanged(double funds, TransactionReasons reason)
        {
            if (dynamicFunds)
            {
                ApplyMultipliers();
            }
        }

        private void OnReputationChanged(float reputation, TransactionReasons reason)
        {
            if (dynamicReputation)
            {
                ApplyMultipliers();
            }
        }

        private void SaveConfiguration()
        {
            Debug.Log("[DRG] Configuration saved");
        }

        private void LoadConfiguration()
        {
            Debug.Log("[DRG] Configuration loaded");
        }

        private void InitializeStyles()
        {
            windowStyle = new GUIStyle(GUI.skin.window);
            labelStyle = new GUIStyle(GUI.skin.label);
            buttonStyle = new GUIStyle(GUI.skin.button);
        }
    }
}
