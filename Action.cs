namespace InputDisplay;

public class Action(string codeName, string displayName)
{
    public string DisplayName { get; init; } = displayName;
    public string CodeName { get; init; } = codeName;

    private Action(string name) : this(name, name) {}
    
    // Menu actions
    public static readonly Action Submit = new Action("Submit");
    public static readonly Action Cancel = new Action("Cancel");

    // Directions
    public static readonly Action Left = new Action("Left");
    public static readonly Action Right = new Action("Right");
    public static readonly Action Up = new Action("Up");
    public static readonly Action Down = new Action("Down");
    
    // Right stick directions
    public static readonly Action RsUp = new Action("RS_Up", "Right Stick Up");
    public static readonly Action RsDown = new Action("RS_Down", "Right Stick Down");
    public static readonly Action RsLeft = new Action("RS_Left", "Right Stick Left");
    public static readonly Action RsRight = new Action("RS_Right", "Right Stick Right");
    
    // Standard move kit
    public static readonly Action Jump = new Action("Jump");
    public static readonly Action Attack = new Action("Attack");
    public static readonly Action Dash = new Action("Dash");
    public static readonly Action SuperDash = new Action("Super Dash", "Crystal Dash");
    public static readonly Action DreamNail = new Action("Dream Nail");
    public static readonly Action Cast = new Action("Cast");
    public static readonly Action QuickCast = new Action("Quick Cast");
    
    // Miscellaneous
    public static readonly Action TextSpeedup = new Action("TextSpeedup", "Text Speedup");
    public static readonly Action SkipCutscene = new Action("SkipCutscene", "Skip Cutscene");
    public static readonly Action QuickMap = new Action("Quick Map", "Map");
    public static readonly Action OpenInventory = new Action("openInventory", "Inventory");
    public static readonly Action PaneRight = new Action("Pane Right");
    public static readonly Action PaneLeft = new Action("Pane Left");
    public static readonly Action Pause = new Action("Pause");

    public static readonly Action[] Actions =
    [
        Submit,
        Cancel,
        Left,
        Right,
        Up,
        Down,
        RsUp,
        RsDown,
        RsLeft,
        RsRight,
        Jump,
        Attack,
        Dash,
        SuperDash,
        DreamNail,
        Cast,
        QuickCast,
        TextSpeedup,
        SkipCutscene,
        QuickMap,
        OpenInventory,
        PaneRight,
        PaneLeft,
        Pause
    ];
}