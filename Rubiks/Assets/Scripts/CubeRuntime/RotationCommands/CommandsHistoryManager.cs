using System;
using System.Collections.Generic;

public static class CommandsHistoryManager
{
    private static Stack<SliceRotationCommand> sliceRotationCommands = new Stack<SliceRotationCommand>();
    public static Action<SliceRotationCommand> OnCommandPushed;
    public static Action<SliceRotationCommand> OnCommandPoped;

    public static void PushCommand(SliceRotationCommand sliceRotationCommand)
    {
        sliceRotationCommands.Push(sliceRotationCommand);
        OnCommandPushed?.Invoke(sliceRotationCommand);
    }
    
    public static void PopCommand()
    {
        if(HasCommands())
            OnCommandPoped?.Invoke(sliceRotationCommands.Pop());
    }

    public static bool HasCommands()
    {
        return sliceRotationCommands.Count > 0;
    }

    public static void ClearHistory()
    {
        sliceRotationCommands.Clear();
    }
}