#Neat.Procedure
Do you think your life is harder only because you have to execute some stored procedures?

So Neat.Procedure is for you.

A stored procedure executer in a neat way.

Why don't you call like this?

	object oneScalarReturn = ProcedureExecuter.ExecuteScalar("YourSPName");

or like this?
	
	int count = ProcedureExecuter.ExecuteNonQuery("YourSPName");
	
or even like this?

	IEnumerable<YourDomainClass> list = ProcedureExecuter.ExecuteReader<YourDomainClass>("YourSPName");
	
No mapping between classes and stored procedures results.

Just that. Simple, fast and Neat.

Duh... Aren't you forgetting stored procedure parameters?

Sure. You can pass it like arguments or like a key/value Dictionary<string, object>.

	string arg1 = "arg1";
	int arg2 = 2;
	bool arg3 = true;
	DateTime arg4 = DateTime.Now;
	ProcedureExecuter.ExecuteNonQuery("YourSPName", arg1, arg2, arg3, arg4);

or

	Dictionary<string, object> parameters = new Dictionary<string, object>()
	{
		{ "arg1", "arg1" },
		{ "arg2", 2 },
		{ "arg3", true },
		{ "arg4", DateTime.Now }
	};
	ProcedureExecuter.ExecuteNonQuery("YourSPName", parameters);

By default, connectionStringName is Neat.Procedure.Settings.ConnectionString.Default

But can be changed by
	
	Connection.ConnectionStringName("YourConnectionStringNameHere");

or 

	Connection.ConnectionString("server=(local)\SQLEXPRESS;database=DBName;Integrated Security=SSPI");

You can install it via NuGet

	Install-Package Neat.Procedure
