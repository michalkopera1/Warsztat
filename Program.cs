using Warsztat;

namespace Warsztat
{

    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        public byte Hours { get; }
        public byte Minutes { get; }
        public byte Seconds { get; }

        public Time(byte hours, byte minutes, byte seconds)
        {
            if (hours >= 24 || minutes >= 60 || seconds >= 60)
            {
                throw new ArgumentException("Invalid time specified.");
            }
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public Time(byte hours, byte minutes) : this(hours, minutes, 0) { }

        public Time(byte hours) : this(hours, 0, 0) { }

        public Time(string time)
        {
            var parts = time.Split(':');
            if (parts.Length < 1 || parts.Length > 3)
            {
                throw new ArgumentException("Invalid time format.");
            }

            byte hours = byte.Parse(parts[0]);
            byte minutes = parts.Length > 1 ? byte.Parse(parts[1]) : (byte)0;
            byte seconds = parts.Length > 2 ? byte.Parse(parts[2]) : (byte)0;

            if (hours >= 24 || minutes >= 60 || seconds >= 60)
            {
                throw new ArgumentException("Invalid time values.");
            }

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public override string ToString()
        {
            return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
        }

        public bool Equals(Time other)
        {
            return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        }

        public int CompareTo(Time other)
        {
            if (Hours != other.Hours) return Hours.CompareTo(other.Hours);
            if (Minutes != other.Minutes) return Minutes.CompareTo(other.Minutes);
            return Seconds.CompareTo(other.Seconds);
        }

        public static bool operator ==(Time left, Time right) => left.Equals(right);
        public static bool operator !=(Time left, Time right) => !left.Equals(right);
        public static bool operator <(Time left, Time right) => left.CompareTo(right) < 0;
        public static bool operator <=(Time left, Time right) => left.CompareTo(right) <= 0;
        public static bool operator >(Time left, Time right) => left.CompareTo(right) > 0;
        public static bool operator >=(Time left, Time right) => left.CompareTo(right) >= 0;

        public Time Plus(TimePeriod period)
        {
            long totalSeconds = Hours * 3600L + Minutes * 60L + Seconds + period.Seconds;
            totalSeconds %= 86400;

            byte newHours = (byte)(totalSeconds / 3600);
            byte newMinutes = (byte)((totalSeconds / 60) % 60);
            byte newSeconds = (byte)(totalSeconds % 60);

            return new Time(newHours, newMinutes, newSeconds);
        }

        public static Time operator +(Time time, TimePeriod period)
        {
            return time.Plus(period);
        }

        public override bool Equals(object obj)
        {
            return obj is Time other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hours, Minutes, Seconds);
        }
    }

    public readonly struct TimePeriod
    {
        public long Seconds { get; }

        public TimePeriod(long seconds)
        {
            if (seconds < 0)
                throw new ArgumentException("Seconds cannot be negative.");
            Seconds = seconds;
        }

        public TimePeriod(int hours, int minutes, int seconds) : this((long)hours * 3600 + minutes * 60 + seconds)
        {
            if (hours < 0 || minutes < 0 || seconds < 0)
                throw new ArgumentException("Hours, minutes, and seconds cannot be negative.");
        }

        public TimePeriod(int hours, int minutes) : this(hours, minutes, 0) { }

        public TimePeriod(int hours) : this(hours, 0, 0) { }

        public TimePeriod(Time start, Time end)
        {
            if (end < start)
                throw new ArgumentException("End time must be later than start time.");

            long startSeconds = start.Hours * 3600 + start.Minutes * 60 + start.Seconds;
            long endSeconds = end.Hours * 3600 + end.Minutes * 60 + end.Seconds;
            Seconds = endSeconds - startSeconds;
        }

        public TimePeriod(string time)
        {
            var parts = time.Split(':');
            if (parts.Length != 3)
                throw new ArgumentException("Time must be in h:mm:ss format.");

            int hours = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);
            int seconds = int.Parse(parts[2]);

            if (hours < 0 || minutes < 0 || minutes >= 60 || seconds < 0 || seconds >= 60)
                throw new ArgumentException("Invalid time values.");

            Seconds = (long)hours * 3600 + minutes * 60 + seconds;
        }

        public override string ToString()
        {
            long totalSeconds = Seconds;
            long hours = totalSeconds / 3600;
            long minutes = (totalSeconds % 3600) / 60;
            long seconds = totalSeconds % 60;
            return $"{hours:D}:{minutes:D2}:{seconds:D2}";
        }
    }

   
}

