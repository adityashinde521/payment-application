using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        public PaymentDetailController(PaymentDetailContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            //return await _context.PaymentDetails.ToListAsync();
            var dbData = await _context.PaymentDetails.ToListAsync();

            if (dbData == null || !dbData.Any())
            {
                // Static fallback data
                var staticData = new List<PaymentDetail>
            {
                new PaymentDetail
                {
                    PMId = 1,
                    CardOwnerName = "Ashu",
                    CardNumber = "9876543210123456",
                    ExpirationDate = "15/05",
                    CVV = "456"
                },
                new PaymentDetail
                {
                    PMId = 2,
                    CardOwnerName = "Aditya",
                    CardNumber = "1234567890123456",
                    ExpirationDate = "04/05",
                    CVV = "254"
                }
            };

                return Ok(staticData);
            }

            return Ok(dbData);
        }
        

        // PUT: api/PaymentDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetail(int id, PaymentDetail paymentDetail)
        {
            if(id!=paymentDetail.PMId)
            {
                return BadRequest();
            }
            _context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: api/PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            return paymentDetail;
        }

        // POST: api/PaymentDetail
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.PMId }, paymentDetail);
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetail>> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return paymentDetail;
        }

        private bool PaymentDetailExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.PMId == id);
        }
    }
}
