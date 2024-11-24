namespace IngameScript
{
    internal class Spinner : IAnimation
    {
        private const string States = @"/|\-";

        public string CurrentFrame
        {
            get
            {
                return States.Substring(CurrentStateIndex, 1);
            }
        }

        private int CurrentStateIndex { get; set; }

        public void NextFrame()
        {
            CurrentStateIndex = CurrentStateIndex == 0
                ? States.Length - 1
                : CurrentStateIndex - 1;
        }
    }
}
