using CloudinaryDotNet.Actions;
using Dapper;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Utilities.Zlib;
using System;
using System.Collections.Generic;
using TicketMonster.Admin.Interface;
using TicketMonster.Admin.Models.Create;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces.EventService.EventDto;
using static System.Collections.Specialized.BitVector32;

namespace TicketMonster.Admin.Repository;

public class EventRepo : BaseRepo, IEventRepo
{
    public EventRepo(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<dynamic>> GetAllEvents()
    {
        var sql = "SELECT e.ID,e.EventName,ca.CategoryName,s.SubCategoryName,v.VenueName,e.EventDate,e.TotalTime,e.IsDeleted,e.CreateTime,\r\ne.ModifyTime,e.CreateBy,e.LastEditBy\r\nFROM events e\r\nINNER JOIN Category ca ON ca.ID = e.CategoryID\r\nINNER JOIN Venues v ON v.ID = e.VenueID\r\nINNER JOIN SubCategory s ON s.ID=e.SubCategoryID\r\n";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<int> CreateNewEvents(EventAndPicDto eventAndPicDto)
    {

        var sqlStatements = new List<string>();

        var eventsSql = "INSERT INTO Events(EventName, CategoryId, SubCategoryId, VenueId, EventDate, TotalTime, IsDeleted, CreateTime, ModifyTime, CreateBy, LastEditBy)" +
                        "VALUES(@EventName, @CategoryId, @SubCategoryId, @VenueId, @EventDate, @TotalTime, @IsDeleted, DATEADD(HOUR, 8, GETUTCDATE()), DATEADD(HOUR, 8, GETUTCDATE()), @CreateBy, @LastEditBy)" +
                        "SET @ID = SCOPE_IDENTITY()";

        sqlStatements.Add(eventsSql);

        foreach (var eventpic in eventAndPicDto.Images)
        {
            var eventsPicSql = "INSERT INTO EventsPic(EventID, Pic, Sort, CreateTime, ModifyTime, CreateBy, LastEditBy)" +
                           $"VALUES(SCOPE_IDENTITY(), '{eventpic.Pic}', {eventpic.Sort}, DATEADD(HOUR, 8, GETUTCDATE()), DATEADD(HOUR, 8, GETUTCDATE()), @CreateBy, @LastEditBy)";

            sqlStatements.Add(eventsPicSql);
        }

        foreach (var performer in eventAndPicDto.PerformersList)
        {
            var performerSql = "INSERT INTO EventPerform(EventID, PerfomerID, IsPrimary, CreateTime, ModifyTime, CreateBy, LastEditBy)" +
                               $"VALUES(@ID, {performer.ID},0,DATEADD(HOUR, 8, GETUTCDATE()) ,DATEADD(HOUR, 8, GETUTCDATE()), @CreateBy, @LastEditBy)";

            sqlStatements.Add(performerSql);
        }

        foreach (var seatsection in eventAndPicDto.Sections)
        {
            if (seatsection.SectionName != "Theater")
            {
                var seatsectionSql = $"DECLARE @SectionID INT,\r\n@j INT\r\n SET @j = 1\r\nSET @SectionID=0 INSERT INTO SeatSection(EventID, VenueID, SectionName, SectionPrice, SectionCapacity) VALUES(@ID, @VenueId,'{seatsection.SectionName}',{seatsection.SectionPrice},{seatsection.SectionCapacity})\r\n\tSET @SectionID = SCOPE_IDENTITY()\r\nWHILE @j <= {seatsection.SectionCapacity} BEGIN INSERT INTO SeatNum(VenueID, SeatNum, SeatSectionID, IsOrdered, RetainTime) VALUES(@VenueId,@j,@SectionID,0,DATEADD(DAY, 365, GETUTCDATE())) \r\nSET @j = @j + 1 \r\nEND";

                sqlStatements.Add(seatsectionSql);
            }
            else
            {
                //var seatsectionSql = $"DECLARE @i INT SET @i = 1 WHILE @i <= {seatsection.SectionCapacity} BEGIN INSERT INTO SeatSection(EventID, VenueID, SectionName, SectionPrice, SectionCapacity) VALUES(@ID, @VenueId,'{seatsection.SectionName}' + CAST(@i AS NVARCHAR),{seatsection.SectionPrice},{seatsection.SectionCapacity}) SET @i = @i + 1 END BEGIN INSERT INTO SeatNum(VenueID, SeatNum, SeatSectionID, IsOrdered, RetainTime) VALUES(@VenueId,1,@SectionID,0,DATEADD(DAY, 365, GETUTCDATE())) END SET @SectionID = SCOPE_IDENTITY(),@SectionID = @SectionID+1";


                var seatsectionSql = $"DECLARE @i INT, \r\n@SectionID INT\r\nSET @i = 1\r\nSET @SectionID=0\r\n\r\nWHILE @i <= {seatsection.SectionCapacity}\r\nBEGIN\r\n    INSERT INTO SeatSection(EventID, VenueID, SectionName, SectionPrice, SectionCapacity)\r\n    VALUES(@ID, @VenueId, '{seatsection.SectionName}' + CAST(@i AS NVARCHAR), {seatsection.SectionPrice}, {seatsection.SectionCapacity}) \r\n\tSET @SectionID = SCOPE_IDENTITY()\r\nINSERT INTO SeatNum(VenueID, SeatNum, SeatSectionID, IsOrdered, RetainTime)\r\nVALUES(@VenueId, 1, @SectionID, 0, DATEADD(DAY, 365, GETUTCDATE()))   \r\nSET @i = @i + 1\r\nSET @SectionID = @SectionID+1\r\nEND";




                sqlStatements.Add(seatsectionSql);
            }

        }




        var combinedSql = string.Join("", sqlStatements);

        var parameters = new
        {
            eventAndPicDto.EventName,
            eventAndPicDto.CategoryId,
            eventAndPicDto.SubCategoryId,
            eventAndPicDto.VenueId,
            eventAndPicDto.EventDate,
            eventAndPicDto.TotalTime,
            eventAndPicDto.IsDeleted,
            eventAndPicDto.CreateBy,
            eventAndPicDto.LastEditBy,
            eventAndPicDto.Pic,
            ID = 0         
        };

        int affectedRows = await ExecuteAsync<IEnumerable<dynamic>>(combinedSql, parameters);
        return affectedRows;
    }

    public async Task<IEnumerable<dynamic>> GetCategoryNameByCategoryId(int CategoryId)
    {
        var sql = $"SELECT CategoryName\r\nFROM Category\r\nWHERE Category.ID = {CategoryId} ";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetEventsById(int EventId)
    {
        var sql = "SELECT * FROM Events Where ID =1";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetAllCategoryName()
    {
        var sql = "SELECT ID,CategoryName FROM Category";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetAllSubCategoryNameAndCategoryId()
    {
        var sql = "SELECT ID,SubCategoryName,CatagoryID FROM SubCategory";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetAllVenueName()
    {
        var sql = "SELECT ID,VenueName FROM Venues";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetAllPerformerName()
    {
        var sql = "SELECT ID,Name,SubCategoryID FROM Performers";
        return await QueryAsync<dynamic>(sql);
    }


}


