var AjaxHandler;

function AjaxUtility() {

    this.CallAsynchMethod = function (serviceUrl, data, onSucceed, onFailed) {
        var settings = AjaxHandler.PrepareSettings(serviceUrl, data, onSucceed, onFailed, true);
        AjaxHandler.CallAsynch(settings);
    };

    this.CallSyncMethod = function (serviceUrl, data, onSucceed, onFailed) {
        var settings = AjaxHandler.PrepareSettings(serviceUrl, data, onSucceed, onFailed, false);
        AjaxHandler.CallAjaxMethod(settings);
    };

    this.CallAsynch = function (settings) {       
        if (settings.isCallAsync) {            
            setTimeout(function () {
                AjaxHandler.CallAjaxMethod(settings);
            }, 300);
        }
        else {
            AjaxHandler.CallAjaxMethod(settings);
        } 
        
    };



    this.CallAjaxMethod = function (settings) {
        if (typeof (showCustomLoader) == "undefined" || showCustomLoader == null || !showCustomLoader) {
            settings.WaitHandler(true, settings.WaitMsg, settings.LoaderContainerId, settings.HideLoader, settings.LoaderPosition);
        }

      
                  $.ajax({
                type: settings.RequestType,
                contentType: settings.RequestContentType,
                dataType: settings.RequestDataType,
                url: settings.Service,
                data: settings.RequestData,
                async: settings.IsCallAsync,
                success: function (responseData) {
                          settings.WaitHandler(false, settings.WaitMsg, settings.LoaderContainerId, settings.HideLoader);
                          settings.SuccessHandler(responseData.d, settings.RequestState);
            },
                error: function (error) {
                          settings.WaitHandler(false, settings.WaitMsg, settings.LoaderContainerId, settings.HideLoader);
                          settings.ErrorHandler(error, settings.RequestState);
            }
            });
        
    };




    this.PrepareSettings = function (serviceUrl, data, onSucceed, onFailed, isCallAsync, requestStateInfo, customWaitMsg, hideLoader, loaderContainerId, loaderPosition) {
        var settings = new JsonAjaxSetting();
        settings.Service = serviceUrl;
        settings.RequestData = JSON.stringify(data);        
        settings.IsCallAsync = isCallAsync;
        if (onSucceed) {
            settings.SuccessHandler = onSucceed;
        }
        if (onFailed) {
            settings.ErrorHandler = onFailed;
        }
        if (requestStateInfo) {
            settings.RequestState = requestStateInfo;
        }
        if (customWaitMsg) {
            settings.WaitMsg = customWaitMsg;
        }
        if(hideLoader){
            settings.HideLoader = hideLoader;
        }
        if (loaderContainerId) {
            settings.LoaderContainerId = loaderContainerId;
        }
        if (loaderPosition) {
            settings.LoaderPosition = loaderPosition;
        }
        return settings;
    };
}

AjaxHandler = new AjaxUtility();


function JsonAjaxSetting() {
    this.RequestType = "POST";
    this.RequestContentType = "application/json; charset=utf-8";
    this.RequestDataType = "json";
    this.Service;
    this.RequestData;
    this.RequestState;
    this.WaitMsg = "Loading";
    this.IsCallAsync = true;
    this.HideLoader = false;
    this.LoaderContainerId = "";
    this.LoaderPosition = "absolute";  /* expected value: absolute/fixed/relative/static */
    this.SuccessHandler = function (responseData, requestState) {
    }
    function DefaultErrorHandler(error, requestState) {
        alert(eval("(" + error.responseText + ")").Message);
    }
    this.ErrorHandler = function (error, requestState) {
        DefaultErrorHandler(error, requestState);
    }

    this.WaitHandler = function (showWait, msg, loaderContainerId, hideLoader, loaderPosition) {
        if (hideLoader) return;
        var progressArea = new ProgressArea();
        progressArea.ProgressHandler(showWait, msg, loaderContainerId, loaderPosition);
    }
}


function ProgressArea() {
    var PAJs = this;

    this.Initialize = function (loaderContainerId, loaderPosition)
    {
        $("#progressAreaAjax").remove();
        if ($("#progressAreaAjax").length == 0) {
            var content = "<div id='progressAreaAjax' style='display: none;position: " + loaderPosition + ";' class='progressDIV pale_border pale_text'><div class='progressAreaImage'></div><div id='progressAreaAjaxMsg' class='header'></div>Please wait...</div>";
            if (loaderContainerId != '') {
                $('#'+ loaderContainerId).append(content);
            }
            else {
                $('body').append(content);
            }
        }
    }

    this.Show = function (msg) {
        msg = msg || "Loading";
        var progress = document.getElementById('progressAreaAjax');
        if (progress != null && typeof progress != 'undefined') {
            var temp_bg_image = $(".progressAreaImage").css('background-image');
            $(".progressAreaImage").css('backgroundImage', 'url()');
            $(".progressAreaImage").css('backgroundImage', temp_bg_image);

            $('#progressAreaAjaxMsg').text(msg);
            progress.style.display = '';
            progress.innerHTML = progress.innerHTML;
        }

    };

    this.Hide = function () {
        $("#progressAreaAjax").hide();
    }

    this.ProgressHandler = function (show, msg, loaderContainerId, loaderPosition) {
        if (show) {
            PAJs.Initialize(loaderContainerId, loaderPosition);
            PAJs.Show(msg);
        }
        else {
            PAJs.Hide();
        }
    }

}

