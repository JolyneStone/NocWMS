export const nocwmsSetting = {
    authConfig: {
        authority: 'http://localhost:5000',
        client_id: 'client_id',
        redirect_uri: 'http://localhost:5200/login-callback',
        response_type: 'id_token token',
        scope: 'openid profile email client_name',//'openid profile email client_name',
        post_logout_redirect_uri: 'http://localhost:5200/logout-callback',
        silent_redirect_uri: 'http://localhost:5200',
        accessTokenExpiringNotificationTime: 4,
        //userStore: new WebStorageStateStore({ store: window.localStorage })
    },
    serverApiBase: 'http://localhost:5100/api'
}