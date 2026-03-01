# TicketMonster
TicketMonster is a .NET Core web application built using a layered (Clean Architecture–style) structure.

## 📁 Project Structure

- `TicketMonster.Admin`
- `TicketMonster.ApplicationCore`
- `TicketMonster.Infrastructure`
- `TicketMonster.Web`
- `TicketMonster.UnitTest`

---
> ⚠️ **Important:** Store connection strings using User Secrets or environment variables.  
> Never commit credentials to source control.
# ⚙️ Environment Configuration
1. Database Connection Strings : "ConnectionStrings": {
    "TicketMonsterConnection": "Server=tcp:bs-2023-summer-04.database.windows.net,1433;Initial Catalog=TicketMonsterDb;Persist Security Info=False;User ID=bs;Password=P@ssword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }

2. Local SQL Server :  "ConnectionStrings": {
    "TicketMonsterConnection": "Server=(localdb)\\mssqllocaldb;Database=TicketMonster;Trusted_Connection=True;MultipleActiveResultSets=true"
  }

## Database Migration Workflow(.Net Core CLI)
There are two recommended approaches:
Scaffolding (Database → Code) — Recommended for initial setup
Migration (Code → Database) — Recommended for schema updates across environments


### Code First (NuGet Package Manager Console)
1. Modify the Entity Models。
2. Add a migration :  `add migration 'MigrationName'`。
3. Update the database : `update-database`。

### Code First From Existing DB(Scaffolding) (Right-click TicketMonsterWeb → Open Terminal)
1. If the database schema changes, regenerate models using : dotnet ef dbcontext scaffold "Name=ConnectionStrings:TicketMonsterConnection" Microsoft.EntityFrameworkCore.SqlServer --output-dir ../TicketMonster.ApplicationCore/Entities --context-dir ../TicketMonster.Infrastructure/Data --namespace TicketMonster.ApplicationCore.Entities --context TicketMonsterContext --context-namespace TicketMonster.Infrastructure.Data --startup-project ./TicketMonster.Web --force 
2. All generated models inside `Entities` must inherit from `BaseEntity`. (If not updated, the project will fail to build)

## Production Site
The production site updates only when changes are merged into the `master` branch.
`https://ticketmonsterfront.azurewebsites.net/`
## Local Development (ngrook)
`ngrok http 5155 --host-header="localhost:5155"`


