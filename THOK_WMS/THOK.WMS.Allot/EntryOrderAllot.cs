using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using THOK.WMS.Allot.Bll;

public class EntryOrderAllot
{
    string  productCode = "";
    string  productName = "";
    string  productPieceUnitCode = "";
    string  productPieceUnitName = "";
    decimal productPieceUnitStandardRate = 0.00M;
    string  productBarUnitCode = "";
    string  productBarUnitName = "";
    decimal productBarUnitStandardRate = 0.00M;
    decimal maxQuantity = 0.00M;
    string  shelfCode = "";

    decimal entryOrderQuantity = 0.00M;
    string  entryOrderUnitCode = "";
    string  entryOrderUnitName = "";
    decimal entryOrderUnitStandardRate = 0.00M;

    decimal entryOrderPieceQuantity = 0.00M;
    decimal entryOrderBarQuantity = 0.00M;

    //准备分配订单！
    DataSet dsDetail = null;

    //是否使用零件区（件烟区）
    bool isUseSmallPieceArea = false;
    bool isUseSmallBarArea = false;
    bool isUseDesignateCell = false;

    string smallPieceAreaAreaType = "";
    string smallBarAreaAreaType = "";
    string targetPieceAreaCode = "";
    IList<string> pieceAreaTypes = new List<string>(); 

    //可分配的储位
    DataTable tableAvailableCell;

    //分配结果
    public  DataTable tableAllotment;
    //保存错误信息显列表！
    public IList<string> errorlist = new List<string>();

    public EntryOrderAllot(DataSet dsDetail, params string[] targetPieceAreaCode)
    {
        this.dsDetail = dsDetail;

        //｜｜=======================================｜｜
        //｜｜主库区	1号库区		0 ！（主库区） 1 ｜｜
        //｜｜件烟区                6 * （件烟区） 2 ｜｜  
        //｜｜零烟区	零烟区		1 ！（条烟区） 3 ｜｜
        //｜｜暂存区	暂存区		3 ！（暂存区） 4 ｜｜
        //｜｜备货区	备货区		2   （备货区） 5 ｜｜        
        //｜｜---------------------------------------｜｜
        //｜｜损烟区	损烟区		4   （残烟区） 6 ｜｜
        //｜｜损益区                  * （损益区） 7 ｜｜
        //｜｜---------------------------------------｜｜     
        //｜｜罚烟区                5   （罚烟区） 8 ｜｜
        //｜｜其他区                8   （其他区） 9 ｜｜
        //｜｜=======================================｜｜ 

        if (targetPieceAreaCode.Length > 0)
        {
            this.targetPieceAreaCode = targetPieceAreaCode[0];
        }
        //todo 修改主库区类型编码及分配顺序。
        this.pieceAreaTypes.Add("0");
        this.pieceAreaTypes.Add("3");

        //todo 修改条烟区类型编码。
        this.smallBarAreaAreaType = "100";
        //todo 修改件烟区类型编码。
        this.smallPieceAreaAreaType = "6";
        //todo 修改是否使用已被指定其他卷烟的空储位
        isUseDesignateCell = false;
    }

    public DataTable Allot()
    {
        try
        {
            AllotBll allotBll = new AllotBll();

            tableAvailableCell = allotBll.FindAvailableCell();
            tableAllotment = tableAvailableCell.Clone();

            for (int i = 0; i < dsDetail.Tables[0].Rows.Count; i++)
            {
                DataRow rowDetail = dsDetail.Tables[0].Rows[i];
                Allot(rowDetail);
            }
        }
        catch (Exception e)
        {
            this.errorlist.Add("--入库分配出错！详情：" + e.Message);
        }

        return tableAllotment;
    }

