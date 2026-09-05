using UnityEngine.Events;

namespace UnityEngine.UI
{
    public class Graphic : UnityEngine.EventSystems.UIBehaviour
    {
        public Color color { get; set; }
        public bool raycastTarget { get; set; }
        public RectTransform rectTransform => null;
    }

    public class MaskableGraphic : Graphic { }

    public class Image : MaskableGraphic
    {
        public Sprite sprite { get; set; }
        public Type type { get; set; }

        public enum Type { Simple, Sliced, Tiled, Filled }
    }

    public class RawImage : MaskableGraphic
    {
        public Texture texture { get; set; }
    }

    public class Sprite : Object { }

    public enum HorizontalWrapMode { Wrap, Overflow }

    public enum VerticalWrapMode { Truncate, Overflow }

    public class Text : MaskableGraphic
    {
        public string text { get; set; }
        public Font font { get; set; }
        public int fontSize { get; set; }
        public FontStyle fontStyle { get; set; }
        public TextAnchor alignment { get; set; }
        public bool supportRichText { get; set; }
        public HorizontalWrapMode horizontalOverflow { get; set; }
        public VerticalWrapMode verticalOverflow { get; set; }
        public float lineSpacing { get; set; }
    }

    public struct ColorBlock
    {
        public Color normalColor { get; set; }
        public Color highlightedColor { get; set; }
        public Color pressedColor { get; set; }
        public Color selectedColor { get; set; }
        public Color disabledColor { get; set; }
        public float colorMultiplier { get; set; }
        public float fadeDuration { get; set; }
    }

    public class Selectable : UnityEngine.EventSystems.UIBehaviour
    {
        public bool interactable { get; set; }
        public Graphic targetGraphic { get; set; }
        public ColorBlock colors { get; set; }
    }

    public class Button : Selectable
    {
        public ButtonClickedEvent onClick => null;

        public class ButtonClickedEvent : UnityEvent { }
    }

    public class Slider : Selectable
    {
        public enum Direction { LeftToRight, RightToLeft, BottomToTop, TopToBottom }

        public float value { get; set; }
        public float minValue { get; set; }
        public float maxValue { get; set; }
        public bool wholeNumbers { get; set; }
        public Direction direction { get; set; }
        public RectTransform fillRect { get; set; }
        public RectTransform handleRect { get; set; }
        public SliderEvent onValueChanged => null;

        public class SliderEvent : UnityEvent<float> { }
    }

    public class GraphicRaycaster : UnityEngine.EventSystems.UIBehaviour { }

    public class CanvasScaler : UnityEngine.EventSystems.UIBehaviour
    {
        public enum ScaleMode { ConstantPixelSize, ScaleWithScreenSize, ConstantPhysicalSize }

        public enum ScreenMatchMode { MatchWidthOrHeight, Expand, Shrink }

        public ScaleMode uiScaleMode { get; set; }
        public Vector2 referenceResolution { get; set; }
        public ScreenMatchMode screenMatchMode { get; set; }
        public float matchWidthOrHeight { get; set; }
    }

    public class LayoutElement : UnityEngine.EventSystems.UIBehaviour
    {
        public float preferredWidth { get; set; }
        public float preferredHeight { get; set; }
        public float minWidth { get; set; }
        public float minHeight { get; set; }
        public float flexibleWidth { get; set; }
        public float flexibleHeight { get; set; }
    }

    public class LayoutGroup : UnityEngine.EventSystems.UIBehaviour
    {
        public RectOffset padding { get; set; }
        public TextAnchor childAlignment { get; set; }
    }

    public class HorizontalOrVerticalLayoutGroup : LayoutGroup
    {
        public float spacing { get; set; }
        public bool childControlWidth { get; set; }
        public bool childControlHeight { get; set; }
        public bool childForceExpandWidth { get; set; }
        public bool childForceExpandHeight { get; set; }
    }

    public class VerticalLayoutGroup : HorizontalOrVerticalLayoutGroup { }

    public class HorizontalLayoutGroup : HorizontalOrVerticalLayoutGroup { }

    public class ContentSizeFitter : UnityEngine.EventSystems.UIBehaviour
    {
        public enum FitMode { Unconstrained, MinSize, PreferredSize }

        public FitMode horizontalFit { get; set; }
        public FitMode verticalFit { get; set; }
    }

    public class AspectRatioFitter : UnityEngine.EventSystems.UIBehaviour
    {
        public enum AspectMode { None, WidthControlsHeight, HeightControlsWidth, FitInParent, EnvelopeParent }

        public AspectMode aspectMode { get; set; }
        public float aspectRatio { get; set; }
    }
}
