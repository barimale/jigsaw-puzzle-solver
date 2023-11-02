using TypeGen.Core.TypeAnnotations;

namespace Christmas.Secret.Gifter.Domain
{
    [ExportTsInterface]
    public class Participant
    {
        public string Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EventId { get; set; }
        public IEnumerable<int> ExcludedOrderIds { get; set; }
        public Row[] ToInputDataRow(int amountOfParticipants)
        {
            var row = new Row[amountOfParticipants];

            for (int i = 1; i <= amountOfParticipants; i++)
            {
                if (i == OrderId)
                {
                    row[i - 1] = new Row() { arrayIndex = i, orderId = OrderId, value = -1 };
                }
                else if (ExcludedOrderIds.Contains(i))
                {
                    row[i - 1] = new Row() { arrayIndex = i, orderId = OrderId, value = 100 };
                }
                else
                {
                    row[i - 1] = new Row() { arrayIndex = i, orderId = OrderId, value = 0 };
                }
            }

            return row;
        }

        public class Row
        {
            public int arrayIndex { get; set; }
            public int orderId { get; set; }
            public int value { get; set; }
        }
    }
}
