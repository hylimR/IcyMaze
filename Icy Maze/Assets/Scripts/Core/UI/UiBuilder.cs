using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IcyMaze.UI
{
    /// Builds the runtime UI in code. Nothing here is wired in a scene file, which is what
    /// lets the remastered HUD and menus drop into the untouched 2015 scenes.
    public static class UiBuilder
    {
        public static readonly Color Ice = new Color(0.62f, 0.86f, 1f);
        public static readonly Color Frost = new Color(0.88f, 0.96f, 1f);
        public static readonly Color Dim = new Color(0.62f, 0.71f, 0.80f);
        public static readonly Color Cleared = new Color(0.55f, 0.94f, 0.71f);
        public static readonly Color Panel = new Color(0.04f, 0.07f, 0.12f, 0.72f);
        public static readonly Color Shade = new Color(0.02f, 0.04f, 0.08f, 0.86f);

        static Font font;
        static EventSystem persistentEventSystem;

        public static Font Font
        {
            get
            {
                if (font != null) return font;
                // Deliberately not ??: UnityEngine.Object overloads == to report
                // destroyed objects as null, and the null-coalescing operator skips it.
                font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                if (font == null) font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                if (font == null) Debug.LogWarning("[IcyMaze] No built-in font found; UI text will not render.");
                return font;
            }
        }

        public static Canvas CreateCanvas(Transform parent, string name, int sortingOrder, bool interactive)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler));
            go.transform.SetParent(parent, false);
            int uiLayer = LayerMask.NameToLayer("UI");
            if (uiLayer >= 0) go.layer = uiLayer;

            var canvas = go.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = sortingOrder;

            var scaler = go.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            if (interactive)
            {
                go.AddComponent<GraphicRaycaster>();
                EnsureEventSystem();
            }
            return canvas;
        }

        public static void EnsureEventSystem()
        {
            if (persistentEventSystem != null) return;

            var go = new GameObject("[Icy Maze EventSystem]", typeof(EventSystem), typeof(StandaloneInputModule));
            Object.DontDestroyOnLoad(go);
            persistentEventSystem = go.GetComponent<EventSystem>();
            ReconcileEventSystems();
        }

        /// The sigil trial ships its own EventSystem. Two live at once and Unity logs a
        /// duplicate warning every frame, so scene-local ones stand down.
        public static void ReconcileEventSystems()
        {
            if (persistentEventSystem == null) return;

            foreach (EventSystem system in Object.FindObjectsByType<EventSystem>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                if (system != persistentEventSystem) system.enabled = false;
            }
        }

        public static RectTransform Rect(Transform parent, string name, Vector2 anchorMin, Vector2 anchorMax,
                                         Vector2 pivot, Vector2 offset, Vector2 size)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            var rect = (RectTransform)go.transform;
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.pivot = pivot;
            rect.anchoredPosition = offset;
            rect.sizeDelta = size;
            return rect;
        }

        public static Image Backdrop(Transform parent, string name, Color color)
        {
            var rect = Rect(parent, name, Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f), Vector2.zero, Vector2.zero);
            var image = rect.gameObject.AddComponent<Image>();
            image.color = color;
            return image;
        }

        public static Text Label(Transform parent, string name, string content, int size, TextAnchor anchor, Color color)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            var text = go.AddComponent<Text>();
            text.font = Font;
            text.fontSize = size;
            text.text = content;
            text.alignment = anchor;
            text.color = color;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.raycastTarget = false;
            text.supportRichText = true;
            return text;
        }

        public static Button TextButton(Transform parent, string label, float width, float height,
                                        UnityEngine.Events.UnityAction onClick)
        {
            var rect = Rect(parent, label + " Button", new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
                            new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(width, height));

            var image = rect.gameObject.AddComponent<Image>();
            image.color = Color.white;

            var button = rect.gameObject.AddComponent<Button>();
            button.targetGraphic = image;
            var colors = button.colors;
            colors.normalColor = new Color(0.10f, 0.18f, 0.28f, 0.95f);
            colors.highlightedColor = new Color(0.20f, 0.36f, 0.54f, 0.98f);
            colors.pressedColor = new Color(0.32f, 0.56f, 0.78f, 1f);
            colors.selectedColor = new Color(0.16f, 0.28f, 0.42f, 0.98f);
            colors.disabledColor = new Color(0.10f, 0.13f, 0.17f, 0.6f);
            colors.colorMultiplier = 1f;
            colors.fadeDuration = 0.08f;
            button.colors = colors;
            if (onClick != null) button.onClick.AddListener(onClick);

            var text = Label(rect, "Text", label, 28, TextAnchor.MiddleCenter, Frost);
            Stretch(text.rectTransform);

            var layout = rect.gameObject.AddComponent<LayoutElement>();
            layout.preferredWidth = width;
            layout.preferredHeight = height;
            layout.flexibleWidth = 0f;
            return button;
        }

        public static void Stretch(RectTransform rect)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }


        /// Anchors a rect to the top-centre of its parent at a given drop and size.
        public static void PlaceTop(RectTransform rect, float drop, Vector2 size)
        {
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.anchoredPosition = new Vector2(0f, -drop);
            rect.sizeDelta = size;
        }

        public static Slider HorizontalSlider(Transform parent, string name, float width, float height)
        {
            RectTransform rect = Rect(parent, name, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f),
                                      new Vector2(0.5f, 1f), Vector2.zero, new Vector2(width, height));

            RectTransform background = Rect(rect, "Background", new Vector2(0f, 0.25f), new Vector2(1f, 0.75f),
                                            new Vector2(0.5f, 0.5f), Vector2.zero, Vector2.zero);
            var backgroundImage = background.gameObject.AddComponent<Image>();
            backgroundImage.color = new Color(0.08f, 0.13f, 0.20f, 0.95f);

            RectTransform fillArea = Rect(rect, "Fill Area", new Vector2(0f, 0.25f), new Vector2(1f, 0.75f),
                                          new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(-height, 0f));
            RectTransform fill = Rect(fillArea, "Fill", new Vector2(0f, 0f), new Vector2(0f, 1f),
                                      new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(height, 0f));
            var fillImage = fill.gameObject.AddComponent<Image>();
            fillImage.color = Ice;

            RectTransform handleArea = Rect(rect, "Handle Slide Area", Vector2.zero, Vector2.one,
                                            new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(-height, 0f));
            RectTransform handle = Rect(handleArea, "Handle", new Vector2(0f, 0f), new Vector2(0f, 1f),
                                        new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(height, 0f));
            var handleImage = handle.gameObject.AddComponent<Image>();
            handleImage.color = Frost;

            var slider = rect.gameObject.AddComponent<Slider>();
            slider.fillRect = fill;
            slider.handleRect = handle;
            slider.targetGraphic = handleImage;
            slider.direction = Slider.Direction.LeftToRight;
            slider.minValue = 0f;
            slider.maxValue = 1f;
            return slider;
        }

        public static VerticalLayoutGroup Column(Transform parent, string name, float spacing, RectOffset padding)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            var group = go.AddComponent<VerticalLayoutGroup>();
            group.spacing = spacing;
            group.padding = padding;
            group.childAlignment = TextAnchor.UpperCenter;
            group.childControlWidth = true;
            group.childControlHeight = true;
            group.childForceExpandWidth = true;
            group.childForceExpandHeight = false;
            return group;
        }
    }
}
