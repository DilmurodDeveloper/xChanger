﻿//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Persons.Exceptions;
using Xeptions;

namespace xChanger.Api.Services.Foundations.Persons
{
    public partial class PersonService
    {
        private delegate ValueTask<Person> ReturningPersonFunction();
        private delegate IQueryable<Person> ReturningPersonsFunction();

        private async ValueTask<Person> TryCatch(ReturningPersonFunction returningPersonFunction)
        {
            try
            {
                return await returningPersonFunction();
            }
            catch (NullPersonException nullPersonException)
            {
                throw CreateAndLogValidationException(nullPersonException);
            }
            catch (InvalidPersonException invalidPersonException)
            {
                throw CreateAndLogValidationException(invalidPersonException);
            }
            catch (NotFoundPersonException notFoundPersonException)
            {
                throw CreateAndLogValidationException(notFoundPersonException);
            }
            catch (SqlException sqlException)
            {
                var failedPersonStorageException =
                    new FailedPersonStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedPersonStorageException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedPersonException =
                    new LockedPersonException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedPersonException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedPersonStorageException =
                    new FailedPersonStorageException(dbUpdateException);

                throw CreateAndLogDependencyException(failedPersonStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsPersonException =
                    new AlreadyExistsPersonException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsPersonException);
            }
            catch (Exception exception)
            {
                var failedPersonServiceException =
                    new FailedPersonServiceException(exception);

                throw CreateAndLogServiceException(failedPersonServiceException);
            }
        }

        private IQueryable<Person> TryCatch(ReturningPersonsFunction returningPersonsFunction)
        {
            try
            {
                return returningPersonsFunction();
            }
            catch (SqlException sqlException)
            {
                var failedPersonStorageException =
                    new FailedPersonStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedPersonStorageException);
            }
            catch (Exception exception)
            {
                var failedPersonServiceException =
                    new FailedPersonServiceException(exception);

                throw CreateAndLogServiceException(failedPersonServiceException);
            }
        }

        private PersonValidationException CreateAndLogValidationException(Xeption exception)
        {
            var personValidationException =
                new PersonValidationException(exception);

            this.loggingBroker.LogError(personValidationException);

            return personValidationException;
        }

        private PersonDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var personDependencyException = new PersonDependencyException(exception);
            this.loggingBroker.LogCritical(personDependencyException);

            return personDependencyException;
        }

        private PersonDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var personDependencyValidationException =
                new PersonDependencyValidationException(exception);

            this.loggingBroker.LogError(personDependencyValidationException);

            return personDependencyValidationException;
        }

        private PersonServiceException CreateAndLogServiceException(Xeption exception)
        {
            var personServiceException = new PersonServiceException(exception);
            this.loggingBroker.LogError(personServiceException);

            return personServiceException;
        }

        private PersonDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var personDependencyException = new PersonDependencyException(exception);
            this.loggingBroker.LogError(personDependencyException);

            return personDependencyException;
        }
    }
}
