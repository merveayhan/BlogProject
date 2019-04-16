using BlogProject.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DAL.ORM.Map
{
    public class AppUserMap:BaseMap<AppUser>
    {
        public AppUserMap()
        {
            ToTable("dbo.Users");
            Property(x => x.UserName).HasMaxLength(50).IsRequired();
            Property(x => x.Password).HasMaxLength(50).IsRequired();
            Property(x => x.Email).IsOptional();
            Property(x => x.ImagePath).IsOptional();
            Property(x => x.Role).IsOptional();
            Property(x => x.FirstName).IsOptional();
            Property(x => x.LastName).IsOptional();


            HasMany(x => x.Articles).WithRequired(x => x.AppUser).HasForeignKey(x => x.AppUserID).WillCascadeOnDelete(false);

            HasMany(x => x.Comments).WithRequired(x => x.AppUser).HasForeignKey(x => x.AppUserID).WillCascadeOnDelete(false);

            HasMany(x => x.Likes).WithRequired(x => x.AppUser).HasForeignKey(x => x.AppUserID).WillCascadeOnDelete(false);





        }
    }
}
