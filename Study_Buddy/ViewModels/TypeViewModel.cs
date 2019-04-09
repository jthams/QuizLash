using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels
{
    [NotMapped]
    public static class TypesEnum
    {
        public enum Type
        {
            [Display(Name = "Multiple Choice")]
            MultipleChoice,
            [Display(Name = "Fill in the Blank")]
            FillInTheBlank,
            [Display(Name = "True or False")]
            TrueOrFalse
        }

    }
}
