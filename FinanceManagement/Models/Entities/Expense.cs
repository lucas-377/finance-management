﻿using System.ComponentModel.DataAnnotations;

namespace FinanceManagement.Models.Entities
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public int MonthId { get; set; } // FK to month.
        public Month Month { get; set; } // Associate month class.
        public int ExpenseTypeId { get; set; } // FK to expense type.
        [Required(ErrorMessage = "Please enter a value.")]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid value.")] // Dont accept negative values and double overflow value.
        public double Value { get; set; }
    }
}
