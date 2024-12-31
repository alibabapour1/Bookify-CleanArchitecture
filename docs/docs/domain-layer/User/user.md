<div dir="rtl">

# کلاس User

همانند کلاس آپارتمان یک سری ValueObject برای پراپرتی های این کلاس تعریف میکنیم. همچنین این کلاس باید از موجودیت Entity  نیز ارث بری کند.

همچنین به جای یک سازنده پابلیک یک Factory method برای ایجاد کاربر ایجاد میکنیم و ورودی های کاربر را دریافت کرده و علاوه بر ایجاد کاربر ایونت های مربوط آن را نیز raise می کند. 

برای مثال هنگامی که کاربر ایجاد شد یک ایونت ایجاد کاربر با تایپ record نیز فراخوانی می شود تا اقدامات لازم مثلا ارسال ایمیل خوش آمد گویی ارسال شود.


```csharp
namespace Bookify.Domain.Users
{
    public sealed class User : Entity
    {
        private User(Guid id, FirstName firstName, LastName lastName, Email email) 
        : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }

        public static User Create(FirstName firstName, LastName lastName, Email email)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName, email);

            user.RaiseDomainEvent(new UserCreatedEvent(user.Id));

            return user;
        }
    }
}
```

![userClass](/images/userClass.png)

</div>