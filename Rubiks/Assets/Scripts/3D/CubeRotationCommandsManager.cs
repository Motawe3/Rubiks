using System.Collections.Generic;

public class CubeRotationCommandsManager
{
    private Cube3D cube3D;
    private Stack<SliceRotationCommand> sliceRotationCommands = new Stack<SliceRotationCommand>();

    public CubeRotationCommandsManager(Cube3D cube3D)
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
}