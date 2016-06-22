// <copyright file="ReentrantBlocker.cs" company="tmori3y2.hatenablog.com">
// Copyright (c) 2016 tmori3y2.hatenablog.com. All rights reserved.
// </copyright>
// <author>tmori3y2</author>
// <date>2016/06/22</date>
// <summary>Implements the reentrant blocker class</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MySampleExtensions
{
    /// <summary>A reentrant blocker.</summary>
    /// <remarks>tmori3y2, 2016/06/22.</remarks>
    public class ReentrantBlocker : IDisposable
    {
        /// <summary>The counter.</summary>
        private int counter;

        /// <summary>
        /// initializes a new instance of the MySampleExtensions.ReentrantBlocker class.
        /// </summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        public ReentrantBlocker()
        {
            counter = 0;
        }

        /// <summary>Gets a value indicating whether the blocked.</summary>
        /// <value>true if blocked, false if not.</value>
        public bool Blocked
        {
            get
            {
                return (counter > 0);
            }
        }

        /// <summary>Gets the enter.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <returns>An IDisposable.</returns>
        public IDisposable Enter()
        {
            counter++;
            return this;
        }

        #region IDisposable members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
        /// resources.
        /// </summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        public void Dispose()
        {
            if (counter > 0)
            {
                counter--;
            }
        }

        #endregion
    }
}
