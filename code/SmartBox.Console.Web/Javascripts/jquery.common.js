;(function($) {

$.request = {};
$.validate = {};
$.common = {};

$.extend(jQuery.request, {
 queryString: function (name) {
        var url = document.URL;
        var arr = url.split('?');
        if (arr.length < 2)    {
            return '';
        }
        url = arr[1];

        //去掉最后一个#字符
        if (url.lastIndexOf('#') == (url.length - 1))
            url = url.substring(0, url.length - 1);

        var arrQueryStringPair = url.split('&');
        if (arrQueryStringPair.length == 0)
            return '';

        //未采用split方法，因为如果形如backUrl=aHR0cDNweD9zdGF0dXM9Mw==
        //的参数采用split方法不能正确获取到等号后面的值
        for (var i = 0; i < arrQueryStringPair.length; ++i) {
            var startIndex = arrQueryStringPair[i].indexOf('=') + 1;
            var sName = arrQueryStringPair[i].substr(0, startIndex - 1);
            var result = arrQueryStringPair[i].substr(startIndex, arrQueryStringPair[i].length - startIndex);
            if (sName.toLowerCase() == name.toLowerCase()) {
                return result;
            }
        }

        return '';
    },
    
    
    /// 调用范例
    /// var params = [];
    /// var o = new Object();
    /// o.name = '_';
    /// o.value = '_';
    /// params.push(o);
        
    /// o = new Object();
    /// o.name = 'sTatus';
    /// o.value = '2';
    /// params.push(o);
        
    /// var url = $.request.replaceQueryString(params);
    ///
    replaceQueryString : function (arrParams) {
        var url = document.URL;
        var destUrl = null;
     var arr = url.split('?');
     if (arr.length < 2) {
      destUrl = arr[0] + '?';
      for (var i = 0; i < arrParams.length; ++i) {
          destUrl += '&' + arrParams[i].name + '=' + arrParams[i].value;
      }
      return destUrl;
     }
     
     if (arr[1].indexOf('&') == 0)
         arr[1] = arr[1].substring(1, arr[1].length);
     url = arr[1];
     
     destUrl = arr[0];

        if (url != '') {
         //去掉最后一个#字符
         if (url.lastIndexOf('#') == (url.length - 1))
          url = url.substring(0, url.length - 1);

         var arrQueryStringPair = url.split('&');
         if (arrQueryStringPair.length == 0)
          return destUrl;
          
            destUrl += '?';

            if (arrQueryStringPair.length > 0 && (arrQueryStringPair[0] != '')) {
                for (var j = 0; j < arrQueryStringPair.length; ++j) {
                    var arrTemp = arrQueryStringPair[j].split('=');
                    if (arrTemp.length == 1) continue;
                    if (arrTemp[0] == '') continue;

                    var flag = false;
                    for (var k = 0; k < arrParams.length; ++k) {
                        if (arrParams[k].name.toLowerCase() == arrTemp[0].toLowerCase()) {
                            destUrl += '&' + arrParams[k].name + '=' + arrParams[k].value;
                            arrParams[k].added = true;
                            flag = true;
                            break;
                        }
                    }
                 
                    if (flag == false)
                        destUrl += '&' + arrTemp[0] + '=' + arrTemp[1];
                }
                
                for (var k = 0; k < arrParams.length; ++k) {
                    if (!arrParams[k].added) {
                        destUrl += '&' + arrParams[k].name + '=' + arrParams[k].value;
                        arrParams[k].added = true;
                    }
                }
            } 
     } else {
         destUrl += '?';
         for (var k = 0; k < arrParams.length; ++k) {
             if (k != 0)
                 destUrl += '&';
             destUrl += arrParams[k].name + '=' + arrParams[k].value;
         }
     }
     
     return destUrl;
    }
});

$.extend(jQuery.validate, {
 isEmail : function(email) {
     return /^.+@.+\..{2,3}$/g.test(email);
 },
 isInt: function(str) {
     return /^\d+$/img.test(str);
 }
});

$.extend(jQuery.common, {
    
    //将value从逗号分隔的字符串中删除
    //比如从a,b,c,d 将c删除，得到a,b,d
    removeFromCommaJoinedText : function (value, container) {
        if (value.length == 0)
            return '';
                
        //去除前后逗号    
        value = value.replace(/^,/, '').replace(/,$/, '');
        container = container.replace(/^,/, '').replace(/,$/, '');
                
        if (container == value)
        {
            return '';
        }
                
        var sArray = container.split(',');
        for (var i = sArray.length - 1; i >= 0; --i)
        {
            if (sArray[i] == value)
                sArray[i] = undefined;
        }
                
        var result = sArray.join(',');
        //因为undefined会连接成,,所以要将,,换成,            
        result = result.replace(/,,/,',');
        result = result.replace(/^,/, '').replace(/,$/, '');
                
        return result;
    },  
    
    
    /*判断两个对象是否相等的js函数
    如果两个对象属性在初始化时出现的顺序不一样 但数目及值一样，最终比较结果也是true
    支持每个属性又是其他类型，如对象、数组、数字、字符串
    
    var a = {Name:"YuanXP",Id:9,Go:{a:'1',b:'2'}};
    var b = {Id:9,Name:"YuanXP",'Go':{a:'1',b:'2'}};

    var r = $.common.equal(a, b);
    alert(r); 
 
    */
    equal : function (objA, objB) {
        if (typeof arguments[0] != typeof arguments[1])
            return false;

        //数组
        if (arguments[0] instanceof Array)
        {
            if (arguments[0].length != arguments[1].length)
                return false;
            
            var allElementsEqual = true;
            for (var i = 0; i < arguments[0].length; ++i)
            {
                if (typeof arguments[0][i] != typeof arguments[1][i])
                    return false;

                if (typeof arguments[0][i] == 'number' && typeof arguments[1][i] == 'number')
                    allElementsEqual = (arguments[0][i] == arguments[1][i]);
                else
                    allElementsEqual = arguments.callee(arguments[0][i], arguments[1][i]);            //递归判断对象是否相等                
            }
            return allElementsEqual;
        }
        
        //对象
        if (arguments[0] instanceof Object && arguments[1] instanceof Object)
        {
            var result = true;
            var attributeLengthA = 0, attributeLengthB = 0;
            for (var o in arguments[0])
            {
                //判断两个对象的同名属性是否相同（数字或字符串）
                if (typeof arguments[0][o] == 'number' || typeof arguments[0][o] == 'string')
                    result = eval("arguments[0]['" + o + "'] == arguments[1]['" + o + "']");
                else {
                    //如果对象的属性也是对象，则递归判断两个对象的同名属性
                    //if (!arguments.callee(arguments[0][o], arguments[1][o]))
                    if (!arguments.callee(eval("arguments[0]['" + o + "']"), eval("arguments[1]['" + o + "']")))
                    {
                        result = false;
                        return result;
                    }
                }
                ++attributeLengthA;
            }
            
            for (var o in arguments[1]) {
                ++attributeLengthB;
            }
            
            //如果两个对象的属性数目不等，则两个对象也不等
            if (attributeLengthA != attributeLengthB)
                result = false;
            return result;
        }
        return arguments[0] == arguments[1];

    }
});

})(jQuery);


