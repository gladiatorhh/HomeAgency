using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Application.Common.Utility;
using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Data;
using System.Linq.Expressions;

namespace HomeAgency.Infrastructure.Common.Impelementations;

public class BookingRepository : Repository<Booking>, IBookingRepository
{
    private readonly HomeAgencyDbContext _context;

    public BookingRepository(HomeAgencyDbContext context):base(context)
    {
        _context = context;
    }

    public void Update(Booking booking)
    {
        _context.Bookings.Update(booking);
    }

    public void UpdateStatus(int bookingId, string bookingStatus)
    {
        var booking = _context.Bookings.Find(bookingId);
        if (booking is not null)
        {
            booking.Status = bookingStatus;
            if (bookingStatus == SD.StatusCheckedIn)
            {
                booking.ActualCheckInDate = DateTime.Now;
            }

            if (bookingStatus == SD.StatusCompleted)
            {
                booking.ActualCheckOutDate = DateTime.Now;
            }

        }
    }

    public void UpdateStripepaymentId(int bookingId, string sessionId, string paymentIntentId)
    {
        var booking = _context.Bookings.Find(bookingId);
        if (booking is not null)
        {
            if (!string.IsNullOrEmpty(sessionId))
            {
                booking.StripeSessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                booking.StripePaymentIntentId = paymentIntentId;
                booking.PaymentDate = DateTime.Now;
                booking.IsPaymentSuccessful = true;
            }
        }
    }
}
