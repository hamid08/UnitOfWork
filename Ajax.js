$(function () {
    var placeholder = $("#modal-placeholder");
    $(document).on('click','button[data-toggle="ajax-modal"]',function () {
        var url = $(this).data('url');
        $.ajax({
            url: url,
            beforeSend: function () { ShowLoading(); },
            complete: function () { $("body").preloader('remove'); },
            error: function () {
                ShowSweetErrorAlert();
            }
        }).done(function (result) {
            placeholder.html(result);
            placeholder.find('.modal').modal('show');
        });
    });


    placeholder.on('click', 'button[data-save="modal"]', function () {
        ShowLoading();
        var form = $(this).parents(".modal").find('form');
        var actionUrl = form.attr('action');

        if (form.length == 0) {
            form = $(".card-body").find('form');
            actionUrl = form.attr('action') + '/' + $(".modal").attr('id');
        }
      
        var dataToSend = new FormData(form.get(0));

        $.ajax({
            url: actionUrl, type: "post", data: dataToSend, processData: false, contentType: false, error: function () {
                ShowSweetErrorAlert();
            }}).done(function (data) {
                var newBody = $(".modal-body", data);
                var newFooter = $(".modal-footer", data);
                placeholder.find(".modal-body").replaceWith(newBody);
                placeholder.find(".modal-footer").replaceWith(newFooter);

            var IsValid = newBody.find("input[name='IsValid']").val() === "True";
            if (IsValid) {
                $.ajax({ url: '/Admin/Base/Notification', error: function () { ShowSweetErrorAlert(); } }).done(function (notification) {

                    ShowSweetSuccessAlert(notification);
                    placeholder.find(".modal").modal('hide');

                });

                var cardElement = $("#CardContent");
                if (cardElement.length != 0) {
                    var cardUrl = cardElement.data('url');
                    $.ajax({ url: cardUrl, error: function () { ShowSweetErrorAlert(); } }).done(function (card) {
                        $("#CardContent").html(card);


                    });
                }
                $table.bootstrapTable('refresh')


            }
        });

        $("body").preloader('remove');
    });


    $(document).on('click', 'a[data-toggle="ajax-modal"]', function () {
        //var url = $(this).data('url');
        var url = $(this).href;
        $.ajax({
            url: url,
            beforeSend: function () { $('#modal-placeholder').after('<div class="preloader d-flex align-items-center justify-content-center"><div class="lds-ellipsis"><div></div><div></div><div></div><div></div></div></div>'); },
            complete: function () { $('.preloader').remove(); },
            error: function () {
                ShowSweetErrorAlert();
            }
        }).done(function (result) {
            placeholder.html(result);
            placeholder.find('.modal').modal('show');
        });
    });

    
});

function ShowSweetErrorAlert() {
    Swal.fire({
        type: 'error',
        title: 'خطایی رخ داده است !!!',
        text: 'لطفا تا برطرف شدن خطا شکیبا باشید.',
        confirmButtonText: 'بستن'
    });
}

function ShowLoading() {
    $("body").preloader({ text: 'لطفا صبر کنید ...' });
}

function ShowSweetSuccessAlert(message) {
    Swal.fire({
        position: 'top-middle',
        type: 'success',
        title: message,
        confirmButtonText: 'بستن',
    })
}





$(document).on('click','button[data-save="Ajax"]',function () {
      var form = $(".newsletter-widget").find('form');
      var actionUrl = form.attr('action');
      var dataToSend = new FormData(form.get(0));

       $.ajax({
            url: actionUrl, type: "post", data: dataToSend, processData: false, contentType: false, error: function () {
                ShowSweetErrorAlert();
            }}).done(function (data) {
                var newForm = $("form", data);
                $(".newsletter-widget").find("form").replaceWith(newForm);
            var IsValid = newForm.find("input[name='IsValid']").val() === "True";
            if (IsValid) {
                $.ajax({ url: '/Admin/Base/Notification', error: function () { ShowSweetErrorAlert(); } }).done(function (notification) {
                    ShowSweetSuccessAlert(notification)
                });
            }
        });
});





function ConfigureSettings(id, action) {
    $.ajax({
        url: "/Admin/UserManager/" + action + "?userId=" + id,
        beforeSend: function () { ShowLoading(); },
        complete: function () { $("body").preloader('remove'); },
        type: "get",
        data: {},
    }).done(function (result) {
        if (result == "فعال" || result == "تایید شده" || result == "قفل نشده") {
            $("#" + action).removeClass("badge-danger").addClass("badge-success");
            $("#btn" + action).removeClass("btn-suceess").addClass("btn-danger");
            if (result == "فعال")
                $("#btn" + action).html("غیرفعال شود");
            else if (result == "قفل نشده")
                $("#btn" + action).html("قفل شود");
            else
                $("#btn" + action).html("تایید نشود");
        }

        else {
            $("#" + action).removeClass("badge-success").addClass("badge-danger");
            $("#btn" + action).removeClass("btn-danger").addClass("btn-success");
            if (result == "غیرفعال")
                $("#btn" + action).html("فعال شود");
            else if (result == "قفل شده")
                $("#btn" + action).html("قفل نشود");
            else
                $("#btn" + action).html("تایید شود");
        }
        $("#" + action).html(result);
    });
}




function ShowErrorMessage(message) {
    Swal.fire({
        type: 'error',
        title: 'خطا !!!',
        text: message,
        confirmButtonText: 'بستن'
    });
}
function ShowToastSuccessAlert() {
    $.toast({
        text: "Don't forget to star the repository if you like it.", // Text that is to be shown in the toast
        heading: 'Note', // Optional heading to be shown on the toast
        icon: 'success', // Type of toast icon
        showHideTransition: 'fade', // fade, slide or plain
        allowToastClose: true, // Boolean value true or false
        hideAfter: 3000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
        stack: 10, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
        position: 'top-left', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values



        textAlign: 'left',  // Text alignment i.e. left, right or center
        loader: true,  // Whether to show loader or not. True by default
        loaderBg: '#4205c0',  // Background color of the toast loader
        beforeShow: function () { }, // will be triggered before the toast is shown
        afterShown: function () { }, // will be triggered after the toat has been shown
        beforeHide: function () { }, // will be triggered before the toast gets hidden
        afterHidden: function () { }  // will be triggered after the toast has been hidden
    });

}
