using HomeAgency.Domain.Entities;

namespace HomeAgency.Application.Common.Interfaces;

public interface IBookingRepository : IRepository<Booking>
{
    void Update(Booking amenity);

    void UpdateStatus(int bookingId, string orderStatus);
    void UpdateStripepaymentId(int bookingId, string sessionId, string paymentIntentId);
}
