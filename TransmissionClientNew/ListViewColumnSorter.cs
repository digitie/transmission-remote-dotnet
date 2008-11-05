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
                        ObjectCompare = new ListViewTorrentNumberComparer("totalSize");
                        break;
                    case 3:
                        ObjectCompare = new ListViewTorrentProgressComparer();
                        break;
                    case 4:
                        ObjectCompare = new ListViewTorrentNumberComparer("rateDownload");
                        break;
                    case 5:
                        ObjectCompare = new ListViewTorrentNumberComparer("rateUpload");
                        break;
                    case 6:
                        ObjectCompare = new ListViewTorrentNumberComparer("eta");
                        break;
                    case 7:
                        ObjectCompare = new ListViewTorrentNumberComparer("haveValid");
                        break;
                    case 8:
                        ObjectCompare = new ListViewTorrentNumberComparer("uploadedEver");
                        break;
                    case 9:
                        ObjectCompare = new ListViewTorrentRatioComparer();
                        break;
                    case 10:
                        ObjectCompare = new ListViewTorrentNumberComparer("seeders");
                        break;
                    case 11:
                        ObjectCompare = new ListViewTorrentNumberComparer("leechers");
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
