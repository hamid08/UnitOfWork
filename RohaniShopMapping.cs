using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RohaniShop.Entities;

namespace RohaniShop.Data.Mapping
{
    public static class RohaniShopMapping
    {
        public static void AddCustomRohaniShopMappings(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FactorProductMapping());
            modelBuilder.ApplyConfiguration(new FactorDriverMapping());
            modelBuilder.ApplyConfiguration(new FactorMapping());




            #region Seed data
            modelBuilder.Entity<TypePayment>().HasData(new TypePayment()
            {
                TypePaymentId = 1,
                Type = "دستگاه پوز"
            },
                new TypePayment()
                {
                    TypePaymentId = 2,
                    Type = "نقدی"
                },
            new TypePayment()
            {
                TypePaymentId = 3,
                Type = "حواله بانکی"
            } ,
            new TypePayment()
            {
                TypePaymentId = 4,
                Type = "چک"
            }

            );

            //Seed data UnitOdMeasurement
            modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement()
                {
                    UnitOfMeasurementId = 1,
                    Unit = "عدد"
                },
                new UnitOfMeasurement()
                {
                    UnitOfMeasurementId = 2,
                    Unit = "کیلو"
                },
                new UnitOfMeasurement()
                {
                    UnitOfMeasurementId = 3,
                    Unit = "شاخه"
                },
                new UnitOfMeasurement()
                {
                    UnitOfMeasurementId = 4,
                    Unit = "بسته"
                },
                new UnitOfMeasurement()
                {
                    UnitOfMeasurementId = 5,
                    Unit = "کیسه"
                },
                new UnitOfMeasurement()
                {
                    UnitOfMeasurementId = 6,
                    Unit = "تن"
                }

            );

            #endregion

        }
    }
}
