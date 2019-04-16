using BlogProject.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BLL.Repository.Entity
{
    public class AppUserRepository:BaseRepository<AppUser>
    {
        public bool CheckCredentials(string UserName, string Password)
        {
            return table.Any(x => x.UserName == UserName && x.Password == Password);
        }

        public AppUser FindByUserName(string UserName)
        {
            return table.FirstOrDefault(x => x.UserName == UserName);

            //return table.Where(x=> x.UserName== UserName).FirstOrDefault();
        }
    }
}
