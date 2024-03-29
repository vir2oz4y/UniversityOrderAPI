﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Order;

public record GetOrderCommand(
    int StudentStoreId,
    int OrderId
) : ICommand;

public record GetOrderCommandResult(
    OrderDTO Order    
) : ICommandResult;

public class GetOrderCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetOrderCommand, GetOrderCommandResult>
{
    public GetOrderCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<GetOrderCommandResult> Handle(GetOrderCommand request, CancellationToken? cancellationToken)
    {
        var order = DbContext.Order.Where(el =>
                el.StudentStoreId == request.StudentStoreId && el.Id == request.OrderId)
            .Include(el => el.Client)
            .Include(el => el.Items)
            .SingleOrDefault();

        if (order is null)
            throw new Exception($"Order with id {request.OrderId} not found");

        return Task.FromResult(new GetOrderCommandResult(
            order.Adapt<OrderDTO>()));
    }
}