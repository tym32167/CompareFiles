namespace CompareFiles
{
    public class CompareStringResult
    {
        public enum ActionType { Deleted, Added, Changed, NotChanged };

        public ActionType Action { get; }
        public string OldValue { get; }
        public string NewValue { get; }

        public CompareStringResult(string oldValue, string newValue, ActionType action)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Action = action;
        }
    }
}