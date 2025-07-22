//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Persons.Exceptions;

namespace xChanger.Api.Services.Foundations.Persons
{
    public partial class PersonService
    {
        private void ValidatePersonOnAdd(Person person)
        {
            ValidatePersonNotNull(person);

            Validate(
                (Rule: IsInvalid(person.Id), Parameter: nameof(Person.Id)),
                (Rule: IsInvalid(person.Name), Parameter: nameof(Person.Name)),
                (Rule: IsInvalid(person.Age), Parameter: nameof(Person.Age)));
        }

        private static void ValidatePersonId(Guid personId) =>
            Validate((Rule: IsInvalid(personId), Parameter: nameof(Person.Id)));

        private static void ValidatePersonNotNull(Person person)
        {
            if (person is null)
            {
                throw new NullPersonException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(int number) => new
        {
            Condition = number <= 0,
            Message = "Value must be greater than 0"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidPersonException = new InvalidPersonException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidPersonException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidPersonException.ThrowIfContainsErrors();
        }
    }
}
