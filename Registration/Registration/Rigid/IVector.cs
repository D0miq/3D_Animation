namespace Registration.Rigid
{
    using System;
    using System.Collections;

    public interface IVector
    {
        int Size { get; }

        float GetValue(int index);

        float Distance(IVector vector);
    }
}
