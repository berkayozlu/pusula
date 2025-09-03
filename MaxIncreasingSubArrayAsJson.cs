using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class MaxIncreasingSubArrayAsJsonSolution
{
    public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
    {
        
        if (numbers == null || numbers.Count == 0)
        {
            return JsonSerializer.Serialize(new List<int>());
        }

        
        if (numbers.Count == 1)
        {
            return JsonSerializer.Serialize(numbers);
        }

        List<int> maxSubArray = new List<int>();
        int maxSum = int.MinValue;
        
        
        for (int i = 0; i < numbers.Count; i++)
        {
            List<int> currentSubArray = new List<int> { numbers[i] };
            int currentSum = numbers[i];
            
            
            for (int j = i + 1; j < numbers.Count; j++)
            {
                if (numbers[j] >= numbers[j - 1])
                {
                    currentSubArray.Add(numbers[j]);
                    currentSum += numbers[j];
                }
                else
                {
                    break;
                }
            }
            
            
            
            if (currentSum > maxSum)
            {
                maxSum = currentSum;
                maxSubArray = new List<int>(currentSubArray);
            }
        }
        
        return JsonSerializer.Serialize(maxSubArray);
    }
}
