namespace HotelBooking.Infrastructure;
public interface IDbInitializer
{
    void Initialize(HotelBookingContext context, bool seedData);
}