//合并两个数组的元素
Array.prototype.union = function(arrayB) {
    for (var i = 0; i < this.length; ++i) {
        var inArrID = false;
        for (var j = 0; j < arrayB.length; ++j) {
            if (arrayB[j] == this[i]) {
                inArrID = true;
                break;
            }
        }
        
        if (!inArrID) {
            arrayB.push(this[i]);
        }
    }
    
    var result = arrayB.join(',');
    result = result.replace(/^,/, '').replace(/,$/, '');
    return result;
}

Array.prototype.removeIndex = function(i)
　{
　  if (isNaN(i) || i > this.length)
　     return false;
　　this.splice(i,1);
　}
　
　Array.prototype.remove = function(key)
　{
　　for (var i = 0; i < this.length; ++i)
　　{
　　   if (this[i] == key)
　　       this.splice(i, 1);
　　}
　}

 
/*
    b = ['1','2','3','4','5'];
    alert("elements: "+b+"nLength: "+b.length);
    b.remove('4');       //删除值为'4'的元素 
    b.removeIndex(3) //删除下标为1的元素
    alert("elements: "+b+"nLength: "+b.length);
*/

//判断数组是否已包含了某个元素的js函数
Array.prototype.contains = function(obj) {
    var i = this.length;
    while (i--) {
        if (this[i] === obj) {
            return true;
        }
    }
    return false;
}