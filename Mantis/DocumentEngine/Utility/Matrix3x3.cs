// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

namespace Mantis.DocumentEngine
{
    // A standard 3x3 transformation matrix.
    public struct Matrix3x3 : IEquatable<Matrix3x3>
    {
        // memory layout:
        //
        //                row no (=vertical)
        //               |  0   1   2   3
        //            ---+----------------
        //            0  | m00 m10 m20 m30
        // column no  1  | m01 m11 m21 m31
        // (=horiz)   2  | m02 m12 m22 m32
        //            3  | m03 m13 m23 m33

        public double m00;
        public double m01;
        public double m02;
        public double m10;
        public double m11;
        public double m12;
        public double m20;
        public double m21;
        public double m22;

        // public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        // {
        //     this.m00 = column0.x; this.m01 = column1.x; this.m02 = column2.x; this.m03 = column3.x;
        //     this.m10 = column0.y; this.m11 = column1.y; this.m12 = column2.y; this.m13 = column3.y;
        //     this.m20 = column0.z; this.m21 = column1.z; this.m22 = column2.z; this.m23 = column3.z;
        //     this.m30 = column0.w; this.m31 = column1.w; this.m32 = column2.w; this.m33 = column3.w;
        // }

        // Access element at [row, column].
        public double this[int row, int column]
        {
            get
            {
                return this[row + column * 3];
            }
            
            set
            {
                this[row + column * 3] = value;
            }
        }

        // Access element at sequential index (0..15 inclusive).
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return m00;
                    case 1: return m10;
                    case 2: return m20;
                    case 3: return m01;
                    case 4: return m11;
                    case 5: return m21;
                    case 6: return m02;
                    case 7: return m12;
                    case 8: return m22;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: m00 = value; break;
                    case 1: m10 = value; break;
                    case 2: m20 = value; break;
                    case 3: m01 = value; break;
                    case 4: m11 = value; break;
                    case 5: m21 = value; break;
                    case 6: m02 = value; break;
                    case 7: m12 = value; break;
                    case 8: m22 = value; break;

                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
        }
        

        // Multiplies two matrices.
        public static Matrix3x3 operator*(Matrix3x3 lhs, Matrix3x3 rhs)
        {
            Matrix3x3 res;
            res.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20;
            res.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21;
            res.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 ;

            res.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 ;
            res.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 ;
            res.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 ;


            res.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 ;
            res.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 ;
            res.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 ;

            return res;
        }
        

        //*undoc*
        public static bool operator==(Matrix3x3 lhs, Matrix3x3 rhs)
        {
            return lhs.Equals(rhs);
        }

        //*undoc*
        public static bool operator!=(Matrix3x3 lhs, Matrix3x3 rhs)
        {
            // Returns true in the presence of NaN values.
            return !(lhs == rhs);
        }
        
        public Vector2 GetPosition()
        {
            return new Vector2(m02, m12);
        }
        

        // Transforms a position by this matrix, with a perspective divide. (generic)
        public Vector2 MultiplyPoint(Vector2 point)
        {
            Vector2 res;
            double w;
            res.x = this.m00 * point.x + this.m01 * point.y + this.m02;
            res.y = this.m10 * point.x + this.m11 * point.y + this.m12;
            w = this.m20 * point.x + this.m21 * point.y  + this.m22;

            w = 1F / w;
            res.x *= w;
            res.y *= w;
            return res;
        }
        

        // Creates a scaling matrix.
        public static Matrix3x3 Scale(Vector2 vector)
        {
            Matrix3x3 m;
            m.m00 = vector.x; m.m01 = 0; m.m02 = 0;
            m.m10 = 0; m.m11 = vector.y; m.m12 = 0;
            m.m20 = 0;
            m.m21 = 0; m.m22 = 1;
            return m;
        }

        // Creates a translation matrix.
        public static  Matrix3x3 Translate(Vector2 vector)
        {
            Matrix3x3 m;
            m.m00 = 1; m.m01 = 0; m.m02 = vector.x;
            m.m10 = 0; m.m11 = 1; m.m12 = vector.y; 
            m.m20 = 0; m.m21 = 0; m.m22 = 1; 
            return m;
        }

        // // Creates a rotation matrix. Note: Assumes unit quaternion
        // public static Matrix4x4 Rotate(Quaternion q)
        // {
        //     // Precalculate coordinate products
        //     float x = q.x * 2.0;
        //     float y = q.y * 2.0;
        //     float z = q.z * 2.0;
        //     float xx = q.x * x;
        //     float yy = q.y * y;
        //     float zz = q.z * z;
        //     float xy = q.x * y;
        //     float xz = q.x * z;
        //     float yz = q.y * z;
        //     float wx = q.w * x;
        //     float wy = q.w * y;
        //     float wz = q.w * z;
        //
        //     // Calculate 3x3 matrix from orthonormal basis
        //     Matrix4x4 m;
        //     m.m00 = 1.0f - (yy + zz); m.m10 = xy + wz; m.m20 = xz - wy; m.m30 = 0.0;
        //     m.m01 = xy - wz; m.m11 = 1.0f - (xx + zz); m.m21 = yz + wx; m.m31 = 0.0;
        //     m.m02 = xz + wy; m.m12 = yz - wx; m.m22 = 1.0f - (xx + yy); m.m32 = 0.0;
        //     m.m03 = 0.0; m.m13 = 0.0; m.m23 = 0.0; m.m33 = 1.0;
        //     return m;
        // }


        private static readonly Matrix3x3 identityMatrix = new Matrix3x3() {m11 = 1, m22 = 1, m00 = 1};
        // Returns the identity matrix (RO).
        public static Matrix3x3 identity    {  get { return identityMatrix; } }

        public bool Equals(Matrix3x3 other)
        {
            return m00.Equals(other.m00) && m01.Equals(other.m01) && m02.Equals(other.m02) && m10.Equals(other.m10) && m11.Equals(other.m11) && m12.Equals(other.m12) && m20.Equals(other.m20) && m21.Equals(other.m21) && m22.Equals(other.m22);
        }

        public override bool Equals(object? obj)
        {
            return obj is Matrix3x3 other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(m00);
            hashCode.Add(m01);
            hashCode.Add(m02);
            hashCode.Add(m10);
            hashCode.Add(m11);
            hashCode.Add(m12);
            hashCode.Add(m20);
            hashCode.Add(m21);
            hashCode.Add(m22);
            return hashCode.ToHashCode();
        }
    }
} //namespace