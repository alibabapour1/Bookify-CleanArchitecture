using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews
{
    public static class ReviewErrors
    {
        public static Error NotEligible =
            new("Review.NotEligible", "The Booking Is Not Completed So It Cannot Be Reviewed.");
    }
}
