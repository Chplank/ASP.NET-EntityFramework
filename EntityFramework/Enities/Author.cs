namespace EntityFramework.Enities
{
    public class Author : BaseEntity
    {
        /// <summary>
        /// FirstName of the Author!
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// LastName of the Author!
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Date of Birth of the Author!
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// A list of Books the Author has written!
        /// </summary>
        public List<Book>? Books { get; set; }
    }
}
