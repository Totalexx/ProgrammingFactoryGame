using System;
using UnityEngine;

namespace Util
{
    public static class Vector3Util
    {
        private const float EPS = 0.001f;

        public static bool EqualsEsp(this Vector3 vectorThis, Vector3 vector)
        {
            return Math.Abs(vectorThis.x - vector.x) < EPS
                   && Math.Abs(vectorThis.y - vector.y) < EPS
                   && Math.Abs(vectorThis.z - vector.z) < EPS;
        }
    }
}