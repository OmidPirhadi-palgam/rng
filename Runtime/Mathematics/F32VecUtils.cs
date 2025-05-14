using FixMath;

namespace Mathematics
{
    public static class F32MathUtils
    {
        public static bool IsSegmentCrossingRect(F32Vec2 segStart, F32Vec2 segEnd, F32Vec2 rectPos, F32Vec2 rectSize)
        {
            // Define the rectangle's corners
            F32Vec2 rectTopLeft = rectPos;
            F32Vec2 rectTopRight = new F32Vec2(rectPos.X + rectSize.X, rectPos.Y);
            F32Vec2 rectBottomLeft = new F32Vec2(rectPos.X, rectPos.Y + rectSize.Y);
            F32Vec2 rectBottomRight = new F32Vec2(rectPos.X + rectSize.X, rectPos.Y + rectSize.Y);

            // Check if either end of the segment is inside the rectangle
            if (IsPointInsideRect(segStart, rectPos, rectSize) || IsPointInsideRect(segEnd, rectPos, rectSize))
            {
                return true;
            }

            // Check if the segment crosses any of the rectangle's edges
            return IsSegmentCrossingSegment(segStart, segEnd, rectTopLeft, rectTopRight) ||
                   IsSegmentCrossingSegment(segStart, segEnd, rectTopRight, rectBottomRight) ||
                   IsSegmentCrossingSegment(segStart, segEnd, rectBottomRight, rectBottomLeft) ||
                   IsSegmentCrossingSegment(segStart, segEnd, rectBottomLeft, rectTopLeft);
        }

        // Helper function to check if a point is inside the rectangle
        private static bool IsPointInsideRect(F32Vec2 point, F32Vec2 rectPos, F32Vec2 rectSize)
        {
            return (point.X >= rectPos.X - rectSize.X / 2 && point.X <= rectPos.X + rectSize.X / 2 &&
                    point.Y >= rectPos.Y - rectSize.Y / 2 && point.Y <= rectPos.Y + rectSize.Y / 2);
        }

        public static bool IsSegmentCrossingSegment(F32Vec2 segA, F32Vec2 segB, F32Vec2 segC, F32Vec2 segD)
        {
            var a = segB - segA;
            var b = segC - segD;
            var c = segC - segA;
            var d = segD - segA;
            var e = segC - segB;
            var f = segD - segB;
            var cross1 = a.Cross(c) * a.Cross(d);
            var cross2 = b.Cross(e) * b.Cross(f);
            return cross1 <= 0 && cross2 <= 0;
        }

        public static F32 Cross(this F32Vec2 a, F32Vec2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static F32 DistanceTo(this F32Vec2 f32Vec2, F32Vec2 other)
        {
            var deltaX = (f32Vec2.X - other.X);
            var deltaY = (f32Vec2.Y - other.Y);
            var deltaX2 = deltaX * deltaX;
            var deltaY2 = deltaY * deltaY;
            var distanceTo = F32.Sqrt(deltaX2 + deltaY2);
            return distanceTo;
        }

        public static F32 ApproximateRealDistanceTo(this F32Vec3 f32Vec3, F32Vec3 other)
        {
            var deltaX = (f32Vec3.X - other.X);
            var deltaY = (f32Vec3.Y - other.Y);
            var deltaZ = (f32Vec3.Z - other.Z);

            var absDeltaX = F32.Abs(deltaX);
            var absDeltaY = F32.Abs(deltaY);
            var absDeltaZ = F32.Abs(deltaZ);

            var maxAbsDelta = F32.Max(F32.Max(absDeltaX, absDeltaY), absDeltaZ);
            var minAbsDelta = F32.Min(F32.Min(absDeltaX, absDeltaY), absDeltaZ);
            var midAbsDelta = absDeltaX + absDeltaY + absDeltaZ - maxAbsDelta - minAbsDelta;

            var distanceTo = F32.Ratio100(94) * maxAbsDelta + F32.Ratio100(38) * midAbsDelta +
                             F32.Ratio100(22) * minAbsDelta;
            return distanceTo;
        }


        public static F32Vec2 Lerp(F32Vec2 a, F32Vec2 b, F32 t)
        {
            return new F32Vec2(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
        }

        public static F32Vec2 ToF32Vec2(this F32Vec3 t)
        {
            return new F32Vec2(t.X, t.Y);
        }

        public static F32Vec3 ToF32Vec3(this F32Vec2 t)
        {
            return new F32Vec3(t.X, t.Y, F32.Zero);
        }


        public static bool IsRectOverlap(F32Vec2 rectAPos, F32Vec2 rectA, F32Vec2 rectBPos, F32Vec2 rectB)
        {
            return rectAPos.X < rectBPos.X + rectB.X &&
                   rectAPos.X + rectA.X > rectBPos.X &&
                   rectAPos.Y < rectBPos.Y + rectB.Y &&
                   rectAPos.Y + rectA.Y > rectBPos.Y;
        }

        public static bool IsPointInsideBox(F32Vec3 point, F32Vec3 boxCenter, F32Vec3 boxSize)
        {
            return point.X >= boxCenter.X - boxSize.X / 2 && point.X <= boxCenter.X + boxSize.X / 2 &&
                   point.Y >= boxCenter.Y - boxSize.Y / 2 && point.Y <= boxCenter.Y + boxSize.Y / 2 &&
                   point.Z >= boxCenter.Z - boxSize.Z / 2 && point.Z <= boxCenter.Z + boxSize.Z / 2;
        }
    }
}