using UnityEngine;

public class SliceRotationCommand
{
    public Vector3 HitUnitPosition { get; private set; }
    public Vector3 HitPointPosition { get; private set; }
    public Vector3 RotationDirection { get; private set; }

    public SliceRotationCommand(Vector3 hitUnitPosition, Vector3 hitPointPosition, Vector3 rotationDirection)
    {
        this.HitUnitPosition = hitUnitPosition;
        this.HitPointPosition = hitPointPosition;
        this.RotationDirection = rotationDirection;
    }

    public SliceRotationCommand UndoCommand()
    {
        return new SliceRotationCommand(HitUnitPosition , HitPointPosition, -RotationDirection);
    }
}