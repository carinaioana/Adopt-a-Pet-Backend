﻿using AdoptPets.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace AdoptPets.Domain.Entities
{
    public class Deworming : AuditableEntity
    {
        public Deworming(Guid animalId, DateTime date, string dewormingType)
        {
            DewormingId = Guid.NewGuid();
            AnimalId = animalId;
            Date = date;
            DewormingType = dewormingType;
        }

        [Key]
        public Guid DewormingId { get; private set; }
        public Guid AnimalId { get; private set; }
        public Animal Animal { get; private set; }
        public DateTime Date { get; private set; }
        public string DewormingType { get; private set; } = string.Empty;

        public static Result<Deworming> Create(Guid animalId, DateTime date, string dewormingType)
        {
            if (date == default)
            {
                return Result<Deworming>.Failure("Date is required.");
            }
            if (string.IsNullOrWhiteSpace(dewormingType))
            {
                return Result<Deworming>.Failure("DewormingType is required.");
            }
            if (animalId == Guid.Empty)
            {
                return Result<Deworming>.Failure("AnimalId is required.");
            }
            return Result<Deworming>.Success(new Deworming(animalId, date, dewormingType));
        }
        public void Update(DateTime date, string dewormingType)
        {
            if (date == default)
            {
                throw new ArgumentNullException(nameof(date), "Date is required.");
            }
            if (string.IsNullOrWhiteSpace(dewormingType))
            {
                throw new ArgumentNullException(nameof(dewormingType), "DewormingType is required.");
            }
            Date = date;
            DewormingType = dewormingType;
        }
        public void UpdateDewormingType(string dewormingType)
        {
            if (string.IsNullOrWhiteSpace(dewormingType))
            {
                throw new ArgumentNullException(nameof(dewormingType), "DewormingType is required.");
            }
            DewormingType = dewormingType;
        }
        public void UpdateDate(DateTime date)
        {
            if (date == default)
            {
                throw new ArgumentNullException(nameof(date), "Date is required.");
            }
            Date = date;
        }

    }
}