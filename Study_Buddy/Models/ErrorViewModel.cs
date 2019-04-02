using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Study_Buddy.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}