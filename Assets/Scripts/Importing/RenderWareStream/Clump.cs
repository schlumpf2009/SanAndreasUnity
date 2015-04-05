﻿using System;
using System.IO;

namespace SanAndreasUnity.Importing.RenderWareStream
{
    [SectionType(TypeId)]
    public class Clump : SectionData
    {
        public const Int32 TypeId = 16;

        public readonly UInt32 AtomicCount;

        public readonly FrameList FrameList;
        public readonly GeometryList GeometryList;
        public readonly Atomic[] Atomics;

        public Clump(SectionHeader header, Stream stream)
        {
            var data = Section<Data>.ReadData(stream); // Struct
            if (data == null) return;

            var reader = new BinaryReader(new MemoryStream(data.Value));

            AtomicCount = reader.ReadUInt32();
            Atomics = new Atomic[AtomicCount];

            FrameList = Section<FrameList>.ReadData(stream); // Frame List
            GeometryList = Section<GeometryList>.ReadData(stream); // Geometry List

            for (int i = 0; i < AtomicCount; ++i)
            {
                Atomics[i] = Section<Atomic>.ReadData(stream); // Atomic
            }

            Section<SectionData>.ReadData(stream); // Extension
        }
    }
}