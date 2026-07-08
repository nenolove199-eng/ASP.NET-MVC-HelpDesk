using HelpDeskAPI.Data;
using HelpDeskAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : Controller
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ عرض كل التذاكر (API)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }

        // ✅ عرض تذكرة واحدة حسب ID (API)
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();
            return ticket;
        }

        // ✅ إنشاء تذكرة جديدة (API)
        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }

        // ✅ تعديل تذكرة موجودة (API)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id) return BadRequest();
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ حذف تذكرة (API)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ البحث عن تذاكر بالعنوان (API)
        [HttpGet("search/{keyword}")]
        public ActionResult<IEnumerable<Ticket>> SearchTickets(string keyword)
        {
            var tickets = _context.Tickets
                .Where(t => t.Title.Contains(keyword))
                .ToList();

            if (!tickets.Any()) return NotFound();

            return tickets;
        }

        // ✅ عرض التذاكر في صفحة MVC
        [HttpGet("mvc")]
        public IActionResult Index()
        {
            var tickets = _context.Tickets.ToList();
            return View(tickets);
        }
    }
}
