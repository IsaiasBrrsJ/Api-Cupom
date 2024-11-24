﻿using Coupon.Core.Entities.Reason;

namespace Coupon.Core.Entities.Coupon
{
    public  class Coupon : EntityBase
    {
        public CouponType TypeCoupon { get; init; }
        public decimal Price { get; private set; } 
        public DateTime ValidAt { get; private set; }
        public bool IsExpired { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime EventDate { get; init; }    
        public int MaxCoupon { get; init; }
        public DateTime CreationDate { get; private set; }
        public virtual Description Description { get; init; }
        public Guid DescriptionId { get; init; }

        public void Deactivate()
        {
            HasDescription();

            IsActive = !IsActive;
        }
        public void UpdatePrice(decimal price)
        {

            if (!IsActive)
                throw new InvalidOperationException("Cupom já desativado");

            HasDescription();

            Price = price;
          
        }

        public void UpdateValidate(DateTime validAt)
        {
            HasDescription();

            ValidAt = validAt;
        }
        public void SetExpired()
        {
            HasDescription();

            IsExpired = !IsExpired;
        }

        private void HasDescription()
        {
            if (Description == null)
                throw new InvalidOperationException("Informe a razao da modificação");
        }
        public static class Factories
        {
            public static Coupon Create(CouponType typeCoupon, decimal price, DateTime validAt, DateTime eventDate, int max)
            {
                return new Coupon
                {
                    TypeCoupon = typeCoupon,
                    Price = price,
                    ValidAt = validAt,
                    EventDate = eventDate,
                    MaxCoupon = max,
                    CreationDate = DateTime.UtcNow
                };
            }
        }

    }
}