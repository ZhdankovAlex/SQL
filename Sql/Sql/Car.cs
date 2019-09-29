namespace Sql
{
    class Car
    {
        public string Name { get; set; }
        public Car(string name)
        {
            this.Name = name;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
