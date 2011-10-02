/*******************************
*
* project name: SAJAX
* version: 1.3
* file name: sajax.js
* date: 2008-02-18
* author: SILENCE
* Email: czsilence@gmail.com
*
*******************************/

/*********************
* CallBackObject
* 构造函数
* method_complete	: 处理服务器端返回数据的自定义函数
* method_getdata	: 构造需要传递的参数的自定义函数
**********************/
function CallBackObject(method_complete, method_getdata)
{
	this.GetData = method_getdata || this.GetData;
	this.OnComplete = method_complete || this.OnComplete;
	this.XMLHttp = this.GetHttpObject();
}

function CallBackObject(method_complete)
{
    this.OnComplete = method_complete || this.OnComplete;
	this.XMLHttp = this.GetHttpObject();
}

/*********************
* GetHttpObject
* 创建XMLHttp对象
* 兼容 IE 和 Firefox
**********************/
CallBackObject.prototype.GetHttpObject = function()
{
	var XMLHttp;
	if(window.ActiveXObject)
	{
		//支持ActiveX对象,Internet Explorer
		XMLHttp = new ActiveXObject("Microsoft.XMLHTTP");
	}
	else if(window.XMLHttpRequest)
	{
		//支持XMLHttpRequest对象,Mozilla Firefox
		XMLHttp = new XMLHttpRequest();
	}
	return XMLHttp;
}

/*********************
* CreateEvent
* 创建与服务器交互的触发器
* objName		: CallBackObject对象
* elementId	: 添加事件的标签ID，如：button
* evtUrl		: 响应点击事件提交的Url
* evtMethod	: 提交Url的方法，"GET" or "POST"
**********************/
CallBackObject.prototype.CreateEvent = function(elementId, evtUrl, evtMethod)
{
	var othis = this;
	var objEvent = document.getElementById(elementId);
	var strData = this.GetData();
	objEvent.style.cursor = "pointer";
	objEvent.onclick = function(){othis.DoCallBack(evtMethod, evtUrl, strData)};
}

/*********************
* PostData
* 以Post方式向服务器提交数据
* Url		: 提交的Url
**********************/
CallBackObject.prototype.PostData = function(Url, strData)
{
    this.DoCallBack("POST", Url, strData);
}

/*********************
* DoCallBack
* 连接服务器并设置回调函数
* Method	: 提交Url的方法，"GET" or "POST"
* Url			: 响应点击事件提交的Url
**********************/
CallBackObject.prototype.DoCallBack = function(Method, Url, strData)
{
	this.XMLHttp = this.GetHttpObject();/*FOR IE6/WIN*/
	if(this.XMLHttp)
	{
		if(this.XMLHttp.readyState == 4 || this.XMLHttp.readyState == 0)
		{
			if(Method == "GET")
			{
				Url += "?";
				Url += strData;
			}
			var othis = this;
			this.XMLHttp.onreadystatechange = function(){othis.onReadyStateChange();}
			this.XMLHttp.open(Method, Url);
			
			if((Method == "POST") && (strData))
			{
				this.XMLHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
				this.XMLHttp.send(strData);
			}
			else
			{
				this.XMLHttp.send(null);
			}
		}
	}
}

/*********************
* onReadyStateChange
* 回调函数
**********************/
CallBackObject.prototype.onReadyStateChange = function()
{
	if(this.XMLHttp.readyState == 1)
	{
		//正在加载
		this.OnLoading();
	}
	else if(this.XMLHttp.readyState == 2)
	{
		//载入完成
		this.OnLoaded();
	}
	else if(this.XMLHttp.readyState == 3)
	{
        //交互
		this.OnInteractive();
	}
	else if(this.XMLHttp.readyState == 4)
	{
		if(this.XMLHttp.status == 0)
		{
			//中断
			if(this.XMLHttp)
			{
				this.XMLHttp.abort();
			}
			this.OnAbort();
		}
		else if(this.XMLHttp.status == 200)
		{
			//alert("操作成功完成");
			//操作成功完成
			this.OnComplete(this.XMLHttp.responseText, this.XMLHttp.responseXML);
		}
		else
		{
			//操作完成但有错误
			this.OnError(this.XMLHttp.status, this.XMLHttp.statusText);
		}
	}
}

/*************************************************************************
* 可根据需要重载的自定义函数
*************************************************************************/

/*************************
* 回调响应函数中的处理函数
* 重载添加自定义操作
* Onloading			: 正在加载
* OnLoaded			: 加载完成
* OnInteractive	: 交互
* OnAbort				: 操作中断
*************************/
CallBackObject.prototype.OnLoading = function(){/*code here*/}
CallBackObject.prototype.OnLoaded = function(){/*code here*/}
CallBackObject.prototype.OnInteractive = function(){/*code here*/}
CallBackObject.prototype.OnAbort = function(){/*code here*/}

/******************
* OnComplete
* 处理服务器端返回数据
*******************/
CallBackObject.prototype.OnComplete = function(responseText, responseXML)
{
	alert("responseText : " + responseText);
}

/********************
* GetData
* 构造需要传递的参数
* 根据需要重载
*********************/
CallBackObject.prototype.GetData = function()
{
	//数据格式为"参数1名=参数1值&参数2名=参数2值&……"
	var strData = "";
	strData += "data=" + escape("data");
	return strData;
}

/******************
* OnError
* 错误处理函数
********************/
CallBackObject.prototype.OnError = function(status, statusText)
{
	alert("错误 : " + status + " : " + statusText);
}

/******************* 全局函数 *********************/

/*********************
* GetItemValue
* 得到输入对象的值并返回字符串类似于："strName=strValue"
**********************/
function getItemValue(itemId)
{
	var strReturn = itemId + "=" + escape(document.getElementById(itemId).value);
	return strReturn;
}

// //JScript 文件