    private void Allot(DataRow rowDetail)
    {
        AllotBll allotBll = new AllotBll();

        productCode = rowDetail["PRODUCTCODE"].ToString();
        productName = rowDetail["PRODUCTNAME"].ToString();
        productPieceUnitCode = allotBll.FindProductPieceUnitCode(productCode);
        productPieceUnitName = allotBll.FindUnitName(productPieceUnitCode);
        productPieceUnitStandardRate = allotBll.FindUnitStandardRate(productPieceUnitCode);

        productBarUnitCode = allotBll.FindProductBarUnitCode(productCode);
        productBarUnitName = allotBll.FindUnitName(productBarUnitCode);
        productBarUnitStandardRate = allotBll.FindUnitStandardRate(productBarUnitCode);

        maxQuantity = allotBll.FindProductMaxQuantity(productCode);
        shelfCode = allotBll.FindProductAssignedCell(productCode);

        isUseSmallPieceArea = allotBll.FindIsUseArea(this.smallPieceAreaAreaType);
        isUseSmallBarArea = allotBll.FindIsUseArea(this.smallBarAreaAreaType);

        entryOrderQuantity = Convert.ToDecimal(rowDetail["INPUTQUANTITY"]);
        entryOrderUnitCode = rowDetail["UNITCODE"].ToString();
        entryOrderUnitName = allotBll.FindUnitName(entryOrderUnitCode);
        entryOrderUnitStandardRate = allotBll.FindUnitStandardRate(entryOrderUnitCode);      
        
        entryOrderPieceQuantity = Math.Floor((entryOrderQuantity * entryOrderUnitStandardRate) / productPieceUnitStandardRate);
        entryOrderBarQuantity = ((entryOrderQuantity * entryOrderUnitStandardRate) % productPieceUnitStandardRate) / productBarUnitStandardRate;        

        Allot(rowDetail, entryOrderPieceQuantity, entryOrderBarQuantity);
    }

    private void Allot(DataRow rowDetail, decimal orderPieceQuantity, decimal orderBarQuantity)
    {
        string orderby = "";
        decimal tmpOrderPieceQuantity = 0.00M;

        if (this.targetPieceAreaCode != "")
        {
            #region 指定库区进行分配
            
            tmpOrderPieceQuantity = orderPieceQuantity + (orderBarQuantity * productBarUnitStandardRate) / productPieceUnitStandardRate;
            if (tmpOrderPieceQuantity > 0)
            {
                tmpOrderPieceQuantity = AllotForPiece(rowDetail, tmpOrderPieceQuantity,this.targetPieceAreaCode);
            }

            #endregion
        }
        else
        {

            #region 条烟区分配

            if (isUseSmallBarArea)
            {
                orderBarQuantity = AllotForBar(rowDetail, orderBarQuantity);
            }

            #endregion

            #region 主库区分配

            orderby = "AREATYPE,AREACODE,SHELFCODE,CELLCODE";
            foreach (string areaType in this.pieceAreaTypes)
            {
                if (orderPieceQuantity > 0)
                {
                    orderPieceQuantity = AllotForPiece(areaType, rowDetail, orderPieceQuantity,orderby, true);
                }
            }

            #endregion

            #region 件烟区分配

            if (isUseSmallPieceArea)
            {
                tmpOrderPieceQuantity = orderPieceQuantity + (orderBarQuantity * productBarUnitStandardRate) / productPieceUnitStandardRate;
                if (tmpOrderPieceQuantity > 0)
                {
                    tmpOrderPieceQuantity = AllotForPiece(rowDetail, tmpOrderPieceQuantity);
                }
            }

            #endregion

            #region 主库区分配

            if (!isUseSmallPieceArea)
            {
                tmpOrderPieceQuantity = orderPieceQuantity + (orderBarQuantity * productBarUnitStandardRate) / productPieceUnitStandardRate;
                orderby = "LAYER_NO,AREATYPE,AREACODE,SHELFCODE,CELLCODE";
                foreach (string areaType in this.pieceAreaTypes)
                {
                    if (tmpOrderPieceQuantity > 0)
                    {
                        tmpOrderPieceQuantity = AllotForPiece(areaType, rowDetail, tmpOrderPieceQuantity, orderby, false);
                    }
                }
            }

            #endregion
        }

        #region 错误信息处理

        if (tmpOrderPieceQuantity > 0)
        {
            this.errorlist.Add("--件烟区储位已满，请确认！");
        }
        else
        {
            orderPieceQuantity = 0;
            orderBarQuantity = 0;
        }

        if (orderPieceQuantity > 0)
        {
            this.errorlist.Add("--主库区储位已满，请确认！");
        }

        if (orderBarQuantity > 0)
        {
            this.errorlist.Add("--条烟区储位已满，请确认！");
        }

        #endregion
    }    

