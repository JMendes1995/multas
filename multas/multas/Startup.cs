using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using multas.Models;
using Owin;

namespace multas
{
    public partial class Startup
    {
        /// <summary>
        /// este metodo é executado apenas 1 vez 
        /// quando a aplicação arranca 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //invocar o metodo para iniciar a config
            IniciaAplicacao();
        }

        /// <summary>
        /// cria, caso não existam, as Roles de suporte à aplicação: agentes, condutores
        /// cria, nesse caso, também, um utilizador...
        /// </summary>
        private void IniciaAplicacao()
        {

            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Agente'
            if (!roleManager.RoleExists("Agentes"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Agentes";
                roleManager.Create(role);
            }
            // criar a Role 'GestaoPessoal'
            if (!roleManager.RoleExists("GestaoPessoal"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "GestaoPessoal";
                roleManager.Create(role);
            }


            // criar um utilizador 'Agentes'
            var user = new ApplicationUser();
            user.UserName = "tania@mail.pt";
            user.Email = "tania@mail.pt";
            //user.Nome = "Luís Freitas";
            string userPWD = "123A_s";
            var chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Agente-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "Agentes");
            }


            // criar um utilizador 'GestaoPessoal'-andre
            user = new ApplicationUser();
            user.UserName = "andre@mail.pt";
            user.Email = "andre@mail.pt";
            //user.Nome = "Luís Freitas";
             userPWD = "123A_s";
             chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-GestaoPessoal-
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, "GestaoPessoal");
            }


        }



        // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97





    }
}
