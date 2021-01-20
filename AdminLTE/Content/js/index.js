function newForm(e) {
    let url = $(e).attr('url');
    openForm(url, translate('new'));
}

function editForm(e) {
    let url = $(e).attr('url');
    openForm(url, translate('edit'));
}

function deleteForm(e) {
    let url = $(e).attr('url');
    openForm(url, translate('delete-record'));
}

function newFormLarge(e) {
    let url = $(e).attr('url');
    openFormLarge(url, translate('new'));
}

function editFormLarge(e) {
    let url = $(e).attr('url');
    openFormLarge(url, translate('edit'));
}

function deleteFormLarge(e) {
    let url = $(e).attr('url');
    openFormLarge(url, translate('delete-record'));
}

function newFormSmall(e) {
    let url = $(e).attr('url');
    openFormSmall(url, translate('new'));
}

function editFormSmall(e) {
    let url = $(e).attr('url');
    openFormSmall(url, translate('edit'));
}

function deleteFormSmall(e) {
    let url = $(e).attr('url');
    openFormSmall(url, translate('delete-record'));
}

function openForm(url, title) {
    startLoading();
    renderHtml(url, title, 'modal-body', function () {
        stopLoading();
        openModal('modal-popup');
    }, 'modal-title');
}

function openFormLarge(url, title) {
    startLoading();
    renderHtml(url, title, 'modal-body-lg', function () {
        stopLoading();
        openModal('modal-popup-lg');
    }, 'modal-title-lg');
}

function openFormSmall(url, title) {
    startLoading();
    renderHtml(url, title, 'modal-body-sm', function () {
        stopLoading();
        openModal('modal-popup-sm');
    }, 'modal-title-sm');
}

function openFormMessage(title, message) {
    openModal('modal-popup-message');
    $('#modal-title-message').html(title);
    $('#modal-body-message').html(message);
}

function renderHtml(url, title, htmlid = 'modal-body', success = undefined, titleid = 'modal-title') {
    $('#' + htmlid).addClass('div-blur');
    render(url, htmlid, success);
    $('#' + titleid).html(title);
}

function pageRender(success = undefined) {
    //const page = pageController[pageController.CurrentControllerName];
    const url = pageController.RenderUrl;
    render(url, 'main-content', success);
}

function render(url, htmlid, success = undefined) {
    if (url.split('/').length < 3)
        url = '/' + localStorage.getItem('language') + url;
    $.get(url, function (response, status, header) {
        const xRespondedString = header.getResponseHeader('x-responded-json');
        if (xRespondedString !== null) {
            const xResponded = JSON.parse(xRespondedString);
            if (xResponded.status === 401) {
                if (xResponded.header !== undefined && xResponded.headers.location !== undefined)
                    window.location.href = xResponded.headers.location;
                else
                    window.location.href = window.location.origin + '/' + localStorage.language.toLowerCase() + '/Account/Login?ReturnUrl=' + (window.location.hash.startsWith('#') ? window.location.hash.substring(1) : window.location.hash);
            }
        }
        $('#' + htmlid).removeClass('div-blur');
        $('#' + htmlid).html(response);
        renderAfterWorking();
        loadTranslate();
        if (success)
            success();
    });
}

function getMaxPopupZindex() {
    const popups = $('.modal.fade');
    let result = 1050;
    for (let i = 0; i < popups.length; i++) {
        const zIndex = Number.parseInt($(popups[i]).css('z-index'));
        if (zIndex > result)
            result = zIndex;
    }
    return result;
}

function startLoading() {
    $('#loading').show();
}

function stopLoading() {
    $('#loading').hide();
}

function openModal(id = 'modal-popup') {
    const maxZindex = getMaxPopupZindex();
    $('#' + id).modal();
    $('#' + id).css('z-index', (maxZindex + 1).toString());

}

function modalConfirm(title, content, yes, no, cancel) {
    openModal('modal-confirm-popup');
    $('#modal-confirm-title').html(title);
    $('#modal-confirm-content').html(content);
    $('#btn-modal-confirm-yes').unbind('click');
    $('#btn-modal-confirm-no').unbind('click');
    $('#btn-modal-confirm-cancel').unbind('click');
    if (yes)
        $('#btn-modal-confirm-yes').click(function () { yes(); });
    if (no)
        $('#btn-modal-confirm-no').click(function () { no(); });
    if (cancel)
        $('#btn-modal-confirm-cancel').click(function () { cancel(); });
}

function datePickerWorking() {
    const items = $('.datepicker');
    for (let i = 0; i < items.length; i++) {
        if (items[i])
            $(items[i]).datepicker();
    }
}

function inputMaskWorking() {
    const items = $('input[data-inputmask]');
    for (let i = 0; i < items.length; i++) {
        if (items[i])
            $(items[i]).inputmask();
    }
}

function selectPickerWorking() {
    $('.selectpicker').selectpicker();
}

function dataTableWorking() {
    $('.table').DataTable();
}

