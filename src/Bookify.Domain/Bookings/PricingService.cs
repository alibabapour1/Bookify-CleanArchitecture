using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Apartments;


namespace Bookify.Domain.Bookings
{
    public class PricingService
    {
        public PricingDetails CalculatePrice(Apartment apartment, DateRange Period)
        {
            var currency = apartment.Price.Currency;

            var priceForPeriod = new Money(apartment.Price.Amount * Period.LengthInDays, currency);

            decimal percentageUpCharge = 0;
            foreach (var apartmentAmenity in apartment.Amenities)
            {
                percentageUpCharge += apartmentAmenity switch
                {
                    Amenity.AirConditioning => 0.01m,
                    Amenity.GardenView or Amenity.MountainView => 0.05m,
                    Amenity.Parking => 0.01m,
                    _ => 0

                };
            }

            var amenitiesUpCharge = Money.Zero(currency);
            if (percentageUpCharge > 0)
            {
                amenitiesUpCharge = new Money(priceForPeriod.Amount * percentageUpCharge, currency);
            }

            var totalPrice = Money.Zero();
            totalPrice += priceForPeriod;

            if(!apartment.CleaningFee.IsZero()) {totalPrice += apartment.CleaningFee;}

            totalPrice += amenitiesUpCharge;

            return new PricingDetails(priceForPeriod, apartment.CleaningFee, amenitiesUpCharge, totalPrice);
        }


        
    }
}
