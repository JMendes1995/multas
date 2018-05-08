
   /// <summary>
   /// cria, caso não existam, as Roles de suporte à aplicação: Veterinario, Funcionario, Dono
   /// cria, nesse caso, também, um utilizador...
   /// </summary>
   private void iniciaAplicacao() {

      MultasDb db = new MultasDb ();

      var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
      var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

      // criar a Role 'Agente'
      if(!roleManager.RoleExists("Agente")) {
         // não existe a 'role'
         // então, criar essa role
         var role = new IdentityRole();
         role.Name = "Agente";
         roleManager.Create(role);
      }

     

	 // criar um utilizador 'Agente'
	 var user = new ApplicationUser();
	 user.UserName = "agente@mail.pt";
	 user.Email = "agente@mail.pt";
	 user.Nome = "Luís Freitas";
	 string userPWD = "123_Asd";
	 var chkUser = userManager.Create(user, userPWD);

	 //Adicionar o Utilizador à respetiva Role-Agente-
	 if(chkUser.Succeeded) {
		var result1 = userManager.AddToRole(user.Id, "Agente");
	 }
  }

 // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97
   
   
   
   