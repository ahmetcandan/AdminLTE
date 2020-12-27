
$(function () {
    $(".datepicker").datepicker({
        autoclose: true
    });

    $('input[type="checkbox"], input[type="radio"]').iCheck({
        checkboxClass: "icheckbox_minimal-blue",
        radioClass: "iradio_minimal-blue"
    });

    $("#datemask").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
    $("#datemask2").inputmask("mm/dd/yyyy", { "placeholder": "mm/dd/yyyy" });
    $("[data-mask]").inputmask();

    // Fix sidebar white space at bottom of page on resize
    $(window).on("load", function () {
        if (localStorage.translateKeys === undefined || localStorage.translateKeys === null || JSON.parse(localStorage.translateKeys).length === 0)
            setTranslateWords();
        if (localStorage.languages === undefined || localStorage.languages === null || JSON.parse(localStorage.languages).length === 0)
            setLanguages();

        const languages = JSON.parse(localStorage.getItem('languages'));
        if (localStorage.languages !== undefined && localStorage.languages !== null && languages.length > 0) {
            for (let i = 0; i < languages.length; i++) {
                $("#language-select").append(new Option(languages[i].Code, languages[i].Code));
            }
        }

        let languageCode = localStorage.getItem('language');
        if (languageCode === null || languageCode === undefined || languageCode === 'null' || languageCode === 'undefined')
            languageCode = 'TR';
        $('#language-select').selectpicker('val', languageCode);
        $('#language-select').parent().width('160px')

        setTimeout(function () {
            $("body").layout("fix");
            $("body").layout("fixSidebar");
        }, 250);

        if (User.UserName === undefined && localStorage.User) {
            const _user = JSON.parse(localStorage.User);
            User.UserName = _user.UserName;
            User.Email = _user.Email;
            User.Roles = _user.Roles;
        }

        const lis = $('#left-menu li');
        menuDeActived();
        for (let i = 0; i < lis.length; i++) {
            const page = pageController[$(lis[i].children[0]).attr('controller')];
            if (page && page.roles) {
                if (anyRoles(User.Roles, page.roles.split(',')))
                    $(lis[i]).show();
                else
                    $(lis[i]).hide();
            }

            if (window.location.hash.length > 2 && $(lis[i].children[0]).attr('controller') === window.location.hash.substring(2)) {
                $(lis[i]).attr('class', ($(lis[i]).attr('class') + ' active').trim());
            }
        }

        const urlHash = window.location.hash;
        if (urlHash !== undefined && urlHash !== null && urlHash.startsWith('#')) {
            startLoading();
            pageController.RenderUrl = urlHash.substring(1);
            window.location.hash = pageController.RenderUrl;
            pageRender(stopLoading);
        }
        else
            loadTranslate();
    });
});