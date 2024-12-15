using EntityFramework.Enities;
using EntityFramework.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestAPIRepositoryTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoanController : ControllerBase
    {
        private readonly IRepository<BookLoan> _loanRepository;

        public BookLoanController(IRepository<BookLoan> loanRepository)
        {
            _loanRepository = loanRepository;
        }

        // POST: api/BookLoan
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookLoan loan)
        {
            if (loan == null)
            {
                return BadRequest("Loan object is null.");
            }

            if (loan.BookId <= 0 || loan.LibraryMemberId <= 0)
            {
                return BadRequest("Invalid BookId or LibraryMemberId.");
            }

            if (loan.LoanDate > DateTime.UtcNow)
            {
                return BadRequest("Loan date cannot be in the future.");
            }

            try
            {
                var createdLoan = await _loanRepository.AddAsync(loan);
                return CreatedAtAction(nameof(GetActiveLoansByMember), new { memberId = loan.LibraryMemberId }, createdLoan);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/BookLoan/{id}/return
        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            try
            {
                var loan = await _loanRepository.GetByIdAsync(id);
                if (loan == null)
                {
                    return NotFound($"Loan with ID {id} not found.");
                }

                if (loan.ReturnDate != null)
                {
                    return BadRequest("This book loan has already been returned.");
                }

                loan.ReturnDate = DateTime.UtcNow;
                await _loanRepository.UpdateAsync(loan);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Loan with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/BookLoan/member/{memberId}
        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetActiveLoansByMember(int memberId)
        {
            try
            {
                var loans = await _loanRepository.GetAllAsync();
                var activeLoans = loans.Where(l => l.LibraryMemberId == memberId && l.ReturnDate == null);

                if (!activeLoans.Any())
                {
                    return NotFound($"No active loans found for member ID {memberId}.");
                }

                return Ok(activeLoans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
