<div dir="rtl">

# ValueObjesct های تعریف شده در کلاس آپارتمان

برخی از آن ها مثل اسم و توضیحات چیز خاصی ندارند.

*Aminity یک enumeration  است.

<div dir="ltr">

# Name

```csharp
namespace Bookify.Domain.Apartments
{
    public record Name(string value);
}
```

# Description

```csharp
namespace Bookify.Domain.Apartments
{
    public record Description(string value);
}
```

# Amenity

```csharp
namespace Bookify.Domain.Apartments
{
    public enum Amenity
    {
        WiFi = 1,
        AirConditioning = 2,
        Parking = 3,
        PetFriendly = 4,
        SwimmingPool = 5,
        Gym = 6,
        Spa = 7,
        Terrace = 8,
        MountainView = 9,
        GardenView = 10
    }
}
```

# Address

```csharp
namespace Bookify.Domain.Apartments
{
    public record Address(
        string Country,
        string State,
        string ZipCode,
        string City,
        string Street);
}
```

</div>



</div>