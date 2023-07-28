using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

using BackendAPI.Services.Implementation;
using BackendAPI.Services.Users;
using AutoMapper;
using BackendAPI.DTOs;
using BackendAPI.Services.TypeContacts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/as pnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DbPhoneContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("stringSQL"));
});

builder.Services.AddScoped<InterfaceUserPhone, UsersService>();
builder.Services.AddScoped<InterfaceTypeContacts, TypeContactService>();
builder.Services.AddAutoMapper(typeof(InterfaceUserPhone));

builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolitic", app =>
    {
        app.AllowAnyOrigin();
        app.AllowAnyHeader();
        app.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

#region request API REST
app.MapGet("/interfaceTypeContacts", async (
    InterfaceTypeContacts _TypeContactService,
    IMapper _mapper
    ) =>
{
    List<TypeContact> listUsers = await _TypeContactService.GetList();

    List<TypeContactDTO> listUsersDTO = _mapper.Map<List<TypeContactDTO>>(listUsers);

    if (listUsersDTO.Count > 0)
    {
        return Results.Ok(listUsersDTO);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapGet("/phoneBook", async (
    InterfaceUserPhone _userService,
    IMapper _mapper
    ) =>
{
List<PhoneBook> listUsers = await _userService.GetList();

List<PhoneBookDTO> listUsersDTO = _mapper.Map<List<PhoneBookDTO>>(listUsers);

if (listUsersDTO.Count > 0)
{
    return Results.Ok(listUsersDTO);
}
else
{
    return Results.NotFound();
    }
});

app.MapPost("/phoneBook", async (
    PhoneBookDTO model,
    InterfaceUserPhone _userService,
    IMapper _mapper
    ) => {
        // Validate the required fields
        if (string.IsNullOrWhiteSpace(model.Name) || model.ContactTypeId == 0 || string.IsNullOrWhiteSpace(model.PhoneNumber))
        {
            return Results.BadRequest("Name, ContactTypeId, and PhoneNumber fields are required.");
        }

        PhoneBook _userSave = _mapper.Map<PhoneBook>(model);
        PhoneBook _userCreate = await _userService.Add(_userSave);

        if (_userCreate.Id != 0) return Results.Ok(_mapper.Map<PhoneBookDTO>(_userCreate));
        else return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });


app.MapPut("/phoneBook/{idUser}", async (
    int idUser,
    PhoneBookDTO model,
    InterfaceUserPhone _userService,
    IMapper _mapper
    ) =>
{
    var _userFind = await _userService.Get(idUser);
    if (_userFind is null) return Results.NotFound();

    var _User = _mapper.Map<PhoneBook>(model);
    if (string.IsNullOrWhiteSpace(_User.Name) || _User.ContactTypeId == 0 || string.IsNullOrWhiteSpace(_User.PhoneNumber))
    {
        return Results.BadRequest("Name, ContactTypeId, and PhoneNumber fields are required.");
    }
    _userFind.Name = _User.Name;
    _userFind.PhoneNumber = _User.PhoneNumber;
    _userFind.Gender = _User.Gender;
    _userFind.Email = _User.Email;
    _userFind.Comments = _User.Comments;
    _userFind.ContactType = _User.ContactType;

    var response = await _userService.Update(_userFind);

    if (response) return Results.Ok(_mapper.Map<PhoneBookDTO>(_userFind));
    else return Results.StatusCode(StatusCodes.Status500InternalServerError);
});

app.MapDelete("/phoneBook/{idUser}", async (
    int idUser,
    InterfaceUserPhone _userService
    ) => {
        var _userFind = await _userService.Get(idUser);
        if (_userFind is null) return Results.NotFound();

        var response = await _userService.Delete(_userFind);

        if (response) return Results.Ok();
        else return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });// TODO
#endregion

app.UseCors("NewPolitic");
app.Run();
