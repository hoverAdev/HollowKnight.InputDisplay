using System.Collections.Generic;
using System.Reflection;
using GlobalEnums;
using MagicUI.Core;
using MagicUI.Elements;
using Modding;
using static InputDisplay.Action;
using GM = On.GameManager;

namespace InputDisplay;

/// <summary>
/// Shows a display of the current inputs.
/// Implementation is loosely based off of
/// <a href="https://github.com/BadMagic100/RandoChecksCounter">RandoChecksCounter</a>.
/// </summary>
/// <seealso cref="HeroActions"/>

// ReSharper disable once UnusedType.Global (Top-level mod declaration)
internal class InputDisplay() : Mod("Input Display"), ITogglableMod
{
    // This object
    internal static InputDisplay Instance { get; private set; }
    
    // Curated list of inputs to listen for
    private static readonly Action[] Actions =
    [
        // Submit and Cancel are menu actions, not gameplay actions, so they have been cut.
        Left,
        Right,
        Up,
        Down,
        // The RS_ actions have been cut for simplicity. In a future graphical version, they could be added back in.
        Jump,
        Attack,
        // Evade has no bindings, and bindings cannot be set via menu.
        Dash,
        SuperDash,
        DreamNail,
        Cast,
        // Despite the name, the Focus action does nothing. The Cast action is used instead.
        QuickMap,
        QuickCast,
        // TextSpeedup and SkipCutscene are too infrequent to be shown constantly, and are somewhat covered by other actions.
        OpenInventory,
        // Are the Pane actions used? I imagine not, as their bindings (L1/L2/R1/R2) correlate to other actions as well.
        Pause
    ];

    // MagicUI elements
    private LayoutRoot _layout;
    private StackLayout _stack;
    private Dictionary<string, TextFormatter<bool>> _textFormatters = new();

    // Current version of the 
    public override string GetVersion()
    {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }

    // Run when the game loads 
    public override void Initialize()
    {
        Instance = this;

        _layout = new LayoutRoot(true, "Input Display");
        _stack = new StackLayout(_layout)
        {
            Padding = new Padding(10),
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Bottom,
        };

        foreach (var action in Actions)
        {
            var child = new TextObject(_layout, action.DisplayName);
            var text = new TextFormatter<bool>(_layout, false,
                pressed => $"{action.DisplayName} pressed: {pressed}")
            {
                Text = child
            };

            _stack.Children.Add(child);

            Log($"Adding _textFormatters[{action.CodeName}]");
            _textFormatters[action.CodeName] = text;
        }

        ModHooks.HeroUpdateHook += Process;

        GM.SetState += (orig, self, state) =>
        {
            _layout.Opacity = (state == GameState.MAIN_MENU) ? 0 : 1;

            orig(self, state);
        };
    }

    private void Process()
    {
        foreach (var action in InputHandler.Instance.inputActions.Actions)
        {
            // Ignore actions we're not checking for
            if (_textFormatters.TryGetValue(action.Name, out var formatter))
                formatter.Data = action.IsPressed;
        }
    }

    public void Unload()
    {
        foreach (var textFormatter in _textFormatters.Values)
        {
            textFormatter.Destroy();
        }

        _textFormatters = null;

        _stack.Destroy();
        _stack = null;

        _layout.Destroy();
        _layout = null;
    }
}