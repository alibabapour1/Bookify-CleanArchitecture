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

        private Booking()
        {
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


        public static Booking ReserveBooking(Guid userId, Apartment apartment, DateRange duration,PricingService pricingService,DateTime utcNow)
        {
            var pricingDetails = pricingService.CalculatePrice(apartment, duration);
            var booking = new Booking(Guid.NewGuid(),
                apartment.Id,
                userId,
                duration, 
                pricingDetails.PriceForPeriod,
                pricingDetails.CleaningFee,
                pricingDetails.AmenitiesUpCharge,
                pricingDetails.TotalPrice,
                BookingStatus.Reserved,
                utcNow);
            booking.RaiseDomainEvents(new BookingReservedDomainEvent(booking.Id));

            apartment.LastBookedOnUtc = utcNow;
            return booking;
        }

        public Result Confirm(DateTime utcNow)
        {
            if (BookingStatus != BookingStatus.Reserved)
            {
                return Result.Failure(BookingErrors.NotReserved);
            }

            BookingStatus = BookingStatus.Confirmed;
            ConfirmedOnUtc = utcNow;
            this.RaiseDomainEvents(new BookingConfirmedDomainEvent(this.Id));

            return Result.Success();

        }

        public Result Reject(DateTime utcNow)
        {
            if (BookingStatus != BookingStatus.Reserved)
            {
                return Result.Failure(BookingErrors.NotReserved);
            }

            BookingStatus = BookingStatus.Rejected;
            RejectedOnUtc = utcNow;
            this.RaiseDomainEvents(new BookingRejectedDomainEvent(this.Id));

            return Result.Success();

        }
        public Result Complete(DateTime utcNow)
        {
            if (BookingStatus != BookingStatus.Confirmed)
            {
                return Result.Failure(BookingErrors.NotConfirmed);
            }

            BookingStatus = BookingStatus.Completed;
            CompletedOnUtc = utcNow;
            this.RaiseDomainEvents(new BookingCompletedDomainEvent(this.Id));

            return Result.Success();

        }

        public Result Cancel(DateTime utcNow)
        {
            if (BookingStatus != BookingStatus.Confirmed)
            {
                return Result.Failure(BookingErrors.NotConfirmed);
            }

            var currentDate = DateOnly.FromDateTime(utcNow);
            if (currentDate > Duration.Start)
            {
                return Result.Failure(BookingErrors.AlreadyStarted);
            }


            CancelledOnUtc = utcNow;
            BookingStatus = BookingStatus.Rejected;
            this.RaiseDomainEvents(new BookingCancelledDomainEvent(this.Id));
            
            
            return Result.Success();
        }


    }
}
