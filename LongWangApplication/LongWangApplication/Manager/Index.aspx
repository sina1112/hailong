<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="LongWangApplication.Manager.Index" %>

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
        $(function () {
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
            $('#cityCombo').omCombo({
                emptyText: '请选择或输入城市',
                forceSelction: true,
                filterDelay: 100,
                optionField: function (data, index) {
                    return data.Name;
                },
                valueField: function (data, index) {
                    return data.Name;
                },
                inputField: function (data, index) {
                    return data.Name;
                },
                filterStrategy: function (value, data) {
                    return data.Name.indexOf(value.toUpperCase()) > -1;
                }
            });
            $.ajax({ url: handlerUrl, data: { action: "getCity" }, cache: true, type: "POST",
                success: function (data) {
                    data = $.parseJSON(data);
                    if (!data.IsError) {
                        if (data.Result.length > 0) {
                            $('#cityCombo').omCombo('setData', data.Result);
                            $('#cityCombo').omCombo('value', '全国');
                            refreshHotelFn();
                        }
                    }
                }
            });


            $('.btn-moreletter').omButton({});
            $('.btn-2letter').omButton({});
            //表格初始化
            $('#tbHotelList').omGrid({
                method: "POST",
                limit: 100,
                height: $(window).height() - 65,
                width:1020,
                singleSelect: false,
                preProcess: function (data) {
                    if (!data.IsError) {

                        return data.Result;
                    } else {
                        jBox.tip(data.Result, "error");
                        return Grid_None;
                    }
                },
                colModel: [{ header: 'HotelID', name: 'HotelId', width: 50, align: 'center' },

                             { header: '城市', name: 'CityName', width: 60, align: 'left' },

                             { header: '酒店名称', name: 'HotelName', align: 'left', width: 'autoExpand' },
                             { header: '是否上线', name: 'IsActive', align: 'center', width: 50,
                                 renderer: function (colValue) {
                                     var isActive = "";
                                     switch (colValue) {
                                         case "8":
                                             isActive = "<span style=\"color:gray;\">否</span>";
                                             break;
                                         case "1":
                                             isActive = "<span style=\"color:red;\">是</span>";
                                             break;
                                     }
                                     return isActive;
                                 }
                             },
                            { header: '操作', name: 'Operate', align: 'left', width: 100,
                                renderer: function (colValue, rowData) {
                                    var edit = $.format("<a id='{0}' href=\"javascript:onShowRoomPrice('{0}','{1}','{2}');\">房价管理</a>", rowData.HotelId, rowData.HotelName, rowData.State);
                                    return edit;
                                }
                            }]

            });

            var refreshHotelFn = function () {
                var v = $.trim($('#cityCombo').val());
                var hn = $.trim($('#hotelTxt').val());
                getActiveCountFn();
                if (v == "全国")
                    v = "";
                $("#tbHotelList").omGrid({ dataSource: handlerUrl, extraData: { action: "getHotelByKey", cityName: v, key: hn} });

            }
            //搜索酒店
            $('#searchBtn').click(function () {
                refreshHotelFn();

            });
            //批量上下线
            $('#activeBtn').click(function () {
                var html = [];
                html.push("<table style='width:300px;padding:0px 50px; '>");
                html.push("<tr><td style='height:40px'><input type='radio' name='setActiveRd' value='1' checked='checked'/>上线选中的酒店</td></tr>");
                html.push("<tr><td style='height:40px'><input type='radio' name='setActiveRd' value='2' />下线选中的酒店</td></tr>");
                html.push("<tr><td style='height:40px'><input type='radio' name='setActiveRd' value='3' />下线所有酒店</td></tr>");
                html.push("</table>");
                var submit = function (v, h, f) {
                    var item = $('input:radio[name="setActiveRd"]:checked').val();
                    switch (item) {
                        case '1':
                            var selectIds = getSelectFn();
                            if (selectIds.length < 1) {
                                jBox.tip("请先选择要操作的酒店！", "alert");
                            } else
                                updateHotelActive("1", selectIds.join(","));
                            break;
                        case '2':
                            var selectIds = getSelectFn();
                            if (selectIds.length < 1) {
                                jBox.tip("请先选择要操作的酒店！", "alert");
                            } else
                                updateHotelActive("8", selectIds.join(","));
                            break;
                        case '3':
                            AllHotelUnActive();
                            break;
                    }
                };
                $.jBox(html.join(""), { title: "酒店上下线", width: "500", submit: submit });


            });

            //批量房态设置
            $('#rstateSettingBtn').click(function () {
                var selectIds = getSelectFn();

                if (selectIds.length < 1) {
                    jBox.tip("请先选择要操作的酒店！", "alert");
                } else {
                    var html = "<table style='padding:0px 10px; '><tr><td style='width:60px;text-align:right;height:40px'>设置时段：</td><td><input id='startTimeInput' /> 至 <input id='endTimeInput' /></td></tr><tr><td style='text-align:right;height:40px'>选择房态：</td><td><input  id='roomStateCombo' /></td></tr></table>";
                    var submit = function (v, h, f) {
                        var sinput = $('#startTimeInput').val();
                        var einput = $('#endTimeInput').val();
                        var sdate = $('#startTimeInput').omCalendar('getDate');
                        var edate = $('#endTimeInput').omCalendar('getDate');

                        if (sinput == '') {
                            $.jBox.tip("请选择开始时间。", 'error', { focusId: "startTimeInput" });
                            return false;
                        }
                        else if (einput == '') {
                            $.jBox.tip("请选择结束时间。", 'error', { focusId: "endTimeInput" });
                            return false;
                        }
                        else if (sdate > edate) {
                            $.jBox.tip("开始时间不能大于结束时间。", 'error', { focusId: "startTimeInput" });
                            return false;
                        }
                        else {
                            $.ajax({ url: handlerUrl, data: { action: "batchUpdateRoomState", hotelIds: selectIds.join(","), changeRoomState: $('#roomStateCombo').val(), startTime: sinput, endTime: einput }, cache: false, type: "POST",
                                success: function (data) {
                                    data = $.parseJSON(data);
                                    if (!data.IsError) {
                                        $.jBox.tip("批量设置成功！", "success");
                                    }
                                    else
                                        $.jBox.tip(data.Result, "error");
                                }
                            });
                        }

                        return true;
                    };

                    $.jBox(html, { title: "批量设置房态（龙网）", width: "500", submit: submit });
                    $('#roomStateCombo').omCombo({
                        dataSource: [{ text: '满房', value: -1 },
                                   { text: '确认有房', value: 1 },
                                   { text: '待确认', value: 0 }, { text: '同步捷旅房态', value: -2}],
                        value: -1,
                        editable: false
                    });
                    var today = new Date();
                    var start = new Date();
                    var end = new Date();
                    start.setDate(today.getDate() - 1);
                    end.setDate(today.getDate() + 30);
                    $('#startTimeInput').omCalendar({
                        minDate: start,
                        maxDate: end,
                        editable: false
                    });
                    $('#endTimeInput').omCalendar({
                        minDate: start,
                        maxDate: end,
                        editable: false
                    });
                }
            });
            //获取选中ID集合
            var getSelectFn = function () {
                var selectedRecords = $('#tbHotelList').omGrid('getSelections', true);
                var selectIds = [];
                if (selectedRecords.length > 0) {
                    $.each(selectedRecords, function (i, item) {

                        selectIds.push(item.HotelId);

                    });
                }

                return selectIds;
            }
            var updateHotelActive = function (state, ids) {
                var newState = (state == "1") ? "上线" : "下线";
                //            jBox.confirm("是否确定批量" + newState + "选择的酒店？", "温馨提示", function (v) {
                //                if (v == "ok") {
                $("body").mask("请稍候...");
                $.ajax({ url: handlerUrl, data: { "action": "updateHotelActive", "active": state, "hotelids": ids }, cache: false, type: "POST",
                    success: function (data) {
                        data = $.parseJSON(data);
                        if (!data.IsError) {
                            jBox.tip(newState + "酒店成功！");
                            $("#tbHotelList").omGrid("reload");
                            getActiveCountFn();
                        } else {
                            jBox.tip(data.Result, "error");
                        }
                        $("body").unmask();
                    }
                });
                //                }
                //            });
            }
            //清除旧数据
            $('#clearRoomState').click(function () {
                var submit = function (v, h, f) {
                    if (v == 'ok') {
                        $.ajax({ url: handlerUrl, data: { action: "clearRoomState" }, cache: false, type: "POST",
                            success: function (data) {
                                data = $.parseJSON(data);
                                if (!data.IsError) {
                                    $.jBox.tip("清除成功！", "success");
                                }
                                else
                                    $.jBox.tip(data.Result, "error");
                            }
                        });
                    }

                    return true;
                };

                $.jBox.confirm("清除今天之前的旧数据可以提高查询性能，确定清除？", "清除之前的旧数据", submit);

            });
        });
        var AllHotelUnActive = function () {
            //        jBox.confirm("是否下线所有已上线的酒店？", "温馨提示", function (v) {
            //            if (v == "ok") {
            $("body").mask("请稍候...");
            $.ajax({ url: handlerUrl, data: { "action": "allHotelUnActive" }, cache: false, type: "POST",
                success: function (data) {
                    data = $.parseJSON(data);
                    if (!data.IsError) {
                        jBox.tip("下线所有酒店成功！");
                        $("#tbHotelList").omGrid("reload");
                        getActiveCountFn();
                    } else {
                        jBox.tip(data.Result, "error");
                    }
                    $("body").unmask();
                }
            });
            //            }
            //        });
        }
        function getActiveCountFn() {
            var activeHotelCount = 0;
            $.ajax({ url: handlerUrl, data: { action: "getActiveHotelCount" }, cache: false, type: "POST",
                success: function (data) {
                    data = $.parseJSON(data);
                    if (!data.IsError) {
                        activeHotelCount = data.Result;
                        $("#lblacount").text(activeHotelCount);
                    }
                }
            });

        }
        //显示房价管理页
        function onShowRoomPrice(id, name, state) {
            var url = $.format("iframe:RoomPrice.aspx?hotelId={0}&state={1}", id, state);
            var config = {
                id: "RoomPrice",
                title: $.format("房价管理({0})", name),
                top: 0,
                width: 900,
                height: 630,
                buttons: {}
            };
            jBox(url, config);
        }
    </script>
</head>
<body>
    <div id="page" style="width: 1024px; height: 100%">
        <div id="north-panel">
            <table style="margin-left: 5px; height: 30px">
                <tr>
                    <td>
                        城市：
                    </td>
                    <td>
                        <input id="cityCombo" />
                    </td>
                    <td>
                        酒店名：
                    </td>
                    <td>
                        <input type="text" id="hotelTxt" class="login_input" />
                    </td>
                    <td>
                        <button id="searchBtn" class="btn-2letter">
                            搜索</button>
                    </td>
                </tr>
            </table>
        </div>
        <div id="center-panel">
            <div>
                <div style="float: left; padding-top: 5px; color: Red">
                    上线酒店总数：<label id='lblacount'></label></div>
                <div style="text-align: right;">
                    <button id="rstateSettingBtn" class="btn-moreletter">
                        房态设置</button>&nbsp;
                    <button id="activeBtn" class="btn-moreletter">
                        酒店上下线</button>&nbsp;
                    <button id="clearRoomState" class="btn-moreletter">
                        清除旧数据</button>
                </div>
            </div>
            <table id="tbHotelList">
            </table>
        </div>
    </div>
</body>
</html>
