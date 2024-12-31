<div dir="rtl">

# کلاس Apartment

همونطور که توی عکس مشخص هستش دیتا تایپ های پراپرتی های کلاس آپارتمان به جای اینکه string باشند به نام خودشون یک دیتا تایپ تعریف شده براشون که هر کدومشون یک record محسوب می شن. 
هدف از ایجاد رکورد جلوگیری از primitive obsession  یعنی استفاده بیش از حد از تایپ های یکسان مثل string  که باعث یکنواختی پراپرتی ها، سخت تر شدن فرآیند بررسی مقادیر آن ها و غیره می شود. 
اما با ایجاد یک رکورد برای مثلا پراپرتی Address میتوانیم هم ولیدیشن هایی روی آن انجام دهیم و …

برخی از مشکلات primitive obsession:

<div dir="ltr">

1- Lack of Validation:

•	A string used as an email address can hold invalid values ("not-an-email").

2-  Reduced Readability:

•	It’s unclear what a string or int represents without context.

3-  No Encapsulation of Domain Logic:

•	Validation or formatting logic for concepts like email addresses has to be repeated in multiple places.

</div>

*دلیل اینکه تمام Setter های پراپرتی های پرایوت هست اینه که نمیخوام مقادیر اون ها در خارج است کلاس تغیری پیدا کنه.


```csharp
namespace Bookify.Domain.Apartments
{
    public sealed class Apartment : Entity
    {
        public Apartment(Guid id, Name name, Description description,
         Address address, Money price, 
         Money cleaningFee, List<Amenity> amenities) : base(id)
        {
            Name = name;
            Description = description;
            Address = address;
            Price = price;
            CleaningFee = cleaningFee;
            Amenities = amenities;
        }

        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public Address Address { get; private set; }
        public Money Price { get; private set; }
        public Money CleaningFee { get; private set; }
        public DateTime? LastBookedOnUtc { get; internal set; }
        public List<Amenity> Amenities { get; private set; } = []; // -> new()
    }
}
```

![apartmentClass](/images/apartmentClass.png)


</div>
