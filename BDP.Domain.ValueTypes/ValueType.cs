namespace BDP.Domain.ValueTypes;

public abstract class ValueType
{
    protected static bool EqualOperator(ValueType left, ValueType right)
    {
        if (left is null ^ right is null)
            return false;
        
        return left is null || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueType left, ValueType right)
        => !(EqualOperator(left, right));

    protected abstract IEnumerable<object> GetAtomicValues();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (ValueType)obj;
        var thisValues = GetAtomicValues().GetEnumerator();
        var otherValues = other.GetAtomicValues().GetEnumerator();
        
        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (thisValues.Current is null ^ otherValues.Current is null)
                return false;

            if (thisValues.Current != null &&
                !thisValues.Current.Equals(otherValues.Current))
            {
                return false;
            }
        }

        return !thisValues.MoveNext() && !otherValues.MoveNext();
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
         .Select(x => x != null ? x.GetHashCode() : 0)
         .Aggregate((x, y) => x ^ y);
    }
}