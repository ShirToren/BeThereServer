﻿using BeTherServer.Models;
using BeTherMongoDB.Services;
using BeTherServer.Services;
using BeTherServer.MongoContext;
using BeTherServer.Services.UpdateLocationService;
using BeTherServer.Services.NotificationsService;
using BeTherServer.Services.ChatService;
using BeTherServer.Services.AnswerService;
using BeTherServer.Services.UserRoomsService;
using BeTherServer.Services.CreditsService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoService"));

//chat
builder.Services.AddSignalR();

builder.Services.AddSingleton<IConnectToAppDBContext,ConnectToAppMongoContext>();
builder.Services.AddSingleton<IAskedQuestionDBContext, AskedQuestionsMongoContext>();
builder.Services.AddSingleton<IQuestionAnswersDBContext, QuestionAnswersMongoContext>();
builder.Services.AddSingleton<IChatMessagesDBContext, ChatMessagesMongoContext>();
builder.Services.AddSingleton<IUserDBContext, UserMongoContext>();
builder.Services.AddSingleton<IUserRoomsDBContext, UserRoomsMongoContext>();

builder.Services.AddSingleton<UserQuestionsService>();
builder.Services.AddScoped<IConnectToAppService, ConnectToAppService>();
builder.Services.AddSingleton<IAskedQuestionService, UserQuestionsService>();
builder.Services.AddSingleton<IUpdateLocationService, UpdateLocationService>();
builder.Services.AddSingleton<INotificationsService, NotificationsService>();
builder.Services.AddSingleton<IAnswerService, AnswerService>();
builder.Services.AddSingleton<IChatService, ChatService>();
builder.Services.AddSingleton<IUserRoomsService, UserRoomsService>();
builder.Services.AddSingleton<ICreditsService, CreditsService>();



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

