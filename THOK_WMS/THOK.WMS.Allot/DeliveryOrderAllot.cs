using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using THOK.WMS.Allot.Bll;

public class DeliveryOrderAllot
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

    decimal deliveryOrderQuantity = 0.00M;
    string deliveryOrderUnitCode = "";
    string deliveryOrderUnitName = "";
    decimal deliveryOrderUnitStandardRate = 0.00M;

    decimal deliveryOrderPieceQuantity = 0.00M;
    decimal deliveryOrderBarQuantity = 0.00M;

    //准备分配订单！
    DataSet dsDetail = null;

    //是否使用零件区（件烟区）
    bool isUseSmallPieceArea = false;
    bool isUseSmallBarArea = false;
    bool isFirstToStockOut = false;

    string smallPieceAreaAreaType = "";
    string smallBarAreaAreaType = "";
    string tempPieceAreaType = "";
    string targetPieceAreaCode = "";
    IList<string> pieceAreaTypes = new List<string>();
    IList<string> pieceAreaTypesOfMovePieceAreaToBarArea = new List<string>();
    IList<string> pieceAreaTypesOfMovePieceAreaToSmallPieceArea = new List<string>();

    //可分配的储位
    DataTable tableAvailableCell;

    //分配结果
    public DataTable tableAllotment;
    public DataTable tableMoveDetail;
    //保存错误信息显列表！
    public IList<string> errorlist = new List<string>();

    public DeliveryOrderAllot(DataSet dsDetail, params string[] targetPieceAreaCode)
    {
        this.dsDetail = dsDetail;

        //｜｜=======================================｜｜
        //｜｜主库区	1号库区		0 ！（主库区） 1 ｜｜
        //｜｜件烟区                5 * （件烟区） 2 ｜｜  
        //｜｜零烟区	零烟区		1 ！（条烟区） 3 ｜｜
        //｜｜暂存区	暂存区		3 ！（暂存区） 4 ｜｜
        //｜｜备货区	备货区		2   （备货区） 5 ｜｜        
        //｜｜---------------------------------------｜｜
        //｜｜损烟区	损烟区		4   （残烟区） 6 ｜｜
        //｜｜损益区                6 * （损益区） 7 ｜｜
        //｜｜---------------------------------------｜｜     
        //｜｜罚烟区                    （罚烟区） 8 ｜｜
        //｜｜其他区                    （其他区） 9 ｜｜
        //｜｜=======================================｜｜ 

        //todo 修改条烟区类型编码。
        this.smallBarAreaAreaType = "1";
        //todo 修改件烟区类型编码。
        this.smallPieceAreaAreaType = "6";
        //todo 修改暂存区类型编码。
        this.tempPieceAreaType = "3";
        //todo 修改是否优先出库。
        this.isFirstToStockOut = true;

        //todo 修改主库区类型编码，及分配顺序。
        this.pieceAreaTypes.Add("3");//暂存区
        this.pieceAreaTypes.Add("0");//主库区
        //todo 修改主库区类型编码，及移库到条烟区的顺序。
        this.pieceAreaTypesOfMovePieceAreaToBarArea.Add("5");//件烟区
        this.pieceAreaTypesOfMovePieceAreaToBarArea.Add("3");//暂存区
        this.pieceAreaTypesOfMovePieceAreaToBarArea.Add("0");//主库区
        this.pieceAreaTypesOfMovePieceAreaToBarArea.Add("2");//备货区
        //todo 修改主库区类型编码，及移库到件烟区的顺序。
        this.pieceAreaTypesOfMovePieceAreaToSmallPieceArea.Add("3");//暂存区
        this.pieceAreaTypesOfMovePieceAreaToSmallPieceArea.Add("0");//主库区
        this.pieceAreaTypesOfMovePieceAreaToSmallPieceArea.Add("2");//备货区        

        if (targetPieceAreaCode.Length > 0)
        {
            this.targetPieceAreaCode = targetPieceAreaCode[0];
        }
    }

    public DataTable Allot()
    {
        try
        {
            AllotBll allotBll = new AllotBll();
            tableAvailableCell = allotBll.FindDeliveryAvailableCell();
            tableAllotment = allotBll.GetEmptyAllotmentTable("OUT");
            tableMoveDetail = allotBll.GetEmptyMoveDetailTable();

            for (int i = 0; i < dsDetail.Tables[0].Rows.Count; i++)
            {
                DataRow rowDetail = dsDetail.Tables[0].Rows[i];
                Allot(rowDetail);
            }
        }
        catch (Exception e)
        {
            this.errorlist.Add("--出库分配出错！详情：" + e.Message);
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

        deliveryOrderQuantity = Convert.ToDecimal(rowDetail["OUTPUTQUANTITY"]);
        deliveryOrderUnitCode = rowDetail["UNITCODE"].ToString();
        deliveryOrderUnitName = allotBll.FindUnitName(deliveryOrderUnitCode);
        deliveryOrderUnitStandardRate = allotBll.FindUnitStandardRate(deliveryOrderUnitCode);

        deliveryOrderPieceQuantity = Math.Floor((deliveryOrderQuantity * deliveryOrderUnitStandardRate) / productPieceUnitStandardRate);
        deliveryOrderBarQuantity = ((deliveryOrderQuantity * deliveryOrderUnitStandardRate) % productPieceUnitStandardRate) / productBarUnitStandardRate;

        Allot(rowDetail, deliveryOrderPieceQuantity, deliveryOrderBarQuantity);
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
            else if (!isUseSmallPieceArea)
            {
                orderPieceQuantity = orderPieceQuantity + (orderBarQuantity * productBarUnitStandardRate) / productPieceUnitStandardRate;
                orderBarQuantity = 0;
            }

            #endregion

            #region 主库区分配

            orderby = "INPUTDATE,LAYER_NO DESC,AREATYPE,AREACODE,SHELFCODE,CELLCODE";
            foreach (string areaType in this.pieceAreaTypes)
            {
                if (orderPieceQuantity > 0)
                {
                    orderPieceQuantity = AllotForPiece(areaType, rowDetail, orderPieceQuantity, orderby,true);
                }
            }

            #endregion

            tmpOrderPieceQuantity = orderPieceQuantity + (orderBarQuantity * productBarUnitStandardRate) / productPieceUnitStandardRate;

            #region 件烟区分配
            if (isUseSmallPieceArea)
            {                
                if (tmpOrderPieceQuantity > 0)
                {
                    tmpOrderPieceQuantity = AllotForPiece(rowDetail, tmpOrderPieceQuantity);
                }
            }

            #endregion

            #region 主库区分配

            if (!this.isUseSmallPieceArea || this.isFirstToStockOut)
            {
                orderby = "LAYER_NO,INPUTDATE,AREATYPE,AREACODE,SHELFCODE,CELLCODE";
                foreach (string areaType in this.pieceAreaTypesOfMovePieceAreaToSmallPieceArea)
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
            this.errorlist.Add(string.Format("--{0}({1}) 库存不足！", productName,productCode));
        }
        else
        {
            orderPieceQuantity = 0;
            orderBarQuantity = 0;
        }

        if (orderPieceQuantity > 0)
        {
            this.errorlist.Add(string.Format("--{0}({1}) 库存不足！", productName, productCode));
        }

        if (orderBarQuantity > 0)
        {
            this.errorlist.Add(string.Format("--条烟区储位已满未能移库，或 {0}({1}) 库存不足，请确认！",productName,productCode));
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
            filter = "AREATYPE = '{0}' AND CURRENTPRODUCT='{1}'";
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
            filter = "AREATYPE = '{0}' AND ASSIGNEDPRODUCT='{1}' AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE = 0 ";
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
            filter = "AREATYPE = '{0}' AND (ASSIGNEDPRODUCT='' OR ASSIGNEDPRODUCT IS NULL) AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE = 0";
            filter = string.Format(filter, areaType);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                orderBarQuantity = AllotForBar(rowsAvailableCell, orderBarQuantity, rowDetail);
            }
            else
            {
                this.errorlist.Add("--条烟区储位已满未能移库，请确认！");
            }
        }  

        #endregion

        return orderBarQuantity;
    }
    //
    private decimal AllotForBar(DataRow[] rowsAvailableBarCell, decimal orderBarQuantity, DataRow rowDetail)
    {
        decimal availableQuantity = 0.00M;
        for (int i = 0; i < rowsAvailableBarCell.Length; i++)
        {
            if (orderBarQuantity == 0)
            {
                break;
            }

            if (rowsAvailableBarCell[i]["UNITCODE"].ToString().Trim() != productBarUnitCode && rowsAvailableBarCell[i]["CURRENTPRODUCT"].ToString() == productCode)
            {
                continue;
            }

            DataRow newAllotRow = tableAllotment.NewRow();

            newAllotRow["BILLNO"] = rowDetail["BILLNO"].ToString();
            newAllotRow["DETAILID"] = rowDetail["ID"].ToString();
            newAllotRow["PALLETID"] = rowsAvailableBarCell[0]["PALLETID"].ToString();
            newAllotRow["PRODUCTCODE"] = productCode;
            newAllotRow["PRODUCTNAME"] = productName;
            newAllotRow["UNITCODE"] = productBarUnitCode;
            newAllotRow["UNITNAME"] = productBarUnitName;
            newAllotRow["CELLCODE"] = rowsAvailableBarCell[i]["CELLCODE"].ToString().Trim();
            newAllotRow["CELLNAME"] = rowsAvailableBarCell[i]["CELLNAME"].ToString().Trim();
            availableQuantity = Convert.ToDecimal(rowsAvailableBarCell[i]["AVAILABLE"]);

            if (availableQuantity >= orderBarQuantity || (i == rowsAvailableBarCell.Length - 1 || rowsAvailableBarCell[i]["CURRENTPRODUCT"].ToString() != productCode))
            {
                if (availableQuantity < orderBarQuantity)
                {
                    rowsAvailableBarCell[i]["CURRENTPRODUCT"] = productCode;
                    rowsAvailableBarCell[i]["UNITCODE"] = productBarUnitCode;

                    if (!MovePieceAreaToBarArea(rowDetail, rowsAvailableBarCell[i]))
                    {
                        this.errorlist.Add(string.Format("--{0}({1}) 库存不足！", productName,productCode));          
                    }
                }
                newAllotRow["QUANTITY"] = orderBarQuantity;

                rowsAvailableBarCell[i]["AVAILABLE"] = Convert.ToDecimal(rowsAvailableBarCell[i]["AVAILABLE"]) - orderBarQuantity;
                rowsAvailableBarCell[i]["ALLOTQUANTITY"] = Convert.ToDecimal(rowsAvailableBarCell[i]["ALLOTQUANTITY"]) + orderBarQuantity;

                orderBarQuantity = 0.00M;
                tableAllotment.Rows.Add(newAllotRow); 
            }
            else if (availableQuantity > 0)
            {
                newAllotRow["QUANTITY"] = availableQuantity;

                rowsAvailableBarCell[i]["AVAILABLE"] = 0.00M;
                rowsAvailableBarCell[i]["ALLOTQUANTITY"] = Convert.ToDecimal(rowsAvailableBarCell[i]["ALLOTQUANTITY"]) + availableQuantity;

                orderBarQuantity = orderBarQuantity - availableQuantity;
                tableAllotment.Rows.Add(newAllotRow); 
            }                    
        }

        return orderBarQuantity;
    }

    private bool MovePieceAreaToBarArea(DataRow rowDetail, DataRow targetAvailableCell)
    {
        bool isMove = false;
        foreach (string areaType in this.pieceAreaTypesOfMovePieceAreaToBarArea)
        {
            if (!isMove)
            {
                string filter = string.Format("AREATYPE={0} AND CURRENTPRODUCT='{1}'",areaType, productCode);
                string orderby = "LAYER_NO,INPUTDATE,AREATYPE,AREACODE,SHELFCODE,CELLCODE";
                DataRow[] sourceAvailableCell = tableAvailableCell.Select(filter,orderby);
                if (sourceAvailableCell.Length > 0)
                {
                    isMove = MovePieceAreaToBarArea(rowDetail, sourceAvailableCell, targetAvailableCell);
                }
            }
        }

        if (!isMove)
        {
            this.errorlist.Add(string.Format("--{0}({1}) 库存不足或条烟区已满,移库到条烟区失败！",productName,productCode));
            return false;
        }
        return true;
    }

    private bool MovePieceAreaToBarArea(DataRow rowDetail, DataRow[] sourceAvailableCelles, DataRow targetAvailableCell)
    {
        bool isMove = false;
        if (sourceAvailableCelles.Length > 0)
        {
            foreach (DataRow sourceAvailableCell in sourceAvailableCelles)
            {
                if (isMove)
                {
                    break;
                }

                if (sourceAvailableCell["UNITCODE"].ToString().Trim() != productPieceUnitCode)
                {
                    continue;
                }

                if (Convert.ToDecimal(sourceAvailableCell["AVAILABLE"]) >= 1.00M)
                {
                    DataRow NewRows = tableMoveDetail.NewRow();
                    NewRows["BILLNO"] = rowDetail["BILLNO"].ToString() +"M";
                    NewRows["PRODUCTCODE"] = productCode;
                    NewRows["PRODUCTNAME"] = productName;
                    NewRows["UNITCODE"] = productPieceUnitCode;
                    NewRows["UNITNAME"] = productPieceUnitName;
                    NewRows["OUT_CELLCODE"] = sourceAvailableCell["CELLCODE"].ToString().Trim();
                    NewRows["OUT_CELLNAME"] = sourceAvailableCell["CELLNAME"].ToString().Trim();
                    NewRows["IN_CELLCODE"] = targetAvailableCell["CELLCODE"].ToString().Trim();
                    NewRows["IN_CELLNAME"] = targetAvailableCell["CELLNAME"].ToString().Trim();
                    NewRows["QUANTITY"] = 1.00M;

                    sourceAvailableCell["AVAILABLE"] = Convert.ToDecimal(sourceAvailableCell["AVAILABLE"]) - 1;
                    targetAvailableCell["AVAILABLE"] = Convert.ToDecimal(targetAvailableCell["AVAILABLE"]) + productPieceUnitStandardRate / productBarUnitStandardRate;

                    tableMoveDetail.Rows.Add(NewRows);
                    if (this.isUseSmallPieceArea
                        && this.tempPieceAreaType != sourceAvailableCell["AREATYPE"].ToString().Trim()
                        && this.smallPieceAreaAreaType != sourceAvailableCell["AREATYPE"].ToString().Trim() 
                        && Convert.ToDecimal(sourceAvailableCell["AVAILABLE"]) > 0)
                    {
                        if (!MovePieceAreaToSmallPieceArea(rowDetail, sourceAvailableCell))//todo  同一储位多单操作
                        {
                            this.errorlist.Add(string.Format("--件烟区已满,{0}({1}) 移库到件烟区失败！", productName,productCode));
                            return false;
                        }
                    }
                    isMove = true;
                }
            }
        }
        return isMove;
    }

    private bool MovePieceAreaToSmallPieceArea(DataRow rowDetail, DataRow sourceAvailableCell)
    {
        string areaType = this.smallPieceAreaAreaType;
        string filter = "";
        string orderby = "AREATYPE,AREACODE,SHELFCODE,CELLCODE";
        DataRow[] rowsAvailableCelles = null;
        bool isMove = false;

        #region 件烟区分配

        if (!isMove)
        {
            //分配件烟区，有已经分配过当前卷烟的储位，将卷烟分给该储位。（已分配非空位）
            filter = "AREATYPE = '{0}' AND CURRENTPRODUCT='{1}'";
            filter = string.Format(filter, areaType, productCode);
            rowsAvailableCelles = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCelles.Length > 0)
            {
                isMove = MovePieceAreaToSmallPieceArea(rowDetail, sourceAvailableCell, rowsAvailableCelles);
            }
        }

        if (!isMove)
        {
            //分配件烟区，有已指定当前卷烟的储位,但储位为空，将卷烟分给该储位。（已指定的空位）
            filter = "AREATYPE = '{0}' AND ASSIGNEDPRODUCT='{1}' AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=0";
            filter = string.Format(filter, areaType, productCode);
            rowsAvailableCelles = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCelles.Length > 0)
            {
                isMove = MovePieceAreaToSmallPieceArea(rowDetail, sourceAvailableCell, rowsAvailableCelles);
            }
        }
        if (!isMove)
        {
            //分配件烟区，有货位未指定卷烟且储位为空,将卷烟分给该储位。（没指定的空位）
            filter = "AREATYPE = '{0}' AND (ASSIGNEDPRODUCT='' OR ASSIGNEDPRODUCT IS NULL) AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=0";
            filter = string.Format(filter, areaType);
            rowsAvailableCelles = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCelles.Length > 0)
            {
                isMove = MovePieceAreaToSmallPieceArea(rowDetail, sourceAvailableCell, rowsAvailableCelles);
            }
        }

        return isMove;
        #endregion
    }

    private bool MovePieceAreaToSmallPieceArea(DataRow rowDetail, DataRow sourceAvailableCell, DataRow[] targetAvailableCelles)
    {
        bool isMove = false;
        if (targetAvailableCelles.Length > 0)
        {
            foreach (DataRow targetAvailableCell in targetAvailableCelles)
            {
                if (isMove)
                {
                    break;
                }

                if (targetAvailableCell["UNITCODE"].ToString().Trim() != productPieceUnitCode && targetAvailableCell["CURRENTPRODUCT"].ToString().Trim() == productCode)
                {
                    continue;
                }

                if (Convert.ToDecimal(targetAvailableCell["AVAILABLE"]) + Convert.ToDecimal(sourceAvailableCell["AVAILABLE"]) <= this.maxQuantity)
                {
                    DataRow NewRows = tableMoveDetail.NewRow();
                    NewRows["BILLNO"] = rowDetail["BILLNO"].ToString() + "M";
                    NewRows["PRODUCTCODE"] = productCode;
                    NewRows["PRODUCTNAME"] = productName;
                    NewRows["UNITCODE"] = productPieceUnitCode;
                    NewRows["UNITNAME"] = productPieceUnitName;
                    NewRows["OUT_CELLCODE"] = sourceAvailableCell["CELLCODE"].ToString().Trim();
                    NewRows["OUT_CELLNAME"] = sourceAvailableCell["CELLNAME"].ToString().Trim();
                    NewRows["IN_CELLCODE"] = targetAvailableCell["CELLCODE"].ToString().Trim();
                    NewRows["IN_CELLNAME"] = targetAvailableCell["CELLNAME"].ToString().Trim();
                    NewRows["QUANTITY"] = Convert.ToDecimal(sourceAvailableCell["AVAILABLE"]);

                    targetAvailableCell["AVAILABLE"] = Convert.ToDecimal(targetAvailableCell["AVAILABLE"]) + Convert.ToDecimal(sourceAvailableCell["AVAILABLE"]); 
                    sourceAvailableCell["AVAILABLE"] = 0.00M;                    
                    
                    targetAvailableCell["CURRENTPRODUCT"] = productCode;
                    targetAvailableCell["UNITCODE"] = productPieceUnitCode;

                    tableMoveDetail.Rows.Add(NewRows);
                    isMove = true;
                }
            }
        }
        return isMove;
    }

    private decimal AllotForPiece(string areaType, DataRow rowDetail, decimal orderPieceQuantity, string orderby,bool isUseSmallPieceArea)
    {
        string filter = "";
        DataRow[] rowsAvailableCell = null;
        decimal tmp = isUseSmallPieceArea ? this.maxQuantity : 0.00M;

        #region 主库区分配

        if (orderPieceQuantity >= tmp && orderPieceQuantity > 0.00M)
        {
            //分配主库区，有已经分配过当前卷烟的储位，将卷烟分给该储位。（已分配非空位）
            filter = "AREATYPE = {0} AND CURRENTPRODUCT='{1}' AND AVAILABLE > 0";
            filter = string.Format(filter, areaType, productCode);
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
        string orderby = "INPUTDATE,AREATYPE,AREACODE,SHELFCODE,CELLCODE,LAYER_NO";
        DataRow[] rowsAvailableCell = null;

        #region 件烟区分配        
        
        if (tmpOrderPieceQuantity > 0)
        {
            //分配件烟区，有已经分配过当前卷烟的储位，将卷烟分给该储位。（已分配非空位）
            filter = "AREATYPE = '{0}' AND CURRENTPRODUCT='{1}'";
            filter = string.Format(filter, areaType, productCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                tmpOrderPieceQuantity = AllotForPiece(rowsAvailableCell, tmpOrderPieceQuantity, rowDetail,false);
            }
        }

        if (!this.isFirstToStockOut && tmpOrderPieceQuantity > 0)
        {
            //分配件烟区，有已指定当前卷烟的储位,但储位为空，将卷烟分给该储位。（已指定的空位）
            filter = "AREATYPE = '{0}' AND ASSIGNEDPRODUCT='{1}' AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=0";
            filter = string.Format(filter, areaType, productCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                tmpOrderPieceQuantity = AllotForPiece(rowsAvailableCell, tmpOrderPieceQuantity, rowDetail, false);
            }
        }
        if (!this.isFirstToStockOut && tmpOrderPieceQuantity > 0)
        {
            //分配件烟区，有货位未指定卷烟且储位为空,将卷烟分给该储位。（没指定的空位）
            filter = "AREATYPE = '{0}' AND (ASSIGNEDPRODUCT='' OR ASSIGNEDPRODUCT IS NULL) AND (CURRENTPRODUCT='' OR CURRENTPRODUCT IS NULL) AND AVAILABLE=0";
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
        string orderby = "INPUTDATE,AREATYPE,AREACODE,SHELFCODE,CELLCODE,LAYER_NO";
        DataRow[] rowsAvailableCell = null;

        #region 件烟区分配

        if (tmpOrderPieceQuantity > 0)
        {
            //分配件烟区，有已经分配过当前卷烟的储位，将卷烟分给该储位。（已分配非空位）
            filter = "AREACODE = '{0}' AND CURRENTPRODUCT='{1}' AND AVAILABLE > 0";
            filter = string.Format(filter, targetPieceAreaCode, productCode);
            rowsAvailableCell = tableAvailableCell.Select(filter, orderby);
            if (rowsAvailableCell.Length > 0)
            {
                tmpOrderPieceQuantity = AllotForPiece(rowsAvailableCell, tmpOrderPieceQuantity, rowDetail, false);
            }
        }

        #endregion

        return tmpOrderPieceQuantity;
    }
    //
    private decimal AllotForPiece(DataRow[] rowsAvailablePieceCell, decimal orderPieceQuantity, DataRow rowDetail, bool isUseSmallPieceArea)
    {
        decimal availableQuantity = 0.00M;
        decimal tmp = isUseSmallPieceArea ? this.maxQuantity : 0.00M;

        for (int i = 0; i < rowsAvailablePieceCell.Length; i++)
        {
            if (orderPieceQuantity < tmp || orderPieceQuantity == 0.00M)
            {
                break;
            }

            if (rowsAvailablePieceCell[i]["UNITCODE"].ToString().Trim() != productPieceUnitCode && rowsAvailablePieceCell[i]["CURRENTPRODUCT"].ToString() == productCode)
            {
                continue;
            }

            DataRow newRow = tableAllotment.NewRow();
            newRow["BILLNO"] = rowDetail["BILLNO"].ToString();
            newRow["DETAILID"] = rowDetail["ID"].ToString(); ;
            newRow["PALLETID"] = rowsAvailablePieceCell[0]["PALLETID"].ToString();
            newRow["PRODUCTCODE"] = productCode;
            newRow["PRODUCTNAME"] = productName;
            newRow["UNITCODE"] = productPieceUnitCode;
            newRow["UNITNAME"] = productPieceUnitName;
            newRow["CELLCODE"] = rowsAvailablePieceCell[i]["CELLCODE"].ToString().Trim();
            newRow["CELLNAME"] = rowsAvailablePieceCell[i]["CELLNAME"].ToString().Trim();

            availableQuantity = Convert.ToDecimal(rowsAvailablePieceCell[i]["AVAILABLE"]);

            if (availableQuantity >= orderPieceQuantity || (!this.isFirstToStockOut && this.isUseSmallPieceArea && (i == rowsAvailablePieceCell.Length - 1 || rowsAvailablePieceCell[i]["CURRENTPRODUCT"].ToString() != productCode) && (this.smallPieceAreaAreaType == rowsAvailablePieceCell[i]["AREATYPE"].ToString().Trim())))
            {
                rowsAvailablePieceCell[i]["CURRENTPRODUCT"] = productCode;
                rowsAvailablePieceCell[i]["UNITCODE"] = productPieceUnitCode;

                if (availableQuantity < orderPieceQuantity)
                {
                    if (!MovePieceAreaToSmallPieceArea(rowDetail, rowsAvailablePieceCell[i], orderPieceQuantity - availableQuantity))
                    {
                        this.errorlist.Add(string.Format("--{0}({1}) 库存不足！", productName,productCode));
                    }
                }
                newRow["QUANTITY"] = orderPieceQuantity;
                rowsAvailablePieceCell[i]["AVAILABLE"] = Convert.ToDecimal(rowsAvailablePieceCell[i]["AVAILABLE"]) - orderPieceQuantity;
                rowsAvailablePieceCell[i]["ALLOTQUANTITY"] = Convert.ToDecimal(rowsAvailablePieceCell[i]["ALLOTQUANTITY"]) + orderPieceQuantity;
                orderPieceQuantity = 0.00M;
                tableAllotment.Rows.Add(newRow);

                if (this.isFirstToStockOut 
                    && this.isUseSmallPieceArea
                    && this.tempPieceAreaType != rowsAvailablePieceCell[i]["AREATYPE"].ToString().Trim() 
                    && this.smallPieceAreaAreaType != rowsAvailablePieceCell[i]["AREATYPE"].ToString().Trim() 
                    && Convert.ToDecimal(rowsAvailablePieceCell[i]["AVAILABLE"]) > 0)
                {
                    if (!MovePieceAreaToSmallPieceArea(rowDetail, rowsAvailablePieceCell[i]))//todo  同一储位多单操作
                    {
                        this.errorlist.Add(string.Format("--件烟区已满,{0}({1}) 移库到件烟区失败！", productName,productCode));
                    }
                }
            }
            else if (availableQuantity > 0)
            {
                newRow["QUANTITY"] = availableQuantity;
                rowsAvailablePieceCell[i]["AVAILABLE"] = 0.00M;
                rowsAvailablePieceCell[i]["ALLOTQUANTITY"] = Convert.ToDecimal(rowsAvailablePieceCell[i]["ALLOTQUANTITY"]) + availableQuantity;
                orderPieceQuantity = orderPieceQuantity - availableQuantity;
                tableAllotment.Rows.Add(newRow);
            }
        }
        return orderPieceQuantity;
    }

    private bool MovePieceAreaToSmallPieceArea(DataRow rowDetail, DataRow targetAvailableCell, decimal quantity)
    {
        foreach (string areaType in this.pieceAreaTypesOfMovePieceAreaToSmallPieceArea)
        {
            if (quantity > 0)
            {
                string filter = string.Format("AREATYPE={0} AND CURRENTPRODUCT='{1}'", areaType, productCode);
                string orderby = "LAYER_NO,INPUTDATE,AREATYPE,AREACODE,SHELFCODE,CELLCODE";
                DataRow[] sourceAvailableCell = tableAvailableCell.Select(filter, orderby);
                if (sourceAvailableCell.Length > 0)
                {
                    quantity = MovePieceAreaToPieceArea(rowDetail, sourceAvailableCell, targetAvailableCell, quantity);
                }
            }
        }
        if (quantity > 0)
        {
            this.errorlist.Add(string.Format("--{0}({1}) 库存不足或件烟区已满，移库到件烟区失败！", productName,productCode));
            return false;
        }
        return true;
    }

    private decimal MovePieceAreaToPieceArea(DataRow rowDetail, DataRow[] sourceAvailableCelles, DataRow targetAvailableCell, decimal quantity)
    {
        if (sourceAvailableCelles.Length > 0)
        {
            foreach (DataRow sourceAvailableCell in sourceAvailableCelles)
            {
                if (quantity <= 0.00M)
                {
                    break;
                }

                if (sourceAvailableCell["UNITCODE"].ToString().Trim() != productPieceUnitCode)
                {
                    continue;
                }

                if (Convert.ToDecimal(sourceAvailableCell["AVAILABLE"]) > 0.00M)
                {
                    DataRow NewRows = tableMoveDetail.NewRow();
                    NewRows["BILLNO"] = rowDetail["BILLNO"].ToString() + "M";
                    NewRows["PRODUCTCODE"] = productCode;
                    NewRows["PRODUCTNAME"] = productName;
                    NewRows["UNITCODE"] = productPieceUnitCode;
                    NewRows["UNITNAME"] = productPieceUnitName;
                    NewRows["OUT_CELLCODE"] = sourceAvailableCell["CELLCODE"].ToString().Trim();
                    NewRows["OUT_CELLNAME"] = sourceAvailableCell["CELLNAME"].ToString().Trim();
                    NewRows["IN_CELLCODE"] = targetAvailableCell["CELLCODE"].ToString().Trim();
                    NewRows["IN_CELLNAME"] = targetAvailableCell["CELLNAME"].ToString().Trim();

                    NewRows["QUANTITY"] = Convert.ToDecimal(sourceAvailableCell["AVAILABLE"]);
                    targetAvailableCell["AVAILABLE"] = Convert.ToDecimal(targetAvailableCell["AVAILABLE"]) + Convert.ToDecimal(sourceAvailableCell["AVAILABLE"]);                                      
                    quantity = quantity - Convert.ToDecimal(sourceAvailableCell["AVAILABLE"]);

                    sourceAvailableCell["AVAILABLE"] = 0.00M;

                    tableMoveDetail.Rows.Add(NewRows);
                }
            }
        }
        return quantity;
    }
}
