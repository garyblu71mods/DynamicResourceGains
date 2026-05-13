using UnityEngine;
using KSP.UI.Screens;
using System;

namespace Dynamic_Resource_Gains
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class DynamicResourceGains : MonoBehaviour
    {
        public const string Name = "Dynamic Resource Gains";
        public const string Version = "1.0.0";

        private ApplicationLauncherButton toolbarButton;
        private Rect windowRect = new Rect(0, 0, 588, 680);
        private Rect manualWindowRect = new Rect(0, 0, 588, 672);
        private bool showWindow = false;
        private bool showManualWindow = false;
        private bool windowsPositioned = false;
        private string hoveredTooltip = string.Empty;
        private GUIStyle tooltipStyle;

        private string scienceMultiplierStr = "100";
        private string fundsMultiplierStr = "100";
        private string reputationMultiplierStr = "100";

        private bool dynamicScience = true;
        private string sciencePerTierStr = "2";
        private string scienceStepStr = "3000";
        private string scienceStepPenaltyStr = "0.1";

        private bool dynamicFunds = true;
        private string fundsScaleFactorStr = "2";
        private string fundsStepStr = "100000";

        private bool dynamicReputation = true;
        private string repScaleFactorStr = "0.1";
        private string repStepStr = "100";

        private DRGScenario scenarioModule;

        public void Awake()
        {
        }

        public void Start()
        {
            scenarioModule = FindObjectOfType<DRGScenario>();
            if (scenarioModule == null)
            {
                Debug.LogError("[DRG] DRGScenario not found!");
            }
            else
            {
                LoadSettings();
            }

            GameEvents.OnTechnologyResearched.Add(OnTechnologyResearched);
            GameEvents.onGUIApplicationLauncherReady.Add(OnAppLauncherReady);
            GameEvents.onGUIApplicationLauncherUnreadifying.Add(OnAppLauncherUnreadifying);

            if (HighLogic.CurrentGame != null && HighLogic.CurrentGame.Mode == Game.Modes.CAREER && ApplicationLauncher.Ready)
            {
                AddToolbarButton();
            }
        }

        private void OnAppLauncherReady()
        {
            if (HighLogic.CurrentGame != null && HighLogic.CurrentGame.Mode == Game.Modes.CAREER)
            {
                AddToolbarButton();
            }
        }

        private void AddToolbarButton()
        {
            if (toolbarButton != null || ApplicationLauncher.Instance == null)
            {
                return;
            }

            Texture2D buttonTexture = CreateGraphIconTexture();

            toolbarButton = ApplicationLauncher.Instance.AddModApplication(
                OnButtonToggleOn,
                OnButtonToggleOff,
                null,
                null,
                null,
                null,
                ApplicationLauncher.AppScenes.SPACECENTER,
                buttonTexture);
        }

        private Texture2D CreateGraphIconTexture()
        {
            const int size = 32;
            Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;

            Color clear = new Color(0f, 0f, 0f, 0f);
            Color background = new Color(0.08f, 0.10f, 0.14f, 1f);
            Color border = new Color(0.18f, 0.22f, 0.30f, 1f);
            Color grid = new Color(0.16f, 0.20f, 0.26f, 1f);
            Color axis = new Color(0.70f, 0.74f, 0.82f, 1f);
            Color line = new Color(0.20f, 0.95f, 0.55f, 1f);
            Color point = new Color(0.95f, 0.95f, 0.98f, 1f);

            Color[] pixels = new Color[size * size];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = clear;
            }
            texture.SetPixels(pixels);

            for (int y = 2; y < size - 2; y++)
            {
                for (int x = 2; x < size - 2; x++)
                {
                    texture.SetPixel(x, y, background);
                }
            }

            for (int x = 1; x < size - 1; x++)
            {
                texture.SetPixel(x, 1, border);
                texture.SetPixel(x, size - 2, border);
            }
            for (int y = 1; y < size - 1; y++)
            {
                texture.SetPixel(1, y, border);
                texture.SetPixel(size - 2, y, border);
            }

            for (int x = 7; x < 26; x += 6)
            {
                DrawLine(texture, x, 5, x, 25, grid);
            }
            for (int y = 7; y < 26; y += 6)
            {
                DrawLine(texture, 5, y, 25, y, grid);
            }

            DrawLine(texture, 5, 5, 5, 26, axis);
            DrawLine(texture, 5, 5, 26, 5, axis);

            Vector2[] points = new[]
            {
                new Vector2(6f, 8f),
                new Vector2(10f, 10f),
                new Vector2(14f, 13f),
                new Vector2(18f, 17f),
                new Vector2(22f, 20f),
                new Vector2(26f, 25f)
            };

            for (int i = 0; i < points.Length - 1; i++)
            {
                DrawLine(texture, Mathf.RoundToInt(points[i].x), Mathf.RoundToInt(points[i].y), Mathf.RoundToInt(points[i + 1].x), Mathf.RoundToInt(points[i + 1].y), line);
            }

            for (int i = 0; i < points.Length; i++)
            {
                DrawFilledCircle(texture, Mathf.RoundToInt(points[i].x), Mathf.RoundToInt(points[i].y), 1, point);
            }

            texture.Apply();
            return texture;
        }

        private void DrawLine(Texture2D texture, int x0, int y0, int x1, int y1, Color color)
        {
            int dx = Mathf.Abs(x1 - x0);
            int dy = Mathf.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                SetPixelSafe(texture, x0, y0, color);
                if (x0 == x1 && y0 == y1)
                {
                    break;
                }

                int e2 = err * 2;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }

        private void DrawFilledCircle(Texture2D texture, int centerX, int centerY, int radius, Color color)
        {
            for (int y = -radius; y <= radius; y++)
            {
                for (int x = -radius; x <= radius; x++)
                {
                    if (x * x + y * y <= radius * radius)
                    {
                        SetPixelSafe(texture, centerX + x, centerY + y, color);
                    }
                }
            }
        }

        private void SetPixelSafe(Texture2D texture, int x, int y, Color color)
        {
            if (x >= 0 && x < texture.width && y >= 0 && y < texture.height)
            {
                texture.SetPixel(x, y, color);
            }
        }

        private void OnButtonToggleOn()
        {
            windowsPositioned = false;
            showWindow = true;
        }

        private void OnButtonToggleOff()
        {
            showWindow = false;
            showManualWindow = false;
        }

        private void OnAppLauncherUnreadifying(GameScenes scene)
        {
            if (toolbarButton != null && ApplicationLauncher.Instance != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(toolbarButton);
                toolbarButton = null;
            }
        }

        public void OnGUI()
        {
            if (!showWindow || HighLogic.LoadedScene != GameScenes.SPACECENTER)
            {
                return;
            }

            if (!windowsPositioned)
            {
                PositionWindows();
                windowsPositioned = true;
            }

            GUI.skin = HighLogic.Skin;
            hoveredTooltip = string.Empty;
            EnsureStyles();

            windowRect = GUI.Window(12345, windowRect, DrawWindow, Name + " v" + Version);

            if (showManualWindow)
            {
                manualWindowRect = GUI.Window(12346, manualWindowRect, DrawManualWindow, "Dynamic Resource Gains - Manual");
            }

            string tooltip = !string.IsNullOrEmpty(GUI.tooltip) ? GUI.tooltip : hoveredTooltip;
            if (!string.IsNullOrEmpty(tooltip))
            {
                DrawTooltip(Event.current.mousePosition, tooltip);
            }
        }

        private void PositionWindows()
        {
            float margin = 20f;
            windowRect.x = Mathf.Max(margin, Screen.width - windowRect.width - margin);
            windowRect.y = 55f;

            manualWindowRect.x = Mathf.Max(margin, windowRect.x + windowRect.width - manualWindowRect.width);
            manualWindowRect.y = Mathf.Max(margin, Screen.height - manualWindowRect.height - margin);
        }

        private void DrawWindow(int id)
        {
            GUILayout.BeginVertical();
            DrawCurrentProgressSection();
            GUILayout.Space(6);

            GUILayout.Label("Career Multipliers", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 17 });
            GUILayout.Space(3);

            scienceMultiplierStr = DrawLabeledTextField("Sci:", scienceMultiplierStr, "Base Science gain in percent. 100 = normal, 200 = double, 50 = half.", "Base Science gain in percent. 100 = normal, 200 = double, 50 = half.", 70f, "%");
            fundsMultiplierStr = DrawLabeledTextField("$:", fundsMultiplierStr, "Base Funds gain in percent. 100 = normal, 200 = double, 50 = half.", "Base Funds gain in percent. 100 = normal, 200 = double, 50 = half.", 70f, "%");
            reputationMultiplierStr = DrawLabeledTextField("Rep:", reputationMultiplierStr, "Base Reputation gain in percent. 100 = normal, 200 = double, 50 = half.", "Base Reputation gain in percent. 100 = normal, 200 = double, 50 = half.", 70f, "%");

            GUILayout.Space(4);

            dynamicScience = DrawToggle(dynamicScience, "Dynamic Sci", "Turns dynamic Science scaling on or off.");
            if (dynamicScience)
            {
                sciencePerTierStr = DrawLabeledTextField("Tier:", sciencePerTierStr, "Penalty applied for each unlocked tech tier.", "Penalty applied for each unlocked tech tier.", 70f, "%");
                scienceStepStr = DrawLabeledTextField("Step:", scienceStepStr, "Every this much lifetime Science adds one extra penalty step.", "Every this much lifetime Science adds one extra penalty step.", 70f, string.Empty);
                scienceStepPenaltyStr = DrawLabeledTextField("Pnl:", scienceStepPenaltyStr, "Additional penalty applied for each Science step.", "Additional penalty applied for each Science step.", 70f, "%");
            }

            dynamicFunds = DrawToggle(dynamicFunds, "Dynamic $", "Turns dynamic Funds scaling on or off.");
            if (dynamicFunds)
            {
                fundsStepStr = DrawLabeledTextField("Step:", fundsStepStr, "After this many funds, one penalty step is counted.", "After this many funds, one penalty step is counted.", 70f, string.Empty);
                fundsScaleFactorStr = DrawLabeledTextField("Pnl:", fundsScaleFactorStr, "Penalty in percent for each funds step.", "Penalty in percent for each funds step.", 70f, "%");
            }

            dynamicReputation = DrawToggle(dynamicReputation, "Dynamic Rep", "Turns dynamic Reputation scaling on or off.");
            if (dynamicReputation)
            {
                repStepStr = DrawLabeledTextField("Step:", repStepStr, "After this much reputation, one penalty step is counted.", "After this much reputation, one penalty step is counted.", 70f, string.Empty);
                repScaleFactorStr = DrawLabeledTextField("Pnl:", repScaleFactorStr, "Penalty in percent for each reputation step.", "Penalty in percent for each reputation step.", 70f, "%");
            }

            GUILayout.Space(2);
            GUILayout.BeginHorizontal();
            if (DrawButton("Apply", "Apply values for the current session without saving.", 70f))
            {
                ApplyMultipliers();
            }
            if (DrawButton("Save", "Save values so they stay after reloading the save.", 70f))
            {
                SaveSettings();
            }
            if (DrawButton("Reset", "Restore the default values in the form.", 70f))
            {
                ResetToDefaults();
            }
            if (DrawButton("Manual", "Open the in-game manual window with usage instructions.", 70f))
            {
                showManualWindow = !showManualWindow;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUI.DragWindow(new Rect(0f, 0f, windowRect.width, 24f));
        }

        private void OnTechnologyResearched(GameEvents.HostTargetAction<RDTech, RDTech.OperationResult> data)
        {
            ApplyMultipliers();
        }

        private void DrawCurrentProgressSection()
        {
            GUIStyle headerStyle = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 17 };
            GUIStyle cellStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 15 };
            GUIStyle rowLabelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontSize = 15 };

            // Draw header with close button
            GUILayout.BeginHorizontal();
            GUILayout.Label("Current Status", headerStyle, GUILayout.ExpandWidth(true));
            if (GUILayout.Button("X", GUILayout.Width(28), GUILayout.Height(28)))
            {
                showWindow = false;
            }
            GUILayout.EndHorizontal();

            int techTier = DRGUtil.GetTechTier();
            float lifetimeScience = DRGUtil.GetLifetimeScience();
            float currentReputation = Reputation.Instance != null ? Reputation.Instance.reputation : 0f;
            float currentFunds = Funding.Instance != null ? (float)Funding.Instance.Funds : 0f;

            float perTierPenaltyPercent = dynamicScience ? ParsePercent(sciencePerTierStr, 2f) : 0f;
            float stepPenaltyPercent = dynamicScience ? ParsePercent(scienceStepPenaltyStr, 0.1f) : 0f;
            float scienceStepValue = ParseFloatValue(scienceStepStr, 3000f);
            float fundsStepValue = ParseFloatValue(fundsStepStr, 100000f);
            float fundsPenaltyPerStepPercent = dynamicFunds ? ParsePercent(fundsScaleFactorStr, 2f) : 0f;
            float repStepValue = ParseFloatValue(repStepStr, 100f);
            float repPenaltyPerStepPercent = dynamicReputation ? ParsePercent(repScaleFactorStr, 0.1f) : 0f;

            float scienceSteps = scienceStepValue > 0f ? lifetimeScience / scienceStepValue : 0f;
            float tierPenaltyNow = dynamicScience ? techTier * perTierPenaltyPercent : 0f;
            float sciencePenaltyNow = dynamicScience ? scienceSteps * stepPenaltyPercent : 0f;
            float fundsPenaltyNow = dynamicFunds && fundsStepValue > 0f ? (currentFunds / fundsStepValue) * fundsPenaltyPerStepPercent : 0f;
            float reputationPenaltyNow = dynamicReputation && repStepValue > 0f ? (currentReputation / repStepValue) * repPenaltyPerStepPercent : 0f;

            // Calculate effective multipliers
            float baseSciencePercent = ParsePercent(scienceMultiplierStr, 100f);
            float baseFundsPercent = ParsePercent(fundsMultiplierStr, 100f);
            float baseReputationPercent = ParsePercent(reputationMultiplierStr, 100f);

            float effectiveSciencePercent = baseSciencePercent - tierPenaltyNow - sciencePenaltyNow;
            float effectiveFundsPercent = baseFundsPercent - fundsPenaltyNow;
            float effectiveReputationPercent = baseReputationPercent - reputationPenaltyNow;

            GUILayout.Space(3);

            // Table Header
            GUILayout.BeginHorizontal();
            GUILayout.Label("", cellStyle, GUILayout.Width(56));
            DrawTableCell("Sci", cellStyle, 84);
            DrawTableCell("$", cellStyle, 84);
            DrawTableCell("Rep", cellStyle, 84);
            GUILayout.EndHorizontal();

            // Row 1: Base Multipliers (%)
            GUILayout.BeginHorizontal();
            GUILayout.Label("Base", rowLabelStyle, GUILayout.Width(56));
            DrawTableCell(baseSciencePercent.ToString("F0") + "%", cellStyle, 84);
            DrawTableCell(baseFundsPercent.ToString("F0") + "%", cellStyle, 84);
            DrawTableCell(baseReputationPercent.ToString("F0") + "%", cellStyle, 84);
            GUILayout.EndHorizontal();

            // Row 2: Penalties (%)
            GUILayout.BeginHorizontal();
            GUILayout.Label("Pnl", rowLabelStyle, GUILayout.Width(56));
            DrawTableCell("-" + tierPenaltyNow.ToString("F1") + "% -" + sciencePenaltyNow.ToString("F1") + "%", cellStyle, 84);
            DrawTableCell("-" + fundsPenaltyNow.ToString("F1") + "%", cellStyle, 84);
            DrawTableCell("-" + reputationPenaltyNow.ToString("F1") + "%", cellStyle, 84);
            GUILayout.EndHorizontal();

            // Row 3: Effective Multipliers (%)
            GUILayout.BeginHorizontal();
            GUILayout.Label("Eff", rowLabelStyle, GUILayout.Width(56));
            DrawTableCell(Mathf.Max(0f, effectiveSciencePercent).ToString("F0") + "%", cellStyle, 84);
            DrawTableCell(Mathf.Max(0f, effectiveFundsPercent).ToString("F0") + "%", cellStyle, 84);
            DrawTableCell(Mathf.Max(0f, effectiveReputationPercent).ToString("F0") + "%", cellStyle, 84);
            GUILayout.EndHorizontal();

            GUILayout.Space(3);
        }

        private void DrawTableCell(string content, GUIStyle style, float width)
        {
            GUILayout.Label(content, style, GUILayout.Width(width), GUILayout.Height(28));
        }

        private float ParsePercent(string value, float fallback)
        {
            float parsed;
            return float.TryParse(value, out parsed) ? parsed : fallback;
        }

        private float ParseFloatValue(string value, float fallback)
        {
            float parsed;
            return float.TryParse(value, out parsed) ? parsed : fallback;
        }

        private void DrawManualWindow(int id)
        {
            GUILayout.BeginVertical();

            GUILayout.Label("Manual - Quick Reference", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 17 });
            GUILayout.Space(4);

            // Base Multipliers
            GUILayout.Label("BASE MULTIPLIERS:", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 15 });
            GUILayout.Label("100%=normal, 200%=double, 50%=half", new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Height(25));

            // Dynamic Science
            GUILayout.Label("DYNAMIC SCIENCE:", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 15 });
            GUILayout.Label("Per Tier: loss % per tier (def: 2%)", new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Height(25));
            GUILayout.Label("Step Size: science per step (def: 3000)", new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Height(25));
            GUILayout.Label("Per Step: loss % per step (def: 0.1%)", new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Height(25));

            // Dynamic Funds
            GUILayout.Label("DYNAMIC FUNDS:", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 15 });
            GUILayout.Label("Step Size: funds per step (def: 100k)", new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Height(25));
            GUILayout.Label("Per Step: loss % per step (def: 2%)", new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Height(25));

            // Dynamic Reputation
            GUILayout.Label("DYNAMIC REPUTATION:", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 15 });
            GUILayout.Label("Step Size: reputation per step (def: 100)", new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Height(25));
            GUILayout.Label("Per Step: loss % per step (def: 0.1%)", new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Height(25));

            // How to use
            GUILayout.Label("HOW TO USE:", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 15 });
            GUILayout.Label("1. Check 'Current Status' for effective %", new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Height(25));
            GUILayout.Label("2. Set values → 3. APPLY (test) →", new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Height(25));
            GUILayout.Label("4. SAVE (keep) → Verify in Current", new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Height(25));

            GUILayout.Space(4);
            GUILayout.Label("Career Mode + Space Center only!", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Italic, fontSize = 14 });

            GUILayout.EndVertical();

            // Draw close button in top right corner
            Rect closeButtonRect = new Rect(manualWindowRect.width - 28f, 4f, 24f, 24f);
            if (GUI.Button(closeButtonRect, "X", new GUIStyle(GUI.skin.button) { fontSize = 15 }))
            {
                showManualWindow = false;
            }

            GUI.DragWindow(new Rect(0f, 0f, manualWindowRect.width - 28f, 24f));
        }

        private void ResetToDefaults()
        {
            scienceMultiplierStr = "100";
            fundsMultiplierStr = "100";
            reputationMultiplierStr = "100";

            dynamicScience = true;
            sciencePerTierStr = "2";
            scienceStepStr = "3000";
            scienceStepPenaltyStr = "0.1";

            dynamicFunds = true;
            fundsStepStr = "100000";
            fundsScaleFactorStr = "2";

            dynamicReputation = true;
            repStepStr = "100";
            repScaleFactorStr = "0.1";
        }

        private string DrawLabeledTextField(string label, string value, string tooltip, string tooltipAlt, float labelWidth, string suffix)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Width(labelWidth));
            value = GUILayout.TextField(value, new GUIStyle(GUI.skin.textField) { fontSize = 15 }, GUILayout.MinWidth(84), GUILayout.MaxWidth(140));
            if (!string.IsNullOrEmpty(suffix))
            {
                GUILayout.Label(suffix, new GUIStyle(GUI.skin.label) { fontSize = 15 }, GUILayout.Width(21));
            }
            GUILayout.EndHorizontal();
            TrackLastRectTooltip(tooltip);
            return value;
        }

        private bool DrawToggle(bool value, string text, string tooltip)
        {
            bool result = GUILayout.Toggle(value, new GUIContent(text, tooltip), new GUIStyle(GUI.skin.toggle) { fontSize = 15 });
            TrackLastRectTooltip(tooltip);
            return result;
        }

        private bool DrawButton(string text, string tooltip, float width)
        {
            bool clicked = GUILayout.Button(new GUIContent(text, tooltip), new GUIStyle(GUI.skin.button) { fontSize = 15 }, GUILayout.Width(width));
            TrackLastRectTooltip(tooltip);
            return clicked;
        }

        private void TrackLastRectTooltip(string tooltip)
        {
            Rect rect = GUILayoutUtility.GetLastRect();
            if (rect.Contains(Event.current.mousePosition))
            {
                hoveredTooltip = tooltip;
            }
        }

        private void EnsureStyles()
        {
            if (tooltipStyle == null)
            {
                tooltipStyle = new GUIStyle(HighLogic.Skin.box);
                tooltipStyle.alignment = TextAnchor.UpperLeft;
                tooltipStyle.wordWrap = true;
                tooltipStyle.padding = new RectOffset(8, 8, 6, 6);
            }
        }

        private void DrawTooltip(Vector2 mousePosition, string tooltip)
        {
            float maxWidth = 280f;
            GUIContent content = new GUIContent(tooltip);
            float height = tooltipStyle.CalcHeight(content, maxWidth);
            Rect tooltipRect = new Rect(mousePosition.x + 16f, mousePosition.y + 16f, maxWidth, height + 4f);
            GUI.Box(tooltipRect, content, tooltipStyle);
        }

        private void ApplyMultipliers()
        {
            try
            {
                float science = float.Parse(scienceMultiplierStr) / 100f;
                float funds = float.Parse(fundsMultiplierStr) / 100f;
                float reputation = float.Parse(reputationMultiplierStr) / 100f;

                if (scenarioModule != null)
                {
                    scenarioModule.BaseScienceMultiplier = DRGUtil.ClampMultiplier(science);
                    scenarioModule.BaseFundsMultiplier = DRGUtil.ClampMultiplier(funds);
                    scenarioModule.BaseReputationMultiplier = DRGUtil.ClampMultiplier(reputation);
                    scenarioModule.DynamicScience = dynamicScience;
                    scenarioModule.DynamicFunds = dynamicFunds;
                    scenarioModule.DynamicReputation = dynamicReputation;

                    if (dynamicScience)
                    {
                        scenarioModule.SciencePerTierPenalty = float.Parse(sciencePerTierStr) / 100f;
                        scenarioModule.ScienceStep = float.Parse(scienceStepStr);
                        scenarioModule.ScienceStepPenalty = float.Parse(scienceStepPenaltyStr) / 100f;
                    }

                    if (dynamicFunds)
                    {
                        scenarioModule.FundsStep = float.Parse(fundsStepStr);
                        scenarioModule.FundsScaleFactor = float.Parse(fundsScaleFactorStr) / 100f;
                    }

                    if (dynamicReputation)
                    {
                        scenarioModule.RepStep = float.Parse(repStepStr);
                        scenarioModule.RepScaleFactor = float.Parse(repScaleFactorStr) / 100f;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[DRG] Error applying multipliers: " + e.Message);
            }
        }

        private void SaveSettings()
        {
            if (scenarioModule != null)
            {
                ApplyMultipliers();
                scenarioModule.SaveMultipliers();
            }
        }

        private void LoadSettings()
        {
            if (scenarioModule == null)
            {
                return;
            }

            scenarioModule.LoadMultipliers();
            scienceMultiplierStr = (scenarioModule.BaseScienceMultiplier * 100f).ToString("F0");
            fundsMultiplierStr = (scenarioModule.BaseFundsMultiplier * 100f).ToString("F0");
            reputationMultiplierStr = (scenarioModule.BaseReputationMultiplier * 100f).ToString("F0");
            dynamicScience = scenarioModule.DynamicScience;
            dynamicFunds = scenarioModule.DynamicFunds;
            dynamicReputation = scenarioModule.DynamicReputation;
            sciencePerTierStr = (scenarioModule.SciencePerTierPenalty * 100f).ToString("F2");
            scienceStepStr = scenarioModule.ScienceStep.ToString("F0");
            scienceStepPenaltyStr = (scenarioModule.ScienceStepPenalty * 100f).ToString("F3");
            fundsStepStr = scenarioModule.FundsStep.ToString("F0");
            fundsScaleFactorStr = (scenarioModule.FundsScaleFactor * 100f).ToString("F2");
            repStepStr = scenarioModule.RepStep.ToString("F0");
            repScaleFactorStr = (scenarioModule.RepScaleFactor * 100f).ToString("F2");
        }

        public void OnDestroy()
        {
            GameEvents.OnTechnologyResearched.Remove(OnTechnologyResearched);
            GameEvents.onGUIApplicationLauncherReady.Remove(OnAppLauncherReady);
            GameEvents.onGUIApplicationLauncherUnreadifying.Remove(OnAppLauncherUnreadifying);

            if (toolbarButton != null && ApplicationLauncher.Instance != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(toolbarButton);
            }
        }
    }
}
