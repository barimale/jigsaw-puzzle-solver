using Christmas.Secret.Gifter.Domain;
using System.Collections.Generic;

namespace Algorithm.ConstraintsPairing.Model.Requests
{
    public class AlgorithmRequest
    {
        public List<Participant> Data { get; set; } = new List<Participant>();

        public InputData ToInputData()
        {
            var costs = new int[Data.Count, Data.Count];

            for (int i = 0; i < Data.Count; i++)
            {
                var row = Data[i].ToInputDataRow(Data.Count);
                for (int j = 0; j < row.Length; j++)
                {
                    costs[i, j] = row[j].value;
                }
            }

            return new InputData()
            {
                Costs = costs
            };
        }
    }
}
