using System.Collections.Generic;

public class CubeRotationCommandsHistory
{
    private Cube3D cube3D;
    private Stack<SliceRotationCommand> sliceRotationCommands = new Stack<SliceRotationCommand>();

    public CubeRotationCommandsHistory(Cube3D cube3D)
    {
        this.cube3D = cube3D;
    }

    public void PushCommand(SliceRotationCommand sliceRotationCommand)
    {
        sliceRotationCommands.Push(sliceRotationCommand);
    }
    
    public SliceRotationCommand PopLastCommand()
    {
        return sliceRotationCommands.Pop();
    }

    public bool HasCommands()
    {
        return sliceRotationCommands.Count > 0;
    }

    public void ClearHistory()
    {
        sliceRotationCommands.Clear();
    }
}