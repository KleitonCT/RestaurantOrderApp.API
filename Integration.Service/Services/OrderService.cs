using Integration.Domain.Entities.Response;
using Integration.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Service.Services
{
    public class OrderService : IOrderService
    {
        public async Task<OrderResponse> ProcessOrder(string input)
        {
            var morningDishies = new Dictionary<string, string>
            {
                {"1", "eggs" },
                {"2", "Toast" },
                {"3", "coffee" }
            };
            var nightDishies = new Dictionary<string, string>
            {
                {"1", "steak" },
                {"2", "potato" },
                {"3", "wine" },
                {"4", "cake" }
            };
            var splitInput = input.Split(',');

            if (splitInput[0].ToLower() != "morning" && splitInput[0].ToLower() != "night")
                return await Task.FromResult(new OrderResponse { ErrorMessage = "Invalid time of day." });

            Array.Sort(splitInput.Skip(1).ToArray());
            if (splitInput[0].ToLower() == "morning")
                return await Task.FromResult(new OrderResponse { Output = BuildOutput(morningDishies, splitInput.Skip(1).ToArray()) });
            else
                return await Task.FromResult(new OrderResponse { Output = BuildOutput(nightDishies, splitInput.Skip(1).ToArray()) });
        }        

        private string BuildOutput(Dictionary<string, string> dishies, string[] input)
        {
            string output = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (!dishies.Keys.Contains(input[i].Trim()))
                    return "Invalid dishy type.";

                output += dishies.FirstOrDefault(x => x.Key == input[i].Trim()).Value + ",";
            }

            return output.Substring(0, output.Length - 1);
        }
    }
}
