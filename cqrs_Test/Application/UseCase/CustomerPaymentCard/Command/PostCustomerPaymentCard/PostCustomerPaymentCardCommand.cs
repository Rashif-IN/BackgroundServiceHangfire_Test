﻿using System;
using MediatR;

namespace cqrs_Test.Application.UseCase.CustomerPaymentCard.Command.PostCustomerPaymentCard
{
    public class PostCustomerPaymentCardCommand : IRequest<PostCustomerPaymentCardCommandDto>
    {
        public PostCustomerPaymentCardData Data { get; set; }
    }
    public class PostCustomerPaymentCardData
    {
        public int id { get; set; }
        public int customer_id { get; set; }
        public string name_on_card { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public int postal_code { get; set; }
        public string credit_card_number { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
    }
}