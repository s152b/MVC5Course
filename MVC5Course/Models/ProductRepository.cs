using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        //�bcontrollers action�ϥήɥ~����k�b�o��ۭq Add, All, Delete,...
        // �� override �[�Ů�ݥi�H�Ƽg���@��
        //�Ψ� IRepository.cs�d�W��

        public Product Find(int id)
        {
            //��ʹ�@Find��k
            //this.All--�w�g�L�o�F
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public override IQueryable<Product> All()
        {
            //�̩��h��ALL�[�L�o--base.All
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
                //���L�o��
                return base.All();
            }
            else
            {
                //�w�g�L�o��
                return this.All();
            }
        }

        public override void Delete(Product entity)
        {
            //�쥻gent��code
            //base.Delete(entity);

            //�q Entity Framework ��������ҫ����Ҫ��覡
            //this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;

            //�ݨD�G�אּ���R�����ơAmark�n�R������
            entity.IsDeleted = true;
            //��L�A��all ��where����

        }
    }

    public interface IProductRepository : IRepository<Product>
    {

    }
}