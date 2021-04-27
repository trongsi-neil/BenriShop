﻿using BenriShop.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using StripeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BenriShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public PaymentController()
        {
            StripeConfiguration.ApiKey = "sk_test_51Ih3a7CcmRncbbOZDTGV9kbogZ3heUlbHBoIZjbCMDHjhQr7r1VSDZbgpfUwlTcOdoH41iHTTDJYvZCfa9MtKPbl00BGoDbMDe";
        }
        // POST: api/Payment/Charge
        [HttpPost("Charge")]
        public IActionResult Charge(PayModelView data)
        {
            //Get token
            var options = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = data.Number,
                    ExpMonth = data.ExpMonth,
                    ExpYear = data.ExpYear,
                    Cvc = data.Cvc,
                },
            };
            var service = new TokenService();
            var tokenId = service.Create(options).Id;

            //Use token to create a charge
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = data.Email,
                Source = tokenId
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = data.Total,
                Description = data.Description,
                Currency = "vnd",
                Customer = customer.Id,
                ReceiptEmail = data.Email,
                Metadata = new Dictionary<string, string>()
                {
                    { "OrderId", data.OrderId },
                }
            });
            if (charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                return Ok("Charge is created!");
            }

            return BadRequest("Error when create charge!");
        }
    }
}