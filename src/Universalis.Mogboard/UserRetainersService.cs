﻿using MySqlConnector;
using Universalis.Mogboard.Entities;
using Universalis.Mogboard.Entities.Id;

namespace Universalis.Mogboard;

internal class UserRetainersService : IMogboardTable<UserRetainer, UserRetainerId>
{
    private readonly string _username;
    private readonly string _password;
    private readonly string _database;
    private readonly int _port;

    public UserRetainersService(string username, string password, string database, int port)
    {
        _username = username;
        _password = password;
        _database = database;
        _port = port;
    }

    public async Task<UserRetainer?> Get(UserRetainerId id, CancellationToken cancellationToken = default)
    {
        await using var db = new MySqlConnection($"User ID={_username};Password={_password};Database={_database};Server=localhost;Port={_port}");
        await db.OpenAsync(cancellationToken);

        await using var command = db.CreateCommand();
        command.CommandText = "select * from dalamud.users_retainers where id=@id limit 1;";
        command.Parameters.Add("@id", MySqlDbType.VarChar);
        command.Parameters["@id"].Value = id.ToString();

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken) ? UserRetainer.FromReader(reader) : null;
    }
}