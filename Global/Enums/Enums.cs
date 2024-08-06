namespace Global.Enums
{
    /// <summary>
    /// Status' for the config pages if they are in view mode, creation mode or edit mode.
    /// </summary>
    public enum ConfigPageStatus
    {
        Viewing,
        Creating,
        Editing,
    }

    public enum AlertMessageTypes
    {
        Success,
        Error,
    }

    public enum AlertMessageReason
    {
        Save,
        Load,
        Delete
    }
}
