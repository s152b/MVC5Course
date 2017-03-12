using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.Validations
{
    public class 庫存不可小於零Attribute : DataTypeAttribute
    {
        public 庫存不可小於零Attribute() : base(DataType.Text)
        {
            this.ErrorMessage = " 庫存不可小於0";
        }

        public override bool IsValid(object value)
        {
            decimal stock = Convert.ToDecimal(value);
            return !(stock < 0);
        }
    }
}