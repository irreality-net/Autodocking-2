namespace IngameScript
{
    internal class Blinker : IAnimation
    {
        public Blinker(string whenVisible, string whenNotVisible = null)
        {
            WhenVisible = whenVisible;
            WhenNotVisible = whenNotVisible ?? new string(' ', whenVisible.Length);
        }

        public string CurrentFrame
        {
            get
            {
                return IsVisible ? WhenVisible : WhenNotVisible;
            }
        }

        public string WhenVisible { get; set; }

        public string WhenNotVisible { get; set; }

        private bool IsVisible { get; set; }

        public void NextFrame()
        {
            IsVisible = !IsVisible;
        }
    }
}
