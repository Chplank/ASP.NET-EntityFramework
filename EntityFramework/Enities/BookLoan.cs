namespace EntityFramework.Enities
{
    public class BookLoan : BaseEntity
    {
        /// <summary>
        /// ID of the Book which was loaned!
        /// </summary>
        public int BookId { get; set; }
        public Book Book { get; set; }
        /// <summary>
        /// Foreign Key to the Libary Member!
        /// </summary>
        public int LibraryMemberId { get; set; }
        public LibraryMember LibraryMember { get; set; }
        /// <summary>
        /// The LoanDate, when the book was loaned
        /// </summary>
        public DateTime LoanDate { get; set; }

        /// <summary>
        /// Return Date, when the Book was returned
        /// </summary>
        public DateTime? ReturnDate { get; set; }
    }
}
