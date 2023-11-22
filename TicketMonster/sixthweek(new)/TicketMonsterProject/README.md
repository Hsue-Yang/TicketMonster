# TicketMonster
## 環境設定(記得在使用者秘密設定)
1. 連線到Azure雲端資料庫 : "ConnectionStrings": {
    "TicketMonsterConnection": "Server=tcp:bs-2023-summer-04.database.windows.net,1433;Initial Catalog=TicketMonsterDb;Persist Security Info=False;User ID=bs;Password=P@ssword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }

2. 連線到Local資料庫 :  "ConnectionStrings": {
    "TicketMonsterConnection": "Server=(localdb)\\mssqllocaldb;Database=TicketMonster;Trusted_Connection=True;MultipleActiveResultSets=true"
  }

## 資料庫異動流程(使用.Net Core CLI)
以下兩種方式建議第一次建立Entity及DbContext時可以使用Scaffold方式還原；遷移資料庫時再使用Migration的方式更新到其他環境的資料庫。


### Code First (NuGet套件管理主控台)
1. 修改Entity Model。
2. 新增Migration異動紀錄 :  `add migration '填上自己取的異動記錄名稱'`。
3. 更新/產出資料庫 : `update-database`。

### Code First From DB(Scaffolding) (對方案 TicketMonsterWeb 按右鍵 Terminal 在終端中開啟)
1. 修改資料庫Schema。
2. Scaffolding資料庫 : dotnet ef dbcontext scaffold "Name=ConnectionStrings:TicketMonsterConnection" Microsoft.EntityFrameworkCore.SqlServer --output-dir ../TicketMonster.ApplicationCore/Entities --context-dir ../TicketMonster.Infrastructure/Data --namespace TicketMonster.ApplicationCore.Entities --context TicketMonsterContext --context-namespace TicketMonster.Infrastructure.Data --startup-project ./TicketMonster.Web --force 
3. 修改 `Entites` 下的 Model 要繼承 `BaseEntity` (沒修改的話會無法建置)。

## 站臺位置
只有當master上有變更 這裡才會變更
https://ticketmonsterfront.azurewebsites.net/
ngrok http 5155 --host-header="localhost:5155"


