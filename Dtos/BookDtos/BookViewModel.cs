namespace libraryVueApp.Dtos
{
    public class BookViewModel
    {
        public string Title { get; internal set; }
        public string Author { get; internal set; }
        public int YearPublished { get; internal set; }
        public string Description { get; internal set; }
        public BookStatus Status { get; internal set; }
        public int QueueLength { get; internal set; }
        public int Id { get; internal set; }
        public bool AlreadyRequested { get; internal set; }
        public string ExpectedReturnDate { get; internal set; }
        public bool IsAbleToReturn { get; internal set; }
    }

    public enum BookStatus
    {
        Free = 1,
        Taken = 2,
        AlreadyRequested = 3
    }
}