function renderAfterWorking() {
    datePickerWorking();
    inputMaskWorking();
    selectPickerWorking();
    dataTableWorking();
}

function closeModal(id = 'modal-popup') {
    $('#' + id).modal('hide');
    $('.modal').css('overflow-y', 'auto');
}

function loginPostForm() {
    $('#loading').show();
    let languageCode = localStorage.language;
    if (window.location.pathname.split('/').length > 2)
        languageCode = window.location.pathname.split('/')[1];
    else if (localStorage.language)
        languageCode = localStorage.language;
    else
        languageCode = 'tr';
    $.post(window.location.origin + '/' + languageCode + '/Account/LoginApi/',
        {
            Email: $('#Email').val(),
            Password: $('#Password').val()
        }, function (data) {
            User.UserName = data.UserName;
            User.Email = data.Email;
            User.Roles = data.Roles;
            localStorage.removeItem('User');
            localStorage.setItem('User', JSON.stringify(User));
            localStorage.setItem('language', languageCode.toUpperCase());
            window.location.href = window.location.origin + '/' + languageCode.toLowerCase() + '#' + getUrlParameter('ReturnUrl');
            loadTranslate();
        });

    $('#loading').hide();
}

function postForm(item, success) {
    $('#loading').show();
    let tagName = $(item).prop('tagName').toLowerCase();
    if (tagName === undefined || tagName === null) return;
    if (tagName === 'form') {
        const url = item.attr('action');
        const xHttp = new XMLHttpRequest();
        xHttp.open('POST', url, false);
        const data = new FormData(item[0]);
        xHttp.send(data);
        const _success = function () {
            if (success)
                success();
            $.toast({
                heading: translate('transaction-successful'),
                icon: 'success',
                position: {
                    right: 1,
                    top: 51
                }
            });
        }
        xHttp.onloadend = _success();
        //const _fail = function () {
        //    toastr.error(translate('transaction-error'));
        //};
        //xHttp.onerror = _fail();
        const modalId = getModalId(item);
        if (modalId === null || modalId === undefined)
            closeModal();
        else
            closeModal(modalId);
        $('#loading').hide();
    }
    else return postForm($(item).parent(), success);
}

function getModalId(element) {
    let tagName = $(element).prop('tagName').toLowerCase();
    if (tagName === undefined || tagName === null) return null;
    if ($(element).attr('class') === 'modal fade in')
        return $(element).attr('id');
    else
        return getModalId(element.parent());
}

function logoff() {
    var postUrl = $('#user-logoff').attr('post-url');
    var redirectUrl = $('#user-logoff').attr('redirect-url');
    $.post(postUrl, function (response) {
        localStorage.removeItem('User');
        $.connection.hub.stop();
        window.location.href = redirectUrl;
    });
}

function setTranslateWords() {
    let languageCode = localStorage.getItem('language');
    if (languageCode === null || languageCode === undefined || languageCode === 'null' || languageCode === 'undefined')
        languageCode = 'TR';
    $.get(window.location.origin + '/' + languageCode + '/TranslationWords/GetTranslationWord/' + languageCode, null, function (data) {
        debugger;
        localStorage.removeItem('translateKeys');
        localStorage.setItem('translateKeys', JSON.stringify(data));
        loadTranslate();
    });
}

function setLanguages() {
    $.get(window.location.origin + '/' + languageCode + '/Languages', null, function (data) {
        localStorage.removeItem('languages');
        localStorage.setItem('languages', JSON.stringify(data));
        loadTranslate();
    });
}

function loadTranslate() {
    const elementTranslate = $('[translate]');
    for (let i = 0; i < elementTranslate.length; i++) {
        const tagName = $(elementTranslate[i]).prop('tagName').toLowerCase();
        if (tagName === "input")
            $(elementTranslate[i]).val(translate($(elementTranslate[i]).attr('translate')));
        else
            $(elementTranslate[i]).html(translate($(elementTranslate[i]).attr('translate')));
    }
}

function translate(code) {
    if (localStorage.translateKeys === null || localStorage.translateKeys === undefined || localStorage.translateKeys.length === 0)
        return code;
    const translateKeys = JSON.parse(localStorage.translateKeys);
    debugger;
    return (translateKeys.filter(c => c.Code === code)[0] === undefined) ? code : translateKeys.filter(c => c.Code === code)[0].Description;
}

function changeLanguage(e) {
    localStorage.removeItem('language');
    localStorage.setItem('language', e.value);
    window.location.pathname = '/' + e.value.toLowerCase();
}

TurkishCharacter = function (text) {
    return text.replaceAll('ı', 'i').replaceAll('ğ', 'g').replaceAll('ö', 'o').replaceAll('ş', 's').replaceAll('ç', 'c').replaceAll('ü', 'u');
}

function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};

function anyRoles(userRoles, pageRoles) {
    for (let i = 0; i < userRoles.length; i++) {
        for (let j = 0; j < pageRoles.length; j++) {
            if (userRoles[i] === pageRoles[j])
                return true;
        }
    }
    return false;
}