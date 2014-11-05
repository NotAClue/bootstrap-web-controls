using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotAClue.Web.UI.BootstrapWebControls
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1035:ICollectionImplementationsHaveStronglyTypedMembers", Justification = "Unnecessary for this specialized class")]
    public class FooterButtonCollection : CollectionBase
    {
            public FooterButtonCollection()
            {
            }

            /// <summary>
            /// Indexer property for the collection that returns and sets an item
            /// </summary>
            public FooterButton this[int index]
            {
                get
                {
                    return (FooterButton)this.List[index];
                }
                set
                {
                    this.List[index] = value;
                }
            }

            /// <summary>
            /// Adds a new error to the collection
            /// </summary>
            public void Add(FooterButton footerButton)
            {
                this.List.Add(footerButton);
            }

            public void Insert(int index, FooterButton item)
            {
                this.List.Insert(index, item);
            }

            public void Remove(FooterButton footerButton)
            {
                List.Remove(footerButton);
            }

            public bool Contains(FooterButton footerButton)
            {
                return this.List.Contains(footerButton);
            }

            //Collection IndexOf method 
            public int IndexOf(FooterButton item)
            {
                return List.IndexOf(item);
            }

            public void CopyTo(FooterButton[] array, int index)
            {
                List.CopyTo(array, index);
            }
    }
}
