﻿using BeTherServer.Models;
using BeTherMongoDB.Services;
using BeTherServer.Services;
using BeTherServer.MongoContext;
using BeTherServer.Chat;
using BeTherServer.Services.UpdateLocationService;
using BeTherServer.Services.NotificationsService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoService"));

//chat
builder.Services.AddSignalR();

builder.Services.AddSingleton<IConnectToAppDBContext,ConnectToAppMongoContext>();
builder.Services.AddSingleton<IQuestionAskedDBContext, AskedQuestionsMongoContext>();
builder.Services.AddSingleton<QuestionsAskedService>();
builder.Services.AddScoped<IConnectToAppService, ConnectToAppService>();
builder.Services.AddSingleton<IQuestionsAskedService, QuestionsAskedService>();
builder.Services.AddSingleton<IUpdateLocationService, UpdateLocationService>();
builder.Services.AddSingleton<INotificationsService, NotificationsService>();


// Add services to the container
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//chat
app.MapHub<ChatHub>("/chat");
app.Run();