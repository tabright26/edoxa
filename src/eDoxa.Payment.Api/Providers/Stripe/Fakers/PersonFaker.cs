// Filename: PersonFaker.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using Person = Stripe.Person;

namespace eDoxa.Payment.Api.Providers.Stripe.Fakers
{
    public sealed class PersonFaker : Faker<Person>
    {
        private readonly AddressFaker _addressFaker = new AddressFaker();
        private readonly DobFaker _dobFaker = new DobFaker();

        public PersonFaker()
        {
            this.RuleFor(person => person.FirstName, faker => faker.Name.FirstName());

            this.RuleFor(person => person.LastName, faker => faker.Name.LastName());

            this.RuleFor(person => person.Email, (faker, person) => faker.Internet.Email(person.FirstName, person.LastName));

            this.RuleFor(person => person.Address, _addressFaker);

            this.RuleFor(person => person.Dob, _dobFaker);
        }

        public Person FakePerson()
        {
            return this.Generate();
        }
    }
}
