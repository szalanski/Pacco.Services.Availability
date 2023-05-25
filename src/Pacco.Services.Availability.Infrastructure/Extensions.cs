﻿using Convey;
using Convey.CQRS.Queries;
using Convey.Docs.Swagger;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Availability.Application.Attributes;
using Pacco.Services.Availability.Application.Events;
using Pacco.Services.Availability.Application.Events.External;
using Pacco.Services.Availability.Application.Services;
using Pacco.Services.Availability.Core.Repositories;
using Pacco.Services.Availability.Infrastructure.Exceptions;
using Pacco.Services.Availability.Infrastructure.Mongo.Documents;
using Pacco.Services.Availability.Infrastructure.Mongo.Repositories;
using Pacco.Services.Availability.Infrastructure.Services;
using System;

namespace Pacco.Services.Availability.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IResourcesRepository, ResourcesMongoRepository>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddTransient<IEventProcessor, EventProcessor>();
            builder.Services.AddSingleton<IEventMapper, EventMapper>();

            builder.Services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies()).AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());


            builder.AddErrorHandler<ExceptionToResponseMapper>()
                .AddMongo()
                .AddMongoRepository<ResourceDocument, Guid>("resources")
                .AddInMemoryQueryDispatcher()
                .AddQueryHandlers()
                .AddRabbitMq()
                .AddSwaggerDocs()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                ;

            return builder;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
               .UseConvey()
               .UsePublicContracts<ContractAttribute>()
               .UseSwaggerDocs()
               .UseRabbitMq()
               .SubscribeEvent<SignedUp>();

            return app;
        }
    }
}