    private decimal AllotForBar(DataRow rowDetail, decimal orderBarQuantity)
    {
        string areaType = this.smallBarAreaAreaType;
        string filter = "";
        string orderby = "AREATYPE,AREACODE,SHELFCODE,CELLCODE";
        DataRow[] rowsAvailableCell = null;

        #region 条烟区分配       
        
        if (orderBarQuantity > 0)
        {
            //分配条烟柜，有已经分配过当前卷烟的储位，将卷烟分给该储位。（已分配非空位）
            filter = "AREATYPE = '{0}' AND CURRENTPRODUCT='{1}' AND AVAILABLE > 0";
            filter = string.Format(filter, areaType, productCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                orderBarQuantity = AllotForBar(rowsAvailableCell, orderBarQuantity, rowDetail);
            }
        }

        if (orderBarQuantity > 0)
        {
            //分配条烟柜，有已指定当前卷烟的储位,但储位为空，将卷烟分给该储位。（已指定的空位）
            filter = "AREATYPE = '{0}' AND ASSIGNEDPRODUCT='{1}' AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, areaType, productCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                orderBarQuantity = AllotForBar(rowsAvailableCell, orderBarQuantity, rowDetail);
            }
        }

        if (orderBarQuantity > 0)
        {
            //分配条烟柜，有货位未指定卷烟且储位为空,将卷烟分给该储位。（没指定的空位）
            filter = "AREATYPE = '{0}' AND (ASSIGNEDPRODUCT='' OR ASSIGNEDPRODUCT IS NULL) AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, areaType);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                orderBarQuantity = AllotForBar(rowsAvailableCell, orderBarQuantity, rowDetail);
            }
        }

        if (orderBarQuantity > 0 && isUseDesignateCell)
        {
            //分配条烟柜，如果有储位为空，将卷烟分给该货位。（空位）
            filter = "AREATYPE = '{0}' AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, areaType);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                orderBarQuantity = AllotForBar(rowsAvailableCell, orderBarQuantity, rowDetail);
            }
        }

        #endregion

        return orderBarQuantity;
    }

    private decimal AllotForBar(DataRow[] rowsAvailableBarCell, decimal orderBarQuantity, DataRow rowDetail)
    {
        decimal availableQuantity = 0.00M;
        for (int i = 0; i < rowsAvailableBarCell.Length; i++)
        {
            if (orderBarQuantity == 0)
            {
                break;
            }

            if (rowsAvailableBarCell[i]["CELLUNITCODE"].ToString().Trim() != productBarUnitCode && rowsAvailableBarCell[i]["CURRENTPRODUCT"].ToString() == productCode)
            {
                continue;
            }

            DataRow newRow = tableAllotment.NewRow();
            newRow["BILLNO"] = rowDetail["BILLNO"].ToString();
            newRow["DETAILID"] = rowDetail["ID"].ToString();
            newRow["PRODUCTCODE"] = productCode;
            newRow["PRODUCTNAME"] = productName;
            newRow["UNITCODE"] = productBarUnitCode;
            newRow["UNITNAME"] = productBarUnitName;
            newRow["CELL_ID"] = rowsAvailableBarCell[i]["CELL_ID"];  //todo CELL_ID
            newRow["SHELFCODE"] = rowsAvailableBarCell[i]["SHELFCODE"];//todo SHELFCODE
            newRow["CELLCODE"] = rowsAvailableBarCell[i]["CELLCODE"];
            newRow["CELLNAME"] = rowsAvailableBarCell[i]["CELLNAME"];
            newRow["MAX_QUANTITY"] = rowsAvailableBarCell[i]["MAX_QUANTITY"];
            availableQuantity = Convert.ToDecimal(rowsAvailableBarCell[i]["AVAILABLE"]);

            if (availableQuantity > 0)
            {
                if (availableQuantity >= orderBarQuantity)
                {
                    newRow["ALLOTQUANTITY"] = orderBarQuantity;
                    rowsAvailableBarCell[i]["AVAILABLE"] = availableQuantity - orderBarQuantity;
                    rowsAvailableBarCell[i]["ALLOTQUANTITY"] = Convert.ToDecimal(rowsAvailableBarCell[i]["ALLOTQUANTITY"]) + orderBarQuantity;
                    rowsAvailableBarCell[i]["CURRENTPRODUCT"] = rowDetail["PRODUCTCODE"].ToString();
                    rowsAvailableBarCell[i]["PRODUCTCODE"] = rowDetail["PRODUCTCODE"].ToString();
                    rowsAvailableBarCell[i]["CELLUNITCODE"] = productBarUnitCode;
                    orderBarQuantity = 0.00M;
                }
                else
                {
                    newRow["ALLOTQUANTITY"] = availableQuantity;
                    rowsAvailableBarCell[i]["AVAILABLE"] = 0.00M;
                    rowsAvailableBarCell[i]["ALLOTQUANTITY"] = Convert.ToDecimal(rowsAvailableBarCell[i]["ALLOTQUANTITY"]) + availableQuantity;
                    rowsAvailableBarCell[i]["CURRENTPRODUCT"] = rowDetail["PRODUCTCODE"].ToString();
                    rowsAvailableBarCell[i]["PRODUCTCODE"] = rowDetail["PRODUCTCODE"].ToString();
                    rowsAvailableBarCell[i]["CELLUNITCODE"] = productBarUnitCode;
                    orderBarQuantity = orderBarQuantity - availableQuantity;
                }
                tableAllotment.Rows.Add(newRow);
            }
        }
        return orderBarQuantity;
    }

    private decimal AllotForPiece(string areaType, DataRow rowDetail, decimal orderPieceQuantity, string orderby,bool isUseSmallPieceArea)
    {
        string filter = "";
        DataRow[] rowsAvailableCell = null;
        decimal tmp = isUseSmallPieceArea ? this.maxQuantity : 0.00M;

        #region 主库区分配


        if (orderPieceQuantity >= tmp && orderPieceQuantity > 0.00M)
        {
            //分配主库区，有已指定当前卷烟的储位,但储位为空，将卷烟分给该储位。（已指定的空位）
            filter = "AREATYPE = '{0}' AND ASSIGNEDPRODUCT='{1}' AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, areaType, productCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                orderPieceQuantity = AllotForPiece(rowsAvailableCell, orderPieceQuantity, rowDetail, isUseSmallPieceArea);
            }
        }

        if (orderPieceQuantity >= tmp && orderPieceQuantity > 0.00M)
        {
            //指定卷烟的货位已放满，查找指定卷烟这排货架是否也放满，未满就分配这个货架的空货位。            
            //分配主库区，有货位未指定卷烟且储位为空,将卷烟分给该储位。（没指定的空位） 
            filter = "AREATYPE = '{0}' AND (ASSIGNEDPRODUCT='' OR ASSIGNEDPRODUCT IS NULL) AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY AND SHELFCODE IN({1}) ";
            filter = string.Format(filter, areaType,shelfCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                orderPieceQuantity = AllotForPiece(rowsAvailableCell, orderPieceQuantity, rowDetail, isUseSmallPieceArea);
            }
        }

        if (orderPieceQuantity >= tmp && orderPieceQuantity > 0.00M)
        {
            //分配主库区，有货位未指定卷烟且储位为空,将卷烟分给该储位。（没指定的空位）
            filter = "AREATYPE = '{0}' AND (ASSIGNEDPRODUCT='' OR ASSIGNEDPRODUCT IS NULL) AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, areaType);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                orderPieceQuantity = AllotForPiece(rowsAvailableCell, orderPieceQuantity, rowDetail, isUseSmallPieceArea);
            }
        }

        if (orderPieceQuantity >= tmp && orderPieceQuantity > 0.00M && isUseDesignateCell)
        {
            //分配主库区，如果有储位为空，将卷烟分给该货位。（空位）
            filter = "AREATYPE = '{0}' AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, areaType);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                orderPieceQuantity = AllotForPiece(rowsAvailableCell, orderPieceQuantity, rowDetail, isUseSmallPieceArea);
            }
        }

        #endregion

        return orderPieceQuantity;
    }

    private decimal AllotForPiece(DataRow rowDetail, decimal tmpOrderPieceQuantity)
    {
        string areaType = this.smallPieceAreaAreaType;
        string filter = "";
        string orderby = "AREATYPE,AREACODE,SHELFCODE,CELLCODE";
        DataRow[] rowsAvailableCell = null;

        #region 件烟区分配        
        
        if (tmpOrderPieceQuantity > 0)
        {
            //分配件烟区，有已经分配过当前卷烟的储位，将卷烟分给该储位。（已分配非空位）
            filter = "AREATYPE = '{0}' AND CURRENTPRODUCT='{1}' AND AVAILABLE > 0";
            filter = string.Format(filter, areaType, productCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                tmpOrderPieceQuantity = AllotForPiece(rowsAvailableCell, tmpOrderPieceQuantity, rowDetail,false);
            }
        }
        if (tmpOrderPieceQuantity > 0)
        {
            //分配件烟区，有已指定当前卷烟的储位,但储位为空，将卷烟分给该储位。（已指定的空位）
            filter = "AREATYPE = '{0}' AND ASSIGNEDPRODUCT='{1}' AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, areaType, productCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                tmpOrderPieceQuantity = AllotForPiece(rowsAvailableCell, tmpOrderPieceQuantity, rowDetail,false);
            }
        }
        if (tmpOrderPieceQuantity > 0)
        {
            //分配件烟区，有货位未指定卷烟且储位为空,将卷烟分给该储位。（没指定的空位）
            filter = "AREATYPE = '{0}' AND (ASSIGNEDPRODUCT='' OR ASSIGNEDPRODUCT IS NULL) AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, areaType);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                tmpOrderPieceQuantity = AllotForPiece(rowsAvailableCell, tmpOrderPieceQuantity, rowDetail,false);
            }
        }
        if (tmpOrderPieceQuantity > 0 && isUseDesignateCell)
        {
            //分配件烟区,如果有储位为空，将卷烟分给该货位。（空位）            
            filter = "AREATYPE = '{0}' AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, areaType);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                tmpOrderPieceQuantity = AllotForPiece(rowsAvailableCell, tmpOrderPieceQuantity, rowDetail, false);
            }
        }

        #endregion

        return tmpOrderPieceQuantity;
    }

    private decimal AllotForPiece(DataRow rowDetail, decimal tmpOrderPieceQuantity, string targetPieceAreaCode)
    {
        string filter = "";
        string orderby = "AREATYPE,AREACODE,SHELFCODE,CELLCODE";
        DataRow[] rowsAvailableCell = null;

        #region 件烟区分配

        //if (tmpOrderPieceQuantity > 0)
        //{
        //    //分配件烟区，有已经分配过当前卷烟的储位，将卷烟分给该储位。（已分配非空位）
        //    filter = "AREACODE = '{0}' AND CURRENTPRODUCT='{1}' AND AVAILABLE > 0";
        //    filter = string.Format(filter, targetPieceAreaCode, productCode);
        //    rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
        //    if (rowsAvailableCell.Length > 0)
        //    {
        //        tmpOrderPieceQuantity = AllotForPiece(rowsAvailableCell, tmpOrderPieceQuantity, rowDetail, false);
        //    }
        //}
        if (tmpOrderPieceQuantity > 0)
        {
            //分配件烟区，有已指定当前卷烟的储位,但储位为空，将卷烟分给该储位。（已指定的空位）
            filter = "AREACODE = '{0}' AND ASSIGNEDPRODUCT='{1}' AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, targetPieceAreaCode, productCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                tmpOrderPieceQuantity = AllotForPiece(rowsAvailableCell, tmpOrderPieceQuantity, rowDetail, false);
            }
        }
        if (tmpOrderPieceQuantity > 0)
        {
            //分配件烟区，有货位未指定卷烟且储位为空,将卷烟分给该储位。（没指定的空位）
            filter = "AREACODE = '{0}' AND (ASSIGNEDPRODUCT='' OR ASSIGNEDPRODUCT IS NULL) AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, targetPieceAreaCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                tmpOrderPieceQuantity = AllotForPiece(rowsAvailableCell, tmpOrderPieceQuantity, rowDetail, false);
            }
        }
        if (tmpOrderPieceQuantity > 0 && isUseDesignateCell)
        {
            //分配件烟区,如果有储位为空，将卷烟分给该货位。（空位）            
            filter = "AREACODE = '{0}' AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=MAX_QUANTITY";
            filter = string.Format(filter, targetPieceAreaCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                tmpOrderPieceQuantity = AllotForPiece(rowsAvailableCell, tmpOrderPieceQuantity, rowDetail, false);
            }
        }

        #endregion

        return tmpOrderPieceQuantity;
    }

    private decimal AllotForPiece(DataRow[] rowsAvailablePieceCell, decimal quantity, DataRow rowDetail, bool isUseSmallPieceArea)
    {
        decimal availableQuantity = 0.00M;
        decimal tmp = isUseSmallPieceArea ? this.maxQuantity : 0.00M;

        for (int i = 0; i < rowsAvailablePieceCell.Length; i++)
        {
            if (quantity < tmp || quantity == 0.00M)
            {
                break;
            }

            if (rowsAvailablePieceCell[i]["CELLUNITCODE"].ToString().Trim() != productPieceUnitCode && rowsAvailablePieceCell[i]["CURRENTPRODUCT"].ToString() == productCode)
            {
                continue;
            }

            DataRow newRow = tableAllotment.NewRow();
            newRow["BILLNO"] = rowDetail["BILLNO"].ToString();
            newRow["DETAILID"] = rowDetail["ID"].ToString();
            newRow["PRODUCTCODE"] = productCode;
            newRow["PRODUCTNAME"] = productName;
            newRow["UNITCODE"] = productPieceUnitCode;
            newRow["UNITNAME"] = productPieceUnitName;
            newRow["CELL_ID"] = rowsAvailablePieceCell[i]["CELL_ID"];
            newRow["SHELFCODE"] = rowsAvailablePieceCell[i]["SHELFCODE"];
            newRow["CELLCODE"] = rowsAvailablePieceCell[i]["CELLCODE"];
            newRow["CELLNAME"] = rowsAvailablePieceCell[i]["CELLNAME"];

            if (maxQuantity == 0.00M)
            {
                newRow["MAX_QUANTITY"] = rowsAvailablePieceCell[i]["MAX_QUANTITY"];
            }
            else
            {
                newRow["MAX_QUANTITY"] = maxQuantity;
            }
            if (decimal.Parse(rowsAvailablePieceCell[i]["AREATYPE"].ToString()) ==3)//暂存区可以分配更多-张青龙-！
            {
                availableQuantity = Convert.ToDecimal(rowsAvailablePieceCell[i]["AVAILABLE"]);
            }
            else//更改，储位可存储数量
            {
                if (decimal.Parse(rowsAvailablePieceCell[i]["AVAILABLE"].ToString()) >= decimal.Parse(rowsAvailablePieceCell[i]["MAX_QUANTITY"].ToString()))
                {
                    availableQuantity = Convert.ToDecimal(newRow["MAX_QUANTITY"].ToString());
                }
                else
                {
                    availableQuantity = Convert.ToDecimal(rowsAvailablePieceCell[i]["AVAILABLE"]);
                }
            }

            if (availableQuantity > 0)
            {
                if (availableQuantity >= quantity)
                {
                    newRow["ALLOTQUANTITY"] = quantity;
                    rowsAvailablePieceCell[i]["AVAILABLE"] = availableQuantity - quantity;
                    rowsAvailablePieceCell[i]["ALLOTQUANTITY"] = Convert.ToDecimal(rowsAvailablePieceCell[i]["ALLOTQUANTITY"]) + quantity;
                    rowsAvailablePieceCell[i]["CURRENTPRODUCT"] = rowDetail["PRODUCTCODE"].ToString();
                    rowsAvailablePieceCell[i]["PRODUCTCODE"] = rowDetail["PRODUCTCODE"].ToString();
                    rowsAvailablePieceCell[i]["CELLUNITCODE"] = productPieceUnitCode;
                    quantity = 0.00M;
                }
                else
                {
                    newRow["ALLOTQUANTITY"] = availableQuantity;
                    rowsAvailablePieceCell[i]["AVAILABLE"] = 0.00M;
                    rowsAvailablePieceCell[i]["ALLOTQUANTITY"] = Convert.ToDecimal(rowsAvailablePieceCell[i]["ALLOTQUANTITY"]) + availableQuantity;
                    rowsAvailablePieceCell[i]["CURRENTPRODUCT"] = rowDetail["PRODUCTCODE"].ToString();
                    rowsAvailablePieceCell[i]["PRODUCTCODE"] = rowDetail["PRODUCTCODE"].ToString();
                    rowsAvailablePieceCell[i]["CELLUNITCODE"] = productPieceUnitCode;
                    quantity = quantity - availableQuantity;
                }
                tableAllotment.Rows.Add(newRow);
            }
        }
        return quantity;
    }
}
