﻿using System;
using OpenCqrs.Domain;
using OpenCqrs.Examples.Domain.Events;

namespace OpenCqrs.Examples.Domain
{
    public class Product : AggregateRootWithEvents
    {
        public string Title { get; private set; }

        public Product()
        {            
        }

        public Product(Guid id, string title) : base(id)
        {
            if (string.IsNullOrEmpty(title))
                throw new ApplicationException("Product title is required.");

            AddAndApplyEvent(new ProductCreated
            {
                AggregateRootId = Id,
                Title = title
            });
        }

        public void UpdateTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ApplicationException("Product title is required.");

            AddAndApplyEvent(new ProductTitleUpdated
            {
                AggregateRootId = Id,
                Title = title
            });
        }

        private void Apply(ProductCreated @event)
        {
            Id = @event.AggregateRootId;
            Title = @event.Title;
        }

        private void Apply(ProductTitleUpdated @event)
        {
            Title = @event.Title;
        }
    }
}
