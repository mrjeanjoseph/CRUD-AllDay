namespace ValueAndRefTypes {
    internal struct WorkTask {

        public int descId;
        public string description;
        public int hours;

        public readonly void PerformWorkTask() {
            Console.WriteLine(  $"Task {description} of {hours} hours has been performed.");
        }
    }
}
