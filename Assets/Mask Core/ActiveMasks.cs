[System.Flags]
public enum ActiveMasks
{
    NONE = 1 << 0,
    RedBlueMask = 1 << 1, // on: red active, blue inactive ---- off: red: inactive, blue: active
    FIND = 1 << 2
}