using Exo_Base.Core.Extensions;
using Exo_Base.Core.Identity;
using Exo_Base.Data.Entities;
using Exo_Base.Data.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Exo_Base.Data
{
    public class ApplicationDbInitializer: CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        private const string _SuperUserEmail = "superuser@exolutus.com";
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeDatabaseForEF(context);
            base.Seed(context);
        }

        protected void InitializeDatabaseForEF(ApplicationDbContext context)
        {
            InitRoleAndUser(context);
            InitNavigationMenus(context);
        }
        
        protected void InitRoleAndUser(ApplicationDbContext context)
        {
            var password = "Apptest@1$#";

            var userManager = new ApplicationUserManager(new ApplicationUserStore(context));
            var roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));

            roleManager.Create(new ApplicationIdentityRole
            {
                Name = SystemRole.SuperUser.ToString(),
                Description = SystemRole.SuperUser.GetDescription(),
                IsSystemConfig = true,
                InsertedOnUtc = DateTime.UtcNow
            });

            var superuser = new ApplicationIdentityUser()
            {
                Email = _SuperUserEmail,
                EmailConfirmed = true,
                UserName = _SuperUserEmail,
                FirstName = "Super",
                MiddleName = "",
                LastName = "User",
                InsertedOnUtc = DateTime.UtcNow,
            };

            var result = userManager.Create(superuser, password);
            if (result.Succeeded)
            {
                superuser.LastModifiedUserId = superuser.Id;
                result = userManager.Update(superuser);
                if(result.Succeeded)
                    userManager.AddToRole(superuser.Id, SystemRole.SuperUser.ToString());
            }
        }
        protected void InitNavigationMenus(ApplicationDbContext context)
        {
            var MainMenuType = "MainMenu";
            var navigationType = new string[3]{
                "GroupTitle",
                MainMenuType,
                "InternalMenu"
            };

            foreach (var type in navigationType)
            {
                if(context.NavigationTypes.Where(x=>x.NavigationTypeName==type).FirstOrDefault() == null)
                    context.Entry(new NavigationTypeEntity() { NavigationTypeName = type }).State = EntityState.Added;
            }
            context.SaveChanges();


            var navTypeEntity = context.NavigationTypes.Where(x => x.NavigationTypeName == MainMenuType).FirstOrDefault();
            var superUserId = context.Users.Where(x => x.UserName == _SuperUserEmail).FirstOrDefault().Id;
            var currentUtcDate = DateTime.UtcNow;

            var roleMenu = new RoleNavigationMenuEntity() {
                RoleId = context.Roles.Where(x => x.Name == SystemRole.SuperUser.ToString()).FirstOrDefault().Id
            };
            var menu = new NavigationMenuEntity();

            if (context.NavigationMenus.Where(x => x.Name == "Home").FirstOrDefault() == null)
            {
                menu = new NavigationMenuEntity();
                menu.Name = "Home";
                menu.DisplayName = "Home";
                menu.ControllerName = "Home";
                menu.ActionName = "Index";
                menu.DisplayOrder = 2;
                menu.LastModifiedUserId = superUserId;
                menu.InsertedOnUtc = currentUtcDate;
                menu.NavigationType = navTypeEntity;

                context.Entry(menu).State = EntityState.Added;
                context.SaveChanges();

                roleMenu.NavigationId = menu.Id;
                context.Entry(roleMenu).State = EntityState.Added;
                context.SaveChanges();
            }

            if (context.NavigationMenus.Where(x => x.Name == "NavigationMenu").FirstOrDefault() == null)
            {
                menu = new NavigationMenuEntity();
                menu.Name = "NavigationMenu";
                menu.DisplayName = "Navigation Menu";
                menu.ControllerName = "Navigation";
                menu.ActionName = "Index";
                menu.DisplayOrder = 2;
                menu.LastModifiedUserId = superUserId;
                menu.InsertedOnUtc = currentUtcDate;
                menu.NavigationType = navTypeEntity;
                context.Entry(menu).State = EntityState.Added;
                context.SaveChanges();

                roleMenu.NavigationId = menu.Id;
                context.Entry(roleMenu).State = EntityState.Added;
                context.SaveChanges();
            }

            if (context.NavigationMenus.Where(x => x.Name == "Users").FirstOrDefault() == null)
            {
                menu = new NavigationMenuEntity();
                menu.Name = "Users";
                menu.DisplayName = "Users";
                menu.ControllerName = "UserAdmin";
                menu.ActionName = "Index";
                menu.DisplayOrder = 3;
                menu.LastModifiedUserId = superUserId;
                menu.InsertedOnUtc = currentUtcDate;
                menu.NavigationType = navTypeEntity;
                context.Entry(menu).State = EntityState.Added;
                context.SaveChanges();

                roleMenu.NavigationId = menu.Id;
                context.Entry(roleMenu).State = EntityState.Added;
                context.SaveChanges();
            }
            if (context.NavigationMenus.Where(x => x.Name == "Roles").FirstOrDefault() == null)
            {
                menu = new NavigationMenuEntity();
                menu.Name = "Roles";
                menu.DisplayName = "Roles";
                menu.ControllerName = "RoleAdmin";
                menu.ActionName = "Index";
                menu.DisplayOrder = 4;
                menu.LastModifiedUserId = superUserId;
                menu.InsertedOnUtc = currentUtcDate;
                menu.NavigationType = navTypeEntity;
                context.Entry(menu).State = EntityState.Added;
                context.SaveChanges();

                roleMenu.NavigationId = menu.Id;
                context.Entry(roleMenu).State = EntityState.Added;
                context.SaveChanges();
            }
            
        }
    }
}
