CREATE TABLE [Country](
    Id varchar(36) NOT NULL PRIMARY KEY,
    Name varchar(60) NOT NULL UNIQUE,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
);

CREATE TABLE [Organization](
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
);

CREATE TABLE [Industry](
    Id varchar(36) NOT NULL PRIMARY KEY,
    Name varchar(300) NOT NULL UNIQUE,
    CreatedAt datetime NOT NULL,
    DeletedAt datetime NULL,
);

CREATE TABLE [IndustryOrganization](
    OrganizationId varchar(36) NOT NULL FOREIGN KEY REFERENCES [Organization](Id) ON DELETE CASCADE,
    IndustryId varchar(36) NOT NULL FOREIGN KEY REFERENCES [Industry](Id) ON DELETE CASCADE,
    PRIMARY KEY (OrganizationId, IndustryId)
);