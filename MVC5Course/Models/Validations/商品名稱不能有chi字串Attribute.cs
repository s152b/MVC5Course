using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.Validations
{
    /// <summary>
    /// 自訂的驗證都是server端的，不會有javascript
    /// </summary>
    public class 商品名稱不能有chi字串Attribute : DataTypeAttribute
    {
        public 商品名稱不能有chi字串Attribute():base(DataType.Text)
        {
            //set 預設錯誤訊息
            this.ErrorMessage = "商品名稱不能有chi字串";
        }
        
        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value);
            if (str.Contains("chi"))
            {
                return false;
            }
            return true;
        }
    }
}