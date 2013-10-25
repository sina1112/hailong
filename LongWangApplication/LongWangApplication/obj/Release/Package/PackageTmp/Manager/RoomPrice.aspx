<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoomPrice.aspx.cs" Inherits="LongWangApplication.Manager.RoomPrice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Resource/Js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../Resource/Js/operamasks-ui.min.js" type="text/javascript"></script>
    <script src="../Resource/Js/function.js" type="text/javascript"></script>
    <link href="../Resource/Css/operamasks-ui/elegant/om-elegant.css" rel="stylesheet"
        type="text/css" />
    <link href="../Resource/Css/style.css" rel="stylesheet" type="text/css" />
    <script src="../Resource/Js/jBox/jquery.jBox-2.3.min.js" type="text/javascript"></script>
    <link href="../Resource/Js/jBox/Skins/Blue/jbox.css" rel="stylesheet" type="text/css" />
    <script src="../Resource/Js/jBox/i18n/jquery.jBox-zh-CN.js" type="text/javascript"></script>
    <script src="../Resource/Js/loadMask/jquery.loadmask.min.js" type="text/javascript"></script>
    <link href="../Resource/Js/loadMask/jquery.loadmask.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var handlerUrl = "../Ashx/ManagerHandler.ashx";
        var state = GetUrlParameter("state");
        $(function () {
            var hotelId = GetUrlParameter("hotelId");
            
            $('#page').omBorderLayout({
                panels: [{
                    id: "north-panel",
                    region: "north",
                    resizable: false,
                    collapsible: false,
                    header: false
                }, {
                    id: "center-panel",
                    region: "center",
                    resizable: false,
                    collapsible: false,
                    header: false
                }],
                spacing: 3
            });

            //下拉框初始化
            $('#roomtypeCombo').omCombo({
                emptyText: '请选择房型',
                editable: true,
                optionField: function (data, index) {
                    return data.Name;
                },
                valueField: function (data, index) {
                    return data.RoomTypeId;
                },
                inputField: function (data, index) {
                    return data.Name;
                },
                onValueChange: function (target, newValue, oldValue, event) {
                    $("#tbRoomPriceList").omGrid({ dataSource: handlerUrl, extraData: { action: "getRoomPrice", roomTypeId: newValue, state: state} });
                }




            });
            $.ajax({ url: handlerUrl, data: { action: "getRoomType", hotelId: hotelId }, cache: true, type: "POST",
                success: function (data) {
                    data = $.parseJSON(data);
                    if (!data.IsError) {
                        if (data.Result.length > 0) {
                            $('#roomtypeCombo').omCombo('setData', data.Result);
                            $('#roomtypeCombo').omCombo('value', data.Result[0].RoomTypeId);
                           // $("#tbRoomPriceList").omGrid({ dataSource: handlerUrl, extraData: { action: "getRoomPrice", roomTypeId: data.Result[0].RoomTypeId, state: state} });
                        }
                    }
                }
            });
            //表格初始化
            $('#tbRoomPriceList').omGrid({
                method: "POST",
                limit: 0,
                height: 530,
                singleSelect: false,
                preProcess: function (data) {
                    if (!data.IsError) {

                        return data.Result;
                    } else {
                        jBox.tip(data.Result, "error");
                        return Grid_None;
                    }
                },
                colModel: [{ header: '房型名称', name: 'roomtypename', width: 'autoExpand', align: 'left' },

                             { header: '日期', name: 'night', width: 100, align: 'center',
                                 renderer: function (colValue) {
                                     return $.format("{0}[{1}]", colValue,formatDate(new Date(colValue)));
                                }
                              },

                             { header: '早餐类型', name: 'includebreakfastqty2', align: 'center', width: 70,
                                 renderer: function (colValue) {
                                     var bstr = "";

                                     switch (colValue) {
                                         case 10:
                                             bstr = "不含";
                                             break;
                                         case 11:
                                             bstr = "1份中早";
                                             break;
                                         case 12:
                                             bstr = "1份西早";
                                             break;
                                         case 13:
                                             bstr = "1份自助";
                                             break;
                                         case 21:
                                             bstr = "2份中早";
                                             break;
                                         case 22:
                                             bstr = "2份西早";
                                             break;
                                         case 23:
                                             bstr = "2份自助";
                                             break;
                                         case 31:
                                             bstr = "3份中早";
                                             break;
                                         case 32:
                                             bstr = "3份西早";
                                             break;
                                         case 33:
                                             bstr = "3份自助";
                                             break;
                                         case 34:
                                             bstr = "床位早";
                                             break;
                                         case 35: //4份自助
                                             bstr = "4份自助";
                                             break;
                                         case 36: //5份自助
                                             bstr = "5份自助";
                                             break;
                                         case 37: //6份自助
                                             bstr = "6份自助";
                                             break;
                                         case 38: //7份自助
                                             bstr = "7份自助";
                                             break;
                                         case 39: //1份早晚自助
                                             bstr = "1份早晚自助";
                                             break;
                                         case 40: //2份早晚自助
                                             bstr = "2份早晚自助";
                                             break;
                                         default: //含早，数量不定
                                             bstr = "含早";
                                             break;
                                     }
                                     return bstr;
                                 }
                             },
                             { header: '成本价', name: 'preeprice', align: 'right', width: '40' },
                             { header: '增减价', name: 'ChanagePrice', align: 'right', width: '40',
                                 renderer: function (colValue) {
                                     var str = "";
                                     if (colValue < 0)
                                         str = $.format("<span style=\"color:red;\">{0}</span>", colValue);
                                     else
                                         str = colValue;
                                     return str;
                                 }
                             },
                             { header: '销售价', name: 'SalePrice', align: 'right', width: '40',
                                 renderer: function (colValue, colData) {
                                     var str = colData.preeprice + colData.ChanagePrice;
                                     return str;

                                 }
                             },
                             { header: '房间数(捷旅)', name: 'qtyable', align: 'right', width: '70',
                                 renderer: function (colValue) {
                                     var str = "";
                                     if (colValue > 0)
                                         str = $.format("<span style=\"color:green;\">{0}</span>", colValue);
                                     else if (colValue == 0)
                                         str = colValue;
                                     else if (colValue < 0)
                                         str = $.format("<span style=\"color:red;\">{0}</span>", colValue); ;
                                     return str;
                                 }
                             },
                              { header: '房态(捷旅)', name: 'roomState', align: 'center', width: '70',
                                  renderer: function (colValue) {
                                      var str = "";
                                      if (colValue > 0)
                                          str = "<span style=\"color:green;\">确认有房</span>";
                                      else if (colValue == 0)
                                          str = "待确认";
                                      else if (colValue < 0)
                                          str = "<span style=\"color:red;\">满房</span>";
                                      return str;
                                  }

                              },
                              { header: '房态(龙网)', name: 'hailongRoomState', align: 'center', width: '70',
                                  renderer: function (colValue) {
                                      var str = "";
                                      if (colValue == 1)
                                          str = "<span style=\"color:green;\">确认有房</span>";
                                      else if (colValue == 0)
                                          str = "待确认";
                                      else if (colValue == -1)
                                          str = "<span style=\"color:red;\">满房</span>";
                                      return str;
                                  }
                              },
                            ]
            });

            //按钮初始化
            $(':button').omButton({});

            //修改增减价
            $('#modifyPriceBtn').click(function () {
                var itemList = initModify();
                if (itemList == null)
                    return;
                var roomTypeId = $('#roomtypeCombo').val();
                var html = "<div style='padding:10px;'>输入增减价：<input  id='chanagePricetxt' name='chanagePricetxt' /></div>";
                var submit = function (v, h, f) {
                    if (f.chanagePricetxt == '') {
                        $.jBox.tip("请输入增减价。", 'error', { focusId: "chanagePricetxt" });
                        return false;
                    }
                    else {
                        $.ajax({ url: handlerUrl, data: { action: "updateChanagePrice", hotelId: hotelId, roomTypeId: roomTypeId, changePrice: f.chanagePricetxt, itemList: arrayToJson(itemList) }, cache: false, type: "POST",
                            success: function (data) {
                                data = $.parseJSON(data);
                                if (!data.IsError) {
                                    //$.jBox.tip("修改成功！","success");
                                    $("#tbRoomPriceList").omGrid("reload");
                                }
                                else
                                    $.jBox.tip(data.Result, "error");
                            }
                        });
                    }

                    return true;
                };

                $.jBox(html, { title: "批量设置增减价", submit: submit });
                $('#chanagePricetxt').omNumberField({ allowDecimals: false });

            });
            
            //修改房态（龙网）
            $('#modifyLWRoomStateBtn').click(function () {

                var itemList = initModify();
                if (itemList == null)
                    return;
                var roomTypeId = $('#roomtypeCombo').val();
                var html = "<div style='float:left;width:100px;height:40px;padding-top:12px;text-align:right'>请选择房态：</div><div style='height:40px;padding-top:10px;'><input  id='roomStateCombo' /></div>";
                var submit = function (v, h, f) {
                    if ($('#roomStateCombo').val() == '') {
                        $.jBox.tip("请选择房态。", 'error', { focusId: "roomStateCombo" });
                        return false;
                    }
                    else {
                        $.ajax({ url: handlerUrl, data: { action: "updateRoomState", hotelId: hotelId, roomTypeId: roomTypeId, changeRoomState: $('#roomStateCombo').val(), itemList: arrayToJson(itemList) }, cache: false, type: "POST",
                            success: function (data) {
                                data = $.parseJSON(data);
                                if (!data.IsError) {
                                    // $.jBox.tip("修改成功！","success");
                                    $("#tbRoomPriceList").omGrid("reload");
                                }
                                else
                                    $.jBox.tip(data.Result, "error");
                            }
                        });
                    }

                    return true;
                };

                $.jBox(html, { title: "批量设置房态（龙网）", submit: submit });
                $('#roomStateCombo').omCombo({
                    dataSource: [{ text: '满房', value: -1 },
                                   { text: '确认有房', value: 1 },
                                   { text: '待确认', value: 0 }, { text: '同步捷旅房态', value: -2}],
                    value: -1,
                    editable: false
                });


            });
            function initModify() {
                var selectedRecords = $('#tbRoomPriceList').omGrid('getSelections', true);
                if (selectedRecords.length < 1)
                    jBox.tip("请先选择要批量修改的记录！", "alert");
                var selectIds = [];
                if (selectedRecords.length < 1)
                    return null;
                var itemList = [];

                $.each(selectedRecords, function (i, item) {
                    var it = {
                        night: item.night,
                        includebreakfastqty2: item.includebreakfastqty2,
                        ratetype:item.ratetype
                    };
                    itemList.push(it);

                });
                return itemList;
            }

        });


       
    </script>
</head>
<body>
    <div id="page" style="width: 900px; height: 600px">
        <div id="north-panel">
            <table>
                <tr  style="margin: 5px 0px 5px 20px;  ">
                    <td>房型：</td>
                    <td><input id="roomtypeCombo" /></td>
                </tr>
            </table>
        </div>
        <div id="center-panel">
            <div style="text-align: right">
                <button id="modifyPriceBtn" class="btn-moreletter">
                    修改增减价</button>&nbsp;
                <button id="modifyLWRoomStateBtn" class="btn-moreletter">修改房态(龙网)</button>
            </div>
            <table id="tbRoomPriceList">
            </table>
        </div>
    </div>
</body>
</html>
