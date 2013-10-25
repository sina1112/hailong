var Grid_None = { total: 0, rows: [] };
var IDCARD_None = "510000000000000000";
//==========================页面加载时JS函数结束===============================
// 检测字符串是否包含非法字符
function checkString(str) {
    var isValid = false;
    var valids = ["<", "%", "\\", ">", "~", "&", "?", "'"];
    $.each(valids, function (key, val) {
        if (str.indexOf(val) >= 0) {
            isValid = true;
            return false; //退出循环。
        }
    });
    return isValid;
}
function vaildStr(str) {
    var isValid = false;
    var valids = ["#", "$", ">", "&"];
    $.each(valids, function (key, val) {
        if (str.indexOf(val) >= 0) {
            isValid = true;
            return false; //退出循环。
        }
    });
    return isValid;
}
//获取url指定参数
function GetUrlParameter(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    else {
        return "";
    }
}

// 打开窗口
function openWindowFn(url, options) {
    jBox(url, options);
}
function ArrayContains(list, item) {
    for (i = 0; i < list.length; i++) {
        if (list[i] == item)
            return true;
    }
    return false;
}
//解决IE6 下 jQuery 操作 select的BUG 
function set_select_val(sel, val) {
    if ($.browser.msie && $.browser.version == "6.0") {
        setTimeout(function () {
            sel.val(val);
        }, 1);
    } else {
        sel.val(val);
    }
}

function repalceChar(str) {
    var reg = new RegExp("#", "g"); //创建正则RegExp对象   
    return str.replace(reg, "");
}

function arrayToJson(o) {
    var r = [];
    if (typeof o == "string") return "\"" + o.replace(/([\'\"\\])/g, "\\$1").replace(/(\n)/g, "\\n").replace(/(\r)/g, "\\r").replace(/(\t)/g, "\\t") + "\"";
    if (typeof o == "object") {
        if (!o.sort) {
            for (var i in o)
                r.push(i + ":" + arrayToJson(o[i]));
            if (!!document.all && !/^\n?function\s*toString\(\)\s*\{\n?\s*\[native code\]\n?\s*\}\n?\s*$/.test(o.toString)) {
                r.push("toString:" + o.toString.toString());
            }
            r = "{" + r.join() + "}";
        } else {
            for (var i = 0; i < o.length; i++) {
                r.push(arrayToJson(o[i]));
            }
            r = "[" + r.join() + "]";
        }
        return r;
    }
    if (typeof o == "undefined") return "\"\"";
    return o.toString();
}

function filterTxt(s) {
    var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）&;—|{}【】‘；：”“'。，、？]");
    var rs = "";
    for (var i = 0; i < s.length; i++) {
        rs = rs + s.substr(i, 1).replace(pattern, '');
    }
    return rs;
}
// 是否是字母
function checkAlphabetFn(str) {
    return str.match(/^[a-zA-Z]*$/);
}
// 格式化日期
function formatDate(date, format) {
    if (arguments.length < 2 && !date.getTime) {
        format = date;
        date = new Date();
    }
    typeof format != 'string' && (format = 'YYYY年MM月DD日 hh时mm分ss秒');
    var week = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', '日', '一', '二', '三', '四', '五', '六'];
    return format.replace(/YYYY|YY|MM|DD|hh|mm|ss|星期|周|www|week/g, function (a) {
        switch (a) {
            case "YYYY": return date.getFullYear();
            case "YY": return (date.getFullYear() + "").slice(2);
            case "MM": return date.getMonth() + 1;
            case "DD": return date.getDate();
            case "hh": return date.getHours();
            case "mm": return date.getMinutes();
            case "ss": return date.getSeconds();
            case "星期": return "星期" + week[date.getDay() + 7];
            case "周": return "周" + week[date.getDay() + 7];
            case "week": return week[date.getDay()];
            case "www": return week[date.getDay()].slice(0, 3);
        }
    });
}
//IE6浏览器提示
function position_fixed(el, eltop, elleft) {
    // check if this is IE6 
    if (!window.XMLHttpRequest)
        window.onscroll = function () {
            el.style.top = (document.documentElement.scrollTop + eltop) + 'px';
            el.style.left = (document.documentElement.scrollLeft + elleft) + 'px';
        }
}
