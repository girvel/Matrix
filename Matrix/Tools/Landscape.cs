// using System;
//
// namespace Matrix.Tools
// {
//     public class AbstractLandscape<T>
//     {
//         private Field<T> _fields;
//         
//         public Landscape(int3 size, Func<int2, T> filler = null)
//         {
//             InternalArray = new T[size.X * size.Y];
//             Size = size;
//
//             if (filler == null) return;
//             
//             foreach (var (v, _) in this)
//             {
//                 this[v] = filler(v);
//             }
//         }
//     }
// }