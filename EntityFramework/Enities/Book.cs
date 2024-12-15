namespace EntityFramework.Enities
{
    public class Book : BaseEntity
    {
        /// <summary>
        /// Title of the Book!
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ISBN Number of the Book!
        /// </summary>
        public string ISBN { get; set; }
        /// <summary>
        /// Length in Pages of the Book
        /// </summary>
        public int Length { get; set; } // in Pages
        public int PublicationYear { get; set; }
        /// <summary>
        /// AuthorID is the Foreign Key to the Author
        /// </summary>
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }

        /// <summary>
        /// List of the Book Loans!
        /// </summary>
        public List<BookLoan>? BookLoans { get; set; }
    }
}
