import { defineConfig } from 'vitepress'

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "Bookify",
  description: "Alireza Abasi",
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    nav: [
      { text: "Home", link: "/", },
      { text: "Examples", link: "/markdown-examples" },
    ],

    sidebar: [
      {
        text: "Domain Layer",
        collapsed: true,
        items: [
          { text: "Overview", link: "/docs/domain-layer/index.md" },
          { text: "Abstractions",
            items:[
              {text: "Entity", link:"/docs/domain-layer/Abstractions/entity.md"},
              {text: "IDomainEvent", link:"/docs/domain-layer/Abstractions/IDomainEvent.md"},
              {text: "IUnitOfWork", link:"/docs/domain-layer/Abstractions/IUnitOfWork.md"},
            ]
           },
          { text: "Apartment", link: "/docs/domain-layer/Apartment/apartment.md",
                items:[
                  { text: "ValueObjects", link:"/docs/domain-layer/Apartment/apartmentValueObjects.md"},
                  { text: "IApartmentRepository", link:"/docs/domain-layer/Apartment/iapartmentRepository.md"},
                ]
               },
          { text: "User", link: "/docs/domain-layer/User/user.md",
                items:[
                  {text: "ValueObjects", link:"/docs/domain-layer/User/userValueObjects.md"},
                  {text: "Events", link: "/docs/domain-layer/User/userEvents.md"},
                  {text: "IUserRepository", link: "/docs/domain-layer/User/iuserRepository.md"}
                ]
          },
          { text: "Booking", link: "/docs/domain-layer/Booking/booking.md",
            items:[
              {text: "ValueObjects", link:"/docs/domain-layer/Booking/bookingValueObjects.md"},
              {text: "Events", link: "/docs/domain-layer/Booking/bookingEvents.md"},
              {text: "PricingService", link: "/docs/domain-layer/Booking/pricingService.md"},
              {text: "BookingErrors", link: "/docs/domain-layer/Booking/bookingErrors.md"},
            ]
          },
          { text: "Shared",
            items:[
              {text: "Currency", link:"/docs/domain-layer/Shared/currency.md"},
              {text: "Money", link: "/docs/domain-layer/Shared/money.md"},
            ]
          }
        ],
      },
      {
        text: "Application layer",
        collapsed: true,
        items: [
          { text: "Get Started", link: "/docs/domain-layer/index.md" },
          { text: "Entities", link: "/docs/domain-layer/entities.md" },
        ],
      },
    ],

    socialLinks: [
      { icon: "github", link: "https://github.com/Alireezaad/Bookify" },
    ],
  },
});
