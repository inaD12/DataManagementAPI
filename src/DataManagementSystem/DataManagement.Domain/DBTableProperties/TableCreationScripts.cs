namespace DataManagement.Domain.DBTableProperties
{
	public static class TableCreationScripts
	{
		public static readonly Dictionary<string, string> AllCreationQueries = new()
		{
			{ "Country",  CountryTableQuery },
			{ "Organization", OrganizationTableQuery },
			{ "Industry", IndustryTableQuery },
			{ "IndustryOrganization", IndustryOrganizationTableQuery },
			{ "UserRole", UserRoleTableQuery },
            {"User", UserTableQuery }
		};


		public const string CountryTableQuery = @"CREATE TABLE [Country](
    Id varchar(36) NOT NULL PRIMARY KEY,
    Name varchar(60) NOT NULL UNIQUE,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
);";

		public const string OrganizationTableQuery = @"CREATE TABLE [Organization](
    Id varchar(36) NOT NULL PRIMARY KEY,
    OrganizationId varchar(50) NOT NULL UNIQUE,
    Name varchar(300) NOT NULL,
    Website varchar(300) NOT NULL,
    CountryId varchar(36) FOREIGN KEY REFERENCES [Country](Id) ON DELETE SET NULL,
    Description text NOT NULL,
    Founded int NOT NULL,
    NumberOfEmployees int NOT NULL,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
);";

		public const string IndustryTableQuery = @"CREATE TABLE [Industry](
    Id varchar(36) NOT NULL PRIMARY KEY,
    Name varchar(300) NOT NULL UNIQUE,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
);";

		public const string IndustryOrganizationTableQuery = @"CREATE TABLE [IndustryOrganization](
    OrganizationId varchar(36) NOT NULL FOREIGN KEY REFERENCES [Organization](Id) ON DELETE CASCADE,
    IndustryId varchar(36) NOT NULL FOREIGN KEY REFERENCES [Industry](Id) ON DELETE CASCADE,
    PRIMARY KEY (OrganizationId, IndustryId)
);";

		public const string UserRoleTableQuery = @"CREATE TABLE [UserRole](
    Id varchar(36) NOT NULL PRIMARY KEY,
    Name varchar(50) NOT NULL UNIQUE,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
);";

		public const string UserTableQuery = @"CREATE TABLE [User] (
	Id varchar(36) NOT NULL PRIMARY KEY,
	Name varchar(50) NOT NULL UNIQUE,
	PasswordHash text NOT NULL,
	Salt text NOT NULL,
	FirstName varchar(50),
	LastName varchar(50),
    UserRoleId varchar(36) NULL FOREIGN KEY REFERENCES [UserRole](Id) ON DELETE SET NULL,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
);";
	}
}
