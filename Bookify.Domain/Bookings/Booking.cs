using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings.Events;

namespace Bookify.Domain.Bookings
{
    public sealed class Booking : Entity
    {
        private Booking(Guid id,Guid apartmentId,Guid userId,DateRange duration,Money priceForPeriod ,Money cleaningFee,
            Money amenitiesUpCharge,Money totalPrice, BookingStatus bookingStatus,DateTime createdOnUtc 
        )
            :base(id)
        {
            BookingStatus = bookingStatus;
            CreatedOnUtc = createdOnUtc;
            ApartmentId = apartmentId;
            UserId = userId;
            Duration = duration;
            PriceForPeriod = priceForPeriod;
            CleaningFee = cleaningFee;
            AmenitiesUpCharge = amenitiesUpCharge;
            TotalPrice = totalPrice;

            
        }

        public Guid ApartmentId { get; private set; }
        public Guid UserId { get; private set; }

        public DateRange Duration { get; private set; }
        public Money PriceForPeriod { get; private set; }
        public Money CleaningFee { get; private set; }
        public Money AmenitiesUpCharge { get; private set; }
        public Money TotalPrice  { get;private set; }
        public BookingStatus BookingStatus { get; private set; }
        public DateTime CreatedOnUtc { get;private set; }
        public DateTime? ConfirmedOnUtc { get; private set; }
        public DateTime? RejectedOnUtc { get; private set; }
        public DateTime? CompletedOnUtc { get; private set; }
        public DateTime? CancelledOnUtc { get; private set; }


        public static Booking ReserveBooking(Guid userId, Guid apartmentId, DateRange duration,PricingDetails pricingDetails ,DateTime utcNow)
        {
            var booking = new Booking(Guid.NewGuid(), apartmentId, userId, duration, pricingDetails.PriceForPeriod,
                pricingDetails.CleaningFee, pricingDetails.AmenitiesUpCharge,
                pricingDetails.TotalPrice,BookingStatus.Reserved,utcNow );
            booking.RaiseDomainEvents(new BookingReservedDomainEvent(booking.Id));
            return booking;
        }
    }
}
