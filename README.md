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