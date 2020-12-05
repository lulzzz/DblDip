﻿using BuildingBlocks.Abstractions;
using ShootQ.Core.ValueObjects;
using System;
using System.Threading.Tasks;

namespace ShootQ.Core.Services
{
    public interface IAvailabilityCheck
    {
        Task<bool> IsAvailable(DateRange dateRange); 
    }

    public class AvailabilityCheck : IAvailabilityCheck
    {
        private readonly IAppDbContext _context;

        public AvailabilityCheck(IAppDbContext context)
        {
            _context = context;
        }

        public Task<bool> IsAvailable(DateRange dateRange)
        {
            throw new NotImplementedException();
        }
    }
}
