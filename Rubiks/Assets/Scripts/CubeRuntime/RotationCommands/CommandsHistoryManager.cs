using System;
using System.Collections.Generic;

public static class CommandsHistoryManager
{
    private static Stack<SliceRotationCommand> sliceRotationCommands = new Stack<SliceRotationCommand>();
    public static Action<SliceRotationCommand> OnCommandPushed;
    public static Action<SliceRotationCommand> OnCommandPoped;
    public static Action OnHistoryUpdated;

    public static void PushCommand(SliceRotationCommand sliceRotationCommand)
    {
        sliceRotationCommands.Push(sliceRotationCommand);
        OnCommandPushed?.Invoke(sliceRotationCommand);
        OnHistoryUpdated?.Invoke();
    }
    
    public static void PopCommand()
    {
        if(HasCommands())
            OnCommandPoped?.Invoke(sliceRotationCommands.Pop().UndoCommand());
        OnHistoryUpdated?.Invoke();
    }

    public static bool HasCommands()
    {
        return sliceRotationCommands.Count > 0;
    }

    public static void ClearHistory()
    {
        sliceRotationCommands.Clear();
        OnHistoryUpdated?.Invoke();
    }
}