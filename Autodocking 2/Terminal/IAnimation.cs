namespace IngameScript
{
    internal interface IAnimation
    {
        string CurrentFrame { get; }

        void NextFrame();
    }
}
