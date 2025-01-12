﻿using Microsoft.Extensions.DependencyInjection;

namespace Coupon.Infrastructure;

public class DependencyResolver
{
    public readonly IServiceProvider _serviceProvider;
    public DependencyResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TCommandType Resolve<TCommandType>()
    {
        return _serviceProvider.GetRequiredService<TCommandType>();
    }
}
