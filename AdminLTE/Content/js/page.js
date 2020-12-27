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
        roles: 'Admin,User',
        default: 'Index',
        edit: 'Edit',
        create: 'Create',
        list: 'List'
    },
    KeyTypes:
    {
        name: 'KeyTypes',
        roles: 'Admin,User',
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
    const url = $(e).attr('url')
    if (pageController.RenderUrl !== url) {
        startLoading();
        pageController.CurrentControllerName = $(e).attr('controller');
        pageController.RenderUrl = url;
        window.location.hash = pageController.RenderUrl;
        menuDeActived();
        const li = $(e).parent();
        $(li).attr('class', $(li).attr('class') + ' active');
        pageRender(stopLoading);
    }
}

function menuDeActived() {
    const lis = $('#left-menu li');
    for (let i = 0; i < lis.length; i++) {
        $(lis[i]).attr('class', $(lis[i]).attr('class').replace('active', ''));
    }
}