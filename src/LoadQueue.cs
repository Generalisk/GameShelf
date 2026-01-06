namespace GameShelf;

/// <summary>
/// A queue that runs one action per frame.
/// 
/// Mainly used for loading massives chunks of textures at once
/// without having the game freeze & maybe even crash on lower-end devices.
/// </summary>
internal static class LoadQueue
{
    private static List<Action> queue
        = new List<Action>();

    internal static void Add(Action action)
        => queue.Add(action);

    internal static void Next()
    {
        if (queue.Count <= 0)
            return;

        queue[0].Invoke();
        queue.RemoveAt(0);
    }
}
