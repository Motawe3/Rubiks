using UnityEngine;

public class SliceRotationCommand
{
    public Vector3 hitUnitPosition { get; private set; }
    public Vector3 hitFacePosition { get; private set; }
    public Vector3 rotationDirection { get; private set; }

    public SliceRotationCommand(Vector3 hitUnitPosition, Vector3 hitFacePosition, Vector3 rotationDirection)
    {
        this.hitUnitPosition = hitUnitPosition;
        this.hitFacePosition = hitFacePosition;
        this.rotationDirection = rotationDirection;
    }

    public SliceRotationCommand UndoCommand()
    {
        return new SliceRotationCommand(hitUnitPosition , hitFacePosition, -rotationDirection);
    }
}