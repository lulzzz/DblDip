using System;

namespace ShootQ.Domain.Features.Expenses
{
    public class ExpenseDto
    {
        public Guid ExpenseId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
