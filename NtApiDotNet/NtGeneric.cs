﻿//  Copyright 2019 Google Inc. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.


namespace NtApiDotNet
{
    /// <summary>
    /// A generic wrapper for any object, used if we don't know the type ahead of time.
    /// </summary>
    public class NtGeneric : NtObjectWithDuplicate<NtGeneric, GenericAccessRights>
    {
        internal NtGeneric(SafeKernelObjectHandle handle) : base(handle)
        {
        }

        internal sealed class NtTypeFactoryImpl : NtTypeFactoryImplBase
        {
            public NtTypeFactoryImpl() : base(false)
            {
            }
        }

        /// <summary>
        /// Convert the generic object to the best typed object.
        /// </summary>
        /// <returns>The typed object. Can be NtGeneric if no better type is known.</returns>
        public NtObject ToTypedObject()
        {
            return ToTypedObject(true).Result;
        }

        /// <summary>
        /// Convert the generic object to the best typed object.
        /// </summary>
        /// <param name="throw_on_error">True to throw on error.</param>
        /// <returns>The typed object. Can be NtGeneric if no better type is known.</returns>
        public NtResult<NtObject> ToTypedObject(bool throw_on_error)
        {
            return DuplicateHandle(Handle, throw_on_error).Map(h => NtType.FromHandle(h));
        }
    }
}
