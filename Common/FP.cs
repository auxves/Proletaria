using System;

namespace Proletaria.Common;

public static class FP
{
    public static void Let<T>(T? value, Action<T> fn)
    {
        if (value is not null)
        {
            fn(value);
        }
    }
}
