using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        //在controllers action使用時外的方法在這邊自訂 Add, All, Delete,...
        // 用 override 加空格看可以複寫哪一些
        //或到 IRepository.cs查規格

        public Product Find(int id)
        {
            //手動實作Find方法
            //this.All--已經過濾了
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public override IQueryable<Product> All()
        {
            //最底層的ALL加過濾--base.All
            return base.All().Where(p => false==p.IsDeleted && p.Stock < 1500);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public IQueryable<Product> All(bool showAll)
        {
            if (showAll)
            {
                //未過濾的
                return base.All();
            }
            else
            {
                //已經過濾的
                return this.All();
            }
        }

        public override void Delete(Product entity)
        {
            //原本gent的code
            //base.Delete(entity);

            //從 Entity Framework 關閉實體模型驗證的方式
            //this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;

            //需求：改為不刪實體資料，mark要刪除那筆
            entity.IsDeleted = true;
            //其他再改all 的where條件

        }
    }

    public interface IProductRepository : IRepository<Product>
    {

    }
}