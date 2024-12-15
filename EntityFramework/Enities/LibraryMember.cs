namespace EntityFramework.Enities
{
    public class LibraryMember : BaseEntity
    {
        /// <summary>
        /// Firstname of LibraryMember!
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// LastName of the LibraryMember!
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Email of the LibraryMember!
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Phone Number of the Library Member!
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// List of BookLoans
        /// </summary>
        public List<BookLoan> BookLoans { get; set; }
    }
}
