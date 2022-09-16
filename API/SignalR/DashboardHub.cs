using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class DashboardHub:Hub
    {
        private readonly IMediator _mediator;

        public DashboardHub(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}