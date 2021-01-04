const pageController = {
    CurrentControllerName: '',
    RenderUrl: '',
    Users:
    {
        name: 'Users',
        roles: 'Admin',
        default: 'Index',
        edit: 'Edit',
        create: 'Create',
        list: 'List',
        parent: 'Settings'
    },
    TranslationLanguages:
    {
        name: 'TranslationLanguages',
        default: 'Index',
        roles: 'Admin',
        edit: 'Edit',
        create: 'Create',
        list: 'List',
        parent: 'Settings',
        TranslationWords:
        {
            name: 'TranslationWords',
            default: 'Index',
            edit: 'Edit',
            create: 'Create',
            list: 'List'
        }
    },
    Roles:
    {
        name: 'Roles',
        roles: 'Admin',
        default: 'Index',
        edit: 'Edit',
        create: 'Create',
        list: 'List',
        parent: 'Settings'
    },
    KeyValues:
    {
        name: 'KeyValues',
        roles: 'User,Admin',
        default: 'Index',
        edit: 'Edit',
        create: 'Create',
        list: 'List'
    },
    KeyTypes:
    {
        name: 'KeyTypes',
        roles: 'User,Admin',
        default: 'Index',
        edit: 'Edit',
        create: 'Create',
        list: 'List'
    },
    Home:
    {
        name: 'Home',
        default: 'Index'
    },
    Settings:
    {
        name: 'Settings',
        default: 'Index',
        roles: 'Admin'
    }
}

const User = {
    UserName: undefined,
    Email: undefined,
    Roles: []
}

function menuClick(e) {
    menuDeActived();
    if (User.UserName === undefined && localStorage.User) {
        const _user = JSON.parse(localStorage.User);
        User.UserName = _user.UserName;
        User.Email = _user.Email;
        User.Roles = _user.Roles;
    }

    const url = $(e).attr('url');
    if (pageController.RenderUrl !== url) {
        startLoading();
        pageController.CurrentControllerName = $(e).attr('controller');
        if (url.split('/').length > 2)
            pageController.RenderUrl = '/' + url.substring(url.indexOf(url.split('/')[2]));
        else
            pageController.RenderUrl = url;
        window.location.hash = pageController.RenderUrl;
        const li = $(e).parent();
        $(li).attr('class', $(li).attr('class') + ' active');
        pageRender(stopLoading);
    }
    const lis = $('#left-menu li');
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
}

function menuDeActived() {
    const lis = $('#left-menu li');
    for (let i = 0; i < lis.length; i++) {
        $(lis[i]).attr('class', $(lis[i]).attr('class').replaceAll('active', ''));
    }
}