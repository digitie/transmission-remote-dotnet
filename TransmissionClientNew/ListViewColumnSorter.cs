using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TransmissionClientNew.Comparers;

namespace TransmissionClientNew
{
    public class ListViewItemSorter : IComparer
    {
        private int ColumnToSort;
        private SortOrder OrderOfSort;
        private IComparer ObjectCompare;
        
        public ListViewItemSorter()
        {
            ColumnToSort = 0;
            OrderOfSort = SortOrder.None;
            ObjectCompare = new ListViewTextInsensitiveComparer(0);
        }

        public int Compare(object x, object y)
        {
            int compareResult;
            compareResult = ObjectCompare.Compare(x, y);
            if (OrderOfSort == SortOrder.Ascending)
            {
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                return (-compareResult);
            }
            else
            {
                return 0;
            }
        }

        /* Set the column and choose the best IComparer implementation */

        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
                switch (ColumnToSort)
                {
                    case 2:
                        ObjectCompare = new ListViewTorrentInt64Comparer("totalSize");
                        break;
                    case 3:
                        ObjectCompare = new ListViewTorrentProgressComparer();
                        break;
                    case 4:
                        ObjectCompare = new ListViewTorrentInt32Comparer("rateDownload");
                        break;
                    case 5:
                        ObjectCompare = new ListViewTorrentInt32Comparer("rateUpload");
                        break;
                    case 6:
                        ObjectCompare = new ListViewTorrentInt64Comparer("eta");
                        break;
                    case 7:
                        ObjectCompare = new ListViewTorrentInt64Comparer("haveValid");
                        break;
                    case 8:
                        ObjectCompare = new ListViewTorrentInt64Comparer("uploadedEver");
                        break;
                    case 9:
                        ObjectCompare = new ListViewTorrentRatioComparer();
                        break;
                    case 10:
                        ObjectCompare = new ListViewTorrentInt32Comparer("seeders");
                        break;
                    case 11:
                        ObjectCompare = new ListViewTorrentInt32Comparer("leechers");
                        break;
                    default:
                        ObjectCompare = new ListViewTextInsensitiveComparer(ColumnToSort);
                        break;
                }
            }
            get
            {
                return ColumnToSort;
            }
        }

        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }

    }
}
