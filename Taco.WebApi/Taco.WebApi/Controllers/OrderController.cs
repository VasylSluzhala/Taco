using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Taco.WebApi.Controllers
{
    /// <summary>
    /// Controller for order processing.
    /// </summary>
    public class OrderController : BaseController
    {
        public OrderController()
        {
        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="menuItemIds">List of menu item ids</param>
        /// <returns>OrderDto object</returns>
        [HttpPost]
        public ActionResult AddOrder([FromBody] List<int> menuItemIds)
        {
            if (menuItemIds == null || menuItemIds.Count <= 0)
            {
                throw new ArgumentException($"Wrong parameter {nameof(menuItemIds)}");
            }

            return Ok();
        }
    }
}
